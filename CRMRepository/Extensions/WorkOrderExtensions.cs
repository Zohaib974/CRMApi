using CRMEntities.Models;
using System.Linq;
using System.Linq.Dynamic.Core;
using CRMRepository.Extensions.Utility;

namespace CRMRepository.Extensions
{
    public static class WorkOrderExtensions
    {
        public static IQueryable<WorkOrder> Search(this IQueryable<WorkOrder> entities, string searchBy, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm) || string.IsNullOrWhiteSpace(searchTerm))
                return entities;
            var searchQuery = QueryBuilder.CreateSearchByQuery<WorkOrder>(searchBy,searchTerm);
            if (string.IsNullOrWhiteSpace(searchQuery))
                return entities;
            object[] searchT = new object[1];
            searchT[0] = searchTerm;
            return entities.Where(searchQuery, searchT);
        }
        public static IQueryable<WorkOrder> Sort(this IQueryable<WorkOrder> entities, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return entities.OrderBy(e => e.Id);
            var orderQuery = QueryBuilder.CreateOrderQuery<WorkOrder>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return entities.OrderBy(e => e.Id);
            return entities.OrderBy(orderQuery);
        }
    }
}