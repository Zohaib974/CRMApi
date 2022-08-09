using AutoMapper;
using CRMContracts;
using CRMEntities.Models;
using CRMHelper;
using CRMModels;
using CRMModels.Common;
using CRMModels.DataTransfersObjects;
using CRMWebHost.ActionFilters;
using CRMWebHost.Base;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebHost.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/contacts")]
    [ApiController]
    public class ContactController : BaseController
    {
        #region declarations
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IServiceManager _serviceManager;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        public string profileImgeDirectory;
        public string importContactDirectory;
        public string filePath;
        public bool isUploadedSuccessfully;
        public ContactController(IRepositoryManager repository, ILoggerManager logger,
                                    IServiceManager serviceManager, IMapper mapper,
                                    IHostingEnvironment hostingEnvironment, IConfiguration configuration,
                                    UserManager<User> userManager) : base(userManager)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _serviceManager = serviceManager;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _userManager = userManager;
            profileImgeDirectory = configuration.GetSection("ProfileImageFolderPath").Value;
            importContactDirectory = configuration.GetSection("ContactImportFolderPath").Value;
            isUploadedSuccessfully = false;

        }
        #endregion
        [HttpPost("addContact")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ContactDto> AddContact([FromForm]CreateContactDto contact)
        {
            var files = HttpContext.Request.Form.Files;
            if (!FileUploadHelper.IsFileExtensionSupported(contact.File))
                return new ContactDto() { Message = "Unsupported file format.Please upload .jpeg file.", Successful = false };

            filePath = Path.Combine("", _hostingEnvironment.ContentRootPath + profileImgeDirectory);
            var path = FileUploadHelper.UploadFiles(filePath, contact.File, "Image_", _logger, out isUploadedSuccessfully);
            contact.ProfileImageLink = path;
            var response = await _serviceManager.CreateContactAsync(contact);
            return response;
        }
        [HttpGet("listContacts")]
        public IActionResult ListContacts([FromQuery] ContactParameters contactParameters)
        {
            var response = _serviceManager.GetContacts(contactParameters);
            return Ok(response);
        }
        [HttpPost("updateContact")]
        public async Task<ContactDto> UpdateContact(UpdateContactDto contact)
        {
            //if (!FileUploadHelper.IsFileExtensionSupported(contact.File))
            //    return new ContactDto() { Message = "Unsupported file format.Please upload .jpeg file.", Successful = false };
            //filePath = Path.Combine("", _hostingEnvironment.ContentRootPath + profileImgeDirectory);
            //var path = FileUploadHelper.UploadFiles(filePath, contact.File, "Image_", _logger, out isUploadedSuccessfully);
            contact.ProfileImageLink = "";
            var response = await _serviceManager.UpdateContactAsync(contact);
            return response;
        }
        [HttpDelete("deleteContact")]
        public async Task<CommonResponse> DeleteContact(long Id)
        {
            var response = await _serviceManager.DeleteContact(Id);
            return response;
        }
        [HttpPost("importContacts")]
        public async Task<CommonResponse> ImportContacts(IFormFile fileToImport)
        {
            var response = new CommonResponse();
            if (!FileUploadHelper.IsFileExtensionSupported(fileToImport))
                return new CommonResponse() { Message = "Unsupported file format.Please upload .csv file.", Successful = false };

            filePath = Path.Combine("", _hostingEnvironment.ContentRootPath + importContactDirectory);
            var path = FileUploadHelper.UploadFiles(filePath, fileToImport, "ImportFile_", _logger, out isUploadedSuccessfully);
            if (!isUploadedSuccessfully)
                return new CommonResponse() { Message = "Fail to upload import file.", Successful = false };

            var importedContacts = GetDataFromImportFile(path);
            if (!(importedContacts.Count() > 0))
                return new CommonResponse() { Message = "Unable to read import file.", Successful = false };

            //start import
            response = await _serviceManager.ImportContactsAync(importedContacts);
            return response;
        }
        [HttpGet("exportContacts")]
        public FileResult ExportContacts([FromQuery] ContactParameters contactParameters)
        {
            StringBuilder sb = new StringBuilder();
            string fileName = "Contacts";
            string reportTitle = "Contacts Data";
            sb.Append(reportTitle);
            sb.Append("\r\n");
            var headerString = "FirstName,LastName,Company,Address1,Id,Address2,City,State,Zip,Email,Website,FaxNumber,MobileNumber,HomeNumber,OfficeNumber,ContactStatus,DisplayName,StartDate,EndDate,LastStatusChangeDate,LastContacted,ContactedCount,IsIImported";
            sb.Append(headerString);
            sb.Append("\r\n");
            var listResponse = _serviceManager.GetContacts(contactParameters);
            if(listResponse.items != null && listResponse.items.Count > 0)
            {
                var csString = listResponse.items.Select(i =>
                                    $"{i.FirstName},{i.LastName},{i.Company},{i.Address1},{i.Id},{i.Address2},{i.City},{i.State}," +
                                    $"{i.Zip},{i.Email},{i.Website},{i.FaxNumber},{i.MobileNumber},{i.HomeNumber}," +
                                    $"{i.OfficeNumber},{i.ContactStatus},{i.DisplayName},{i.StartDate},{i.EndDate},{i.LastStatusChangeDate},{i.LastContacted},{i.ContactedCount},{i.IsImported}").ToArray();
                for (int i = 0; i < csString.Length; i++)
                {
                    sb.Append(csString[i]);
                    sb.Append("\r\n");
                }
            }
            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", fileName);
        }
        [HttpGet("getContact")]
        public async Task<ContactDto> GetContact([FromQuery]long Id)
        {
            if (Id == 0)
                return new ContactDto() { Successful = false, Message = "Invalid contact id." };
            ContactDto response =await _serviceManager.GetContactByIdAsync(Id);
            return response;
        }
        [HttpGet("getRelatedContact")]
        public async Task<List<ContactDto>> GetRelatedContact([FromQuery]long companyId)
        {
            var response =await _serviceManager.GetRelatedContacts(companyId);
            return response;
        }
        #region Private Methods
        private List<CreateContactDto> GetDataFromImportFile(string path)
        {
            var list = new List<CreateContactDto>();
            string jsonString;
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields;
                    bool tableCreated = false;
                    while (tableCreated == false)
                    {
                        colFields = csvReader.ReadFields();
                        foreach (string column in colFields)
                        {
                            DataColumn datecolumn = new DataColumn(column);
                            datecolumn.AllowDBNull = true;
                            csvData.Columns.Add(datecolumn);
                        }
                        tableCreated = true;
                    }
                    while (!csvReader.EndOfData)
                    {
                        csvData.Rows.Add(csvReader.ReadFields());
                    }
                }
                jsonString = JsonConvert.SerializeObject(csvData);
                list = JsonConvert.DeserializeObject<List<CreateContactDto>>(jsonString);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while reading csv file.Error: " + ex.ToString());
            }
            return list;
        }
        #endregion
    }
}
