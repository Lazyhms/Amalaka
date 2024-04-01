using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Linq.Expressions;

namespace Microsoft.EntityFrameworkCore
{
    public static partial class EntityFrameworkCoreQueryableExtensions
    {
        public static IOrderedQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> source, string propertyOrFieldName) where TSource : class
        {
            var parameter = Expression.Parameter(typeof(TSource));
            var member = Expression.PropertyOrField(parameter, propertyOrFieldName);

            return (IOrderedQueryable<TSource>)
                (source.Provider is EntityQueryProvider
                    ? source.Provider.CreateQuery<TSource>(
                        Expression.Call(
                            null,
                            QueryableMethods.OrderBy.MakeGenericMethod(parameter.Type, member.Type),
                            [source.Expression, Expression.Lambda(member, parameter)]))
                     : source);
        }

        public static IOrderedQueryable<TSource> OrderByDescending<TSource>(this IQueryable<TSource> source, string propertyOrFieldName) where TSource : class
        {
            var parameter = Expression.Parameter(typeof(TSource));
            var member = Expression.PropertyOrField(parameter, propertyOrFieldName);

            return (IOrderedQueryable<TSource>)
                (source.Provider is EntityQueryProvider
                    ? source.Provider.CreateQuery<TSource>(
                        Expression.Call(
                            null,
                            QueryableMethods.OrderByDescending.MakeGenericMethod(parameter.Type, member.Type),
                            [source.Expression, Expression.Lambda(member, parameter)]))
                     : source);
        }

        public static IOrderedQueryable<TSource> ThenBy<TSource>(this IOrderedQueryable<TSource> source, string propertyOrFieldName) where TSource : class
        {
            var parameter = Expression.Parameter(typeof(TSource));
            var member = Expression.PropertyOrField(parameter, propertyOrFieldName);

            return (IOrderedQueryable<TSource>)
                (source.Provider is EntityQueryProvider
                    ? source.Provider.CreateQuery<TSource>(
                        Expression.Call(null,
                        QueryableMethods.ThenBy.MakeGenericMethod(parameter.Type, member.Type),
                        [source.Expression, Expression.Lambda(member, parameter)]))
                    : source);
        }

        public static IOrderedQueryable<TSource> ThenByDescending<TSource>(this IOrderedQueryable<TSource> source, string propertyOrFieldName) where TSource : class
        {
            var parameter = Expression.Parameter(typeof(TSource));
            var member = Expression.PropertyOrField(parameter, propertyOrFieldName);

            return (IOrderedQueryable<TSource>)
                (source.Provider is EntityQueryProvider
                    ? source.Provider.CreateQuery<TSource>(
                        Expression.Call(null,
                        QueryableMethods.ThenByDescending.MakeGenericMethod(parameter.Type, member.Type),
                        [source.Expression, Expression.Lambda(member, parameter)]))
                    : source);
        }

        public static (int Total, List<TSource>) Pagination<TSource>(this IQueryable<TSource> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            if (count == 0)
            {
                return (0, []);
            }
            var data = source.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            return (count, data);
        }

        public static async Task<(int Total, List<TSource>)> PaginationAsync<TSource>(this IQueryable<TSource> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            if (count == 0)
            {
                return (0, []);
            }
            var data = await source.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
            return (count, data);
        }
    }
}