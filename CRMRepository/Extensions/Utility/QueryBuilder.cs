using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CRMRepository.Extensions.Utility
{
    public static class QueryBuilder
    {
        public static string CreateOrderQuery<T>(string orderByQueryString)
        {
            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();
            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;
                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi =>
               pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
                if (objectProperty == null)
                    continue;
                var direction = param.EndsWith(" asc") ? "ascending" : "descending";
                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction},");
            }
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            return orderQuery;
        }
        public static string CreateSearchByQuery<T>(string searchBy,string dataToSearch)
        {
            if (string.IsNullOrWhiteSpace(searchBy) || string.IsNullOrWhiteSpace(dataToSearch))
                return null;
            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var searchQueryBuilder = new StringBuilder(); 
            var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(searchBy, StringComparison.InvariantCultureIgnoreCase));
            if (objectProperty == null)
                return null;
            searchQueryBuilder.Append($"{objectProperty.Name.ToString()} {"==@0"},");
            var searchQuery = searchQueryBuilder.ToString().TrimEnd(',', ' ');
            return searchQuery;
        }
    }
}

