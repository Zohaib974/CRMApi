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
    [ApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    [Route("api/workOrders")]
    [ApiController]
    public class WorkOrderController : BaseController
    {
        #region declarations
        private readonly IServiceManager _serviceManager;
        public WorkOrderController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        #endregion

        [HttpPost("addWorkOrder")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<WorkOrderDto> AddWorkOrder(CreateWorkOrderDto model)
        {
            var response = await _serviceManager.CreateWorkOrderAsync(model);
            return response;
        }
        [HttpGet("listWorkOrders")]
        public IActionResult ListWorkOrders([FromQuery] WorkOrderParameters JobParameters)
        {
            var response = _serviceManager.GetWorkOrders(JobParameters);
            return Ok(response);
        }
        [HttpPost("updateWorkOrder")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<WorkOrderDto> UpdateWorkOrder(UpdateWorkOrderDto model)
        {
            var response = await _serviceManager.UpdateWorkOrderAsync(model);
            return response;
        }
        [HttpDelete("deleteWorkOrder")]
        public async Task<CommonResponse> DeleteWorkOrder(long Id)
        {
            var response = await _serviceManager.DeleteWorkOrder(Id);
            return response;
        }
        [HttpGet("getWorkOrder")]
        public async Task<WorkOrderDto> GetWorkOrder([FromQuery] long Id)
        {
            if (Id == 0)
                return new WorkOrderDto() { Successful = false, Message = "Invalid WorkOrder id." };
            WorkOrderDto response = await _serviceManager.GetWorkOrderByIdAsync(Id);
            return response;
        }
    }
}
