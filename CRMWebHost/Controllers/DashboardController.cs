using AutoMapper;
using CRMContracts;
using CRMEntities.Models;
using CRMModels.Common;
using CRMModels.DataTransfersObjects;
using CRMWebHost.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRMWebHost.Controllers
{
    //[Authorize]
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : Controller
    {
        #region declarations
        private readonly IServiceManager _serviceManager;
        public DashboardController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;

        }
        #endregion
        [HttpGet("home")]
        public IActionResult Statistics()
        {
            return Ok(new { isSuccess = true, message = "welcome to home screen." });
        }
        [HttpGet("getUserColumns")]
        public async Task<List<UserColumnDto>> ListContacts([FromQuery]UserColumnRequest requestParameters)
        {
            var response =await _serviceManager.GetUserColumnsAsync(requestParameters);
            return response;
        }
        [HttpPost("addUserColumns")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<CommmonListResponse<UserColumnDto>> AddUserColumns(List<CreateUserColumnDto> request)
        {
            var response =await _serviceManager.CreateUserColumnsAsync(request);
            return response;
        }
    }
}
