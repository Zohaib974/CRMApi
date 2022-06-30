using CRMContracts;
using CRMEntities;
using CRMEntities.Models;
using CRMModels;
using CRMModels.DataTransfersObjects;
using CRMRepository.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CRMRepository
{
    public class WorkOrderRepository : RepositoryBase<WorkOrder>, IWorkOrderRepository
    {

        public WorkOrderRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public void CreateWorkOrder(WorkOrder entity) => Create(entity);
        public void CreateWorkOrders(List<WorkOrder> entities)
        {
            CreateEntities(entities);
        }
        public PagedList<WorkOrder> GetWorkOrders(WorkOrderParameters workOrderParameters, bool trackChanges = false)
        {
            var events = FindByCondition(e => !e.IsDeleted && (e.ContactId != null
                                    && e.ContactId.Value == workOrderParameters.ContactId), trackChanges)
                                    .Search(workOrderParameters.SearchBy, workOrderParameters.SearchTerm)
                                    .Sort(workOrderParameters.OrderBy)
                                    .ToList();

            return PagedList<WorkOrder>.ToPagedList(events, workOrderParameters.PageNumber, workOrderParameters.PageSize);
        }

        public async Task<WorkOrder> GetWorkOrderByIdAsync(long id, bool trackChanges)
        {
            return await FindByCondition(e => !e.IsDeleted && e.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
        }
        #region helperMethod
        public void MarkModified(WorkOrder entity,UpdateWorkOrderDto WorkOrderDto)
        {
            RepositoryContext.Entry(entity).State = EntityState.Modified;
            var entityProperties = typeof(WorkOrder).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var dtoProperties = typeof(UpdateWorkOrderDto).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach(var dProp in dtoProperties)
            {
                 var dPropValue = dProp.GetValue(WorkOrderDto);
                if(dPropValue == null)
                {
                    var entityProperty = entityProperties.FirstOrDefault(pi => pi.Name.Equals(dProp.Name, StringComparison.InvariantCultureIgnoreCase));
                    if(entityProperty == null)
                        continue;
                    var epropName = entityProperty.Name.ToString();
                    RepositoryContext.Entry(entity).Property(epropName).IsModified = false;
                }
            }
        }
        #endregion
    }
}
