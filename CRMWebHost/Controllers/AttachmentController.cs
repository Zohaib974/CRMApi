using AutoMapper;
using CRMContracts;
using CRMContracts.Email;
using CRMEntities.Models;
using CRMHelper;
using CRMModels;
using CRMModels.Common;
using CRMModels.DataTransfersObjects;
using CRMWebHost.ActionFilters;
using CRMWebHost.Base;
using CRMWebHost.ModelBinders;
using EmailService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebHost.Controllers
{
    //[Authorize]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/attachments")]
    [ApiController]
    public class AttachmentController : BaseController
    {
        #region declarations
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IServiceManager _serviceManager;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;
        public string attachmentDirectory;
        public string filePath;
        public bool isUploadedSuccessfully;
        public AttachmentController(IRepositoryManager repository, ILoggerManager logger,
                                    IServiceManager serviceManager, IMapper mapper,
                                    IHostingEnvironment hostingEnvironment, IConfiguration configuration,
                                     IEmailService emailService, UserManager<User> userManager)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _serviceManager = serviceManager;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _userManager = userManager;
            _emailService = emailService;
            attachmentDirectory = configuration.GetSection("AttachmentFolderPath").Value;
            isUploadedSuccessfully = false;

        }
        #endregion

        [HttpGet("listAttachments")]
        public List<AttachmentGroups> ListAttachments([FromQuery] AttachmentParameters attachmentParameters)
        {
            var response = _serviceManager.GetAttachments(attachmentParameters);
            return response; 
        } 
        [HttpPost("uploadAttachments")]
        public async Task<CommonResponse> UploadAttachments([FromForm]UploadAttachmentsDto uploadAttachments)
        {
            var File = HttpContext.Request.Form.Files;
            if (File == null || File.Count == 0 || uploadAttachments.UploadedBy == 0 || uploadAttachments.ContactId == 0)
                return new CommonResponse() { Successful = false,Message = "Uploaded_By/Contact_Id/Files requried" };
            
            List<CreateAttachmentDto> attachments = new List<CreateAttachmentDto>();
            string notUploadedFiles = string.Empty;
            foreach(var file in File)
            {
                var size = Math.Round((decimal)(file.Length / (1024 * 1024))); //2MB
                if (size < 2)
                {
                    filePath = Path.Combine("", _hostingEnvironment.ContentRootPath + attachmentDirectory);
                    var path = FileUploadHelper.UploadFiles(filePath, file, uploadAttachments.IsImage ? "Image_" : "File_", _logger, out isUploadedSuccessfully);

                    FileInfo fi = new FileInfo(file.FileName);
                    var attchment = new CreateAttachmentDto()
                    {
                        IsUploaded = isUploadedSuccessfully,
                        UploadedBy = uploadAttachments.UploadedBy,
                        ContactId = uploadAttachments.ContactId,
                        FileExension = fi.Extension,
                        FileName = file.FileName,
                        FileLink = path,
                        FileSize = FileUploadHelper.SizeConverter(file.Length),
                        isImageFile = uploadAttachments.IsImage
                    };
                    attachments.Add(attchment);
                }
                else
                {
                    notUploadedFiles = notUploadedFiles + (string.IsNullOrWhiteSpace(notUploadedFiles)?"" : ",") + file.FileName;
                }
            }
            var response =await _serviceManager.AddAttchmentsAsync(attachments);
            response.Message = !string.IsNullOrEmpty(notUploadedFiles) ?  response.Message + "Filesize more than 2 mb are not uploaded.Filenames: " + notUploadedFiles : response.Message;
            return response;
        }
        [HttpGet("downloadAttachments")]
        public async Task<IActionResult> DownloadAttachments(string ids)
        {
            if (string.IsNullOrEmpty(ids))
                return Ok(new CommonResponse(false, "No document identifier provided"));

            var objectArray = ids.Split(new[] { "," },
                                                StringSplitOptions.RemoveEmptyEntries)
                                               .Select(x => long.Parse(x.Trim()))
                                               .ToArray();
            if (ids == null)
            {
                return Ok(new CommonResponse(false, "Invalid document identifers provided."));
            }
            var response = await _serviceManager.GetAttachmentsByIdsAsync(objectArray);
            if (response.Any())
            {
                return DownloadAttachments(response.Select(a => a.FileLink).ToList());
            }
            return Ok(new CommonResponse(false, "No files found"));
        }
        [HttpPost("emailAttachment")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<CommonResponse> EmailAttachment(EmailAttachmentDto emailAttachment)
        {
            var response = await _serviceManager.GetAttachmentsByIdAsync(emailAttachment.DocumentId);
            if (response == null)
                return new CommonResponse(false, $"Document with identifier {emailAttachment.DocumentId} doesnt not exist.");

            IRecipient recipient = new Recipient(response.FileName , emailAttachment.EmailAddress);
            IEmailModel emailModel = new AttachmentEmailModel(recipient);

            emailModel.Attachments.Add(response.FileLink);
            await _emailService.SendEmailAsync(emailModel);
            return new CommonResponse(true, "Email sent successfully");
        }
        #region private methods
        private FileResult DownloadAttachments(List<string> fileNames)
        {
            try
            {
                var zipName = $"archive-{DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss")}.zip";
                //var files = Directory.GetFiles(Path.Combine("", _hostingEnvironment.ContentRootPath + attachmentDirectory)).ToList();
                var files = fileNames;
                List<byte[]> filesPath = new List<byte[]>();
                files.ForEach(file =>
                {
                    var fPath = file;
                    byte[] bytes = Encoding.ASCII.GetBytes(fPath);
                    filesPath.Add(bytes);
                });
                using (var memoryStream = new MemoryStream())
                {
                    using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {
                        filesPath.ForEach(file =>
                        {
                            string fPath = Encoding.ASCII.GetString(file);
                            var entry = archive.CreateEntry(System.IO.Path.GetFileName(fPath), CompressionLevel.Fastest);
                            using (var streamWriter = entry.Open())
                            {
                                streamWriter.Write(file, 0, file.Length);
                            }
                        });
                    }
                    return File(memoryStream.ToArray(), "application/zip", zipName);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }

        }
        #endregion
    }
}
