using CRMContracts;
using CRMModels;
using CRMModels.Common;
using CRMModels.DataTransfersObjects;
using CRMWebHost.ActionFilters;
using CRMWebHost.Base;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRMWebHost.Controllers
{
    //[Authorize]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/events")]
    [ApiController]
    public class EventController : BaseController
    {
        #region declarations
        private readonly IServiceManager _serviceManager;
        public EventController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        #endregion

        [HttpPost("addEvent")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<EventDto> AddEvent(CreateEventDto model)
        {
            var response = await _serviceManager.CreateEventAsync(model);
            return response;
        }
        [HttpGet("listEvents")]
        public IActionResult ListEvents([FromQuery] EventParameters JobParameters)
        {
            var response = _serviceManager.GetEvents(JobParameters);
            return Ok(response);
        }
        [HttpPost("updateEvent")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<EventDto> UpdateEvent(UpdateEventDto model)
        {
            var response = await _serviceManager.UpdateEventAsync(model);
            return response;
        }
        [HttpDelete("deleteEvent")]
        public async Task<CommonResponse> DeleteEvent(long Id)
        {
            var response = await _serviceManager.DeleteEvent(Id);
            return response;
        }
        [HttpGet("getEvent")]
        public async Task<EventDto> GetEvent([FromQuery] long Id)
        {
            if (Id == 0)
                return new EventDto() { Successful = false, Message = "Invalid Event id." };
            EventDto response = await _serviceManager.GetEventByIdAsync(Id);
            return response;
        }
    }
}
