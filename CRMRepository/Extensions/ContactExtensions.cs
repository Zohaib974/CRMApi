using CRMEntities.Models;
using System.Linq;
using System.Linq.Dynamic.Core;
using CRMRepository.Extensions.Utility;

namespace CRMRepository.Extensions
{
    public static class ContactExtensions
    {
        public static IQueryable<Contact> Search(this IQueryable<Contact> contacts, string searchBy, string searchTerm)
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
        public static IQueryable<Contact> Sort(this IQueryable<Contact> employees, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return employees.OrderBy(e => e.Id);
            var orderQuery = QueryBuilder.CreateOrderQuery<Contact>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return employees.OrderBy(e => e.Id);
            return employees.OrderBy(orderQuery);
        }
    }
}