using System.Linq.Expressions;
using System.Reflection;

namespace SalesManagement.Application.Common
{
    public static class QueryHelper
    {
        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, int page, int size)
        {
            return query.Skip((page - 1) * size).Take(size);
        }

        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, string order)
        {
            if (string.IsNullOrWhiteSpace(order)) return query;

            var orderingParams = order.Split(',')
                .Select(o => o.Trim().Split(' '))
                .Select(parts => new { Field = parts[0], Direction = parts.Length > 1 && parts[1].Equals("desc", StringComparison.OrdinalIgnoreCase) ? "descending" : "ascending" });

            IOrderedQueryable<T>? orderedQuery = null;

            foreach (var param in orderingParams)
            {
                var property = typeof(T).GetProperty(param.Field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (property == null) continue;

                var paramExpression = Expression.Parameter(typeof(T), "x");
                var propertyAccess = Expression.MakeMemberAccess(paramExpression, property);
                var orderByExpression = Expression.Lambda(propertyAccess, paramExpression);

                if (orderedQuery == null)
                {
                    orderedQuery = param.Direction == "descending"
                        ? Queryable.OrderByDescending(query, (dynamic)orderByExpression)
                        : Queryable.OrderBy(query, (dynamic)orderByExpression);
                }
                else
                {
                    orderedQuery = param.Direction == "descending"
                        ? Queryable.ThenByDescending(orderedQuery, (dynamic)orderByExpression)
                        : Queryable.ThenBy(orderedQuery, (dynamic)orderByExpression);
                }
            }

            return orderedQuery ?? query;
        }

        public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, Dictionary<string, string> filters)
        {
            foreach (var filter in filters)
            {
                var property = typeof(T).GetProperty(filter.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (property == null) continue;

                var paramExpression = Expression.Parameter(typeof(T), "x");
                var propertyAccess = Expression.MakeMemberAccess(paramExpression, property);
                var value = Convert.ChangeType(filter.Value, property.PropertyType);
                var equalsExpression = Expression.Equal(propertyAccess, Expression.Constant(value));

                var lambda = Expression.Lambda<Func<T, bool>>(equalsExpression, paramExpression);
                query = query.Where(lambda);
            }

            return query;
        }
    }
}
