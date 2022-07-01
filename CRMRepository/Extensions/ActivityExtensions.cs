using CRMEntities.Models;
using System.Linq;
using System.Linq.Dynamic.Core;
using CRMRepository.Extensions.Utility;

namespace CRMRepository.Extensions
{
    public static class ActivityExtensions
    {
        public static IQueryable<Activity> Search(this IQueryable<Activity> entities, string searchBy, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm) || string.IsNullOrWhiteSpace(searchTerm))
                return entities;
            var searchQuery = QueryBuilder.CreateSearchByQuery<Activity>(searchBy,searchTerm);
            if (string.IsNullOrWhiteSpace(searchQuery))
                return entities;
            object[] searchT = new object[1];
            searchT[0] = searchTerm;
            return entities.Where(searchQuery, searchT);
        }
        public static IQueryable<Activity> Sort(this IQueryable<Activity> entities, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return entities.OrderBy(e => e.Id);
            var orderQuery = QueryBuilder.CreateOrderQuery<Activity>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return entities.OrderBy(e => e.Id);
            return entities.OrderBy(orderQuery);
        }
    }
}