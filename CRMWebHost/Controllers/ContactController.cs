using AutoMapper;
using CRMContracts;
using CRMEntities.Models;
using CRMModels;
using CRMModels.DataTransfersObjects;
using CRMWebHost.ActionFilters;
using CRMWebHost.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CRMWebHost.Controllers
{
    //[Authorize]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/contacts")]
    [ApiController]
    public class ContactController : BaseController
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IServiceManager _serviceManager;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        public ContactController(IRepositoryManager repository, ILoggerManager logger,
                                    IServiceManager serviceManager, IMapper mapper,
                                    IHostingEnvironment hostingEnvironment,IConfiguration configuration,
                                    UserManager<User> userManager)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _serviceManager = serviceManager;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _userManager = userManager;
        }
        [HttpPost("addContact")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ContactDto> AddContact([FromForm]CreateContactDto contact)
        {
            var files = HttpContext.Request.Form.Files;
            var profileImgePath = _configuration.GetSection("ProfileImageFolderPath").Value;
            var path = Path.Combine("", _hostingEnvironment.ContentRootPath + profileImgePath);
            path = UploadFiles(path, files);
            contact.ProfileImageLink = path;
            var response =await _serviceManager.CreateContactAsync(contact);
            return response;
        }
        [HttpGet("listContacts")]
        public IActionResult ListContacts([FromQuery] ContactParameters contactParameters)
        {
            var response =  _serviceManager.GetContacts(contactParameters);
            return Ok(response);
        }
        #region Private Methods
        private string UploadFiles(string path,IFormFileCollection files)
        {
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    FileInfo fi = new FileInfo(file.FileName);
                    var newFileName = "Image_" + DateTime.Now.Ticks + fi.Extension;
                    path = path + newFileName;
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
            }
            return path;
        }
        #endregion
    }
}
