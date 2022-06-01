using CRMEntities.Models;
using System.Linq;
using System.Linq.Dynamic.Core;
using CRMRepository.Extensions.Utility;

namespace CRMRepository.Extensions
{
    public static class AttachmentExtensions
    {
        public static IQueryable<Attachment> Search(this IQueryable<Attachment> contacts, string searchBy, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm) || string.IsNullOrWhiteSpace(searchTerm))
                return contacts;
            var searchQuery = QueryBuilder.CreateSearchByQuery<Contact>(searchBy,searchTerm);
            if (string.IsNullOrWhiteSpace(searchQuery))
                return contacts;
            object[] searchT = new object[1];
            searchT[0] = searchTerm;
            return contacts.Where(searchQuery, searchT);
        }
        public static IQueryable<Attachment> Sort(this IQueryable<Attachment> employees, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return employees.OrderBy(e => e.Id);
            var orderQuery = QueryBuilder.CreateOrderQuery<Attachment>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return employees.OrderBy(e => e.Id);
            return employees.OrderBy(orderQuery);
        }
    }
}