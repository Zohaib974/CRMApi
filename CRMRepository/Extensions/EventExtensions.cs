using CRMEntities.Models;
using System.Linq;
using System.Linq.Dynamic.Core;
using CRMRepository.Extensions.Utility;

namespace CRMRepository.Extensions
{
    public static class EventExtensions
    {
        public static IQueryable<Event> Search(this IQueryable<Event> events, string searchBy, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm) || string.IsNullOrWhiteSpace(searchTerm))
                return events;
            var searchQuery = QueryBuilder.CreateSearchByQuery<Event>(searchBy,searchTerm);
            if (string.IsNullOrWhiteSpace(searchQuery))
                return events;
            object[] searchT = new object[1];
            searchT[0] = searchTerm;
            return events.Where(searchQuery, searchT);
        }
        public static IQueryable<Event> Sort(this IQueryable<Event> events, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return events.OrderBy(e => e.Id);
            var orderQuery = QueryBuilder.CreateOrderQuery<Event>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return events.OrderBy(e => e.Id);
            return events.OrderBy(orderQuery);
        }
    }
}