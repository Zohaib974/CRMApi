using CRMEntities.Models;
using CRMModels;
using CRMModels.DataTransfersObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRMContracts
{
    public interface IWorkOrderRepository
    {
        void CreateWorkOrder(WorkOrder entity);
        PagedList<WorkOrder> GetWorkOrders(WorkOrderParameters eventParameters, bool trackChanges);
        Task<WorkOrder> GetWorkOrderByIdAsync(long id, bool trackChanges);
        void MarkModified(WorkOrder entity, UpdateWorkOrderDto workOrderDto);
        void CreateWorkOrders(List<WorkOrder> entities);
    }
}
