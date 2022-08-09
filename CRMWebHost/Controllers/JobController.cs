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
    [Route("api/jobs")]
    [ApiController]
    public class JobController : BaseController
    {
        #region declarations
        private readonly IServiceManager _serviceManager;
        public JobController(IServiceManager serviceManager, UserManager<User> userManager) : base(userManager)
        {
            _serviceManager = serviceManager;
        }
        #endregion

        [HttpPost("addJob")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<JobDto> AddJob(CreateJobDto Job)
        {
            var response = await _serviceManager.CreateJobAsync(Job);
            return response;
        }
        [HttpGet("listJobs")]
        public IActionResult ListJobs([FromQuery] JobParameters JobParameters)
        {
            var response = _serviceManager.GetJobs(JobParameters);
            return Ok(response);
        }
        [HttpPost("updateJob")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<JobDto> UpdateJob(UpdateJobDto Job)
        {
            var response = await _serviceManager.UpdateJobAsync(Job);
            return response;
        }
        [HttpDelete("deleteJob")]
        public async Task<CommonResponse> DeleteJob(long Id)
        {
            var response = await _serviceManager.DeleteJob(Id);
            return response;
        }
        [HttpGet("getJob")]
        public async Task<JobDto> GetJob([FromQuery] long Id)
        {
            if (Id == 0)
                return new JobDto() { Successful = false, Message = "Invalid Job id." };
            JobDto response = await _serviceManager.GetJobByIdAsync(Id);
            return response;
        }
    }
}
