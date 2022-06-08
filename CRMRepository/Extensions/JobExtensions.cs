using CRMEntities.Models;
using System.Linq;
using System.Linq.Dynamic.Core;
using CRMRepository.Extensions.Utility;

namespace CRMRepository.Extensions
{
    public static class JobExtensions
    {
        public static IQueryable<Job> Search(this IQueryable<Job> jobs, string searchBy, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm) || string.IsNullOrWhiteSpace(searchTerm))
                return jobs;
            var searchQuery = QueryBuilder.CreateSearchByQuery<Job>(searchBy,searchTerm);
            if (string.IsNullOrWhiteSpace(searchQuery))
                return jobs;
            object[] searchT = new object[1];
            searchT[0] = searchTerm;
            return jobs.Where(searchQuery, searchT);
        }
        public static IQueryable<Job> Sort(this IQueryable<Job> jobs, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return jobs.OrderBy(e => e.Id);
            var orderQuery = QueryBuilder.CreateOrderQuery<Job>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return jobs.OrderBy(e => e.Id);
            return jobs.OrderBy(orderQuery);
        }
    }
}