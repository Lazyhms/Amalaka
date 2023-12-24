using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Linq.Expressions;

namespace Microsoft.EntityFrameworkCore
{
    public static partial class EntityFrameworkCoreQueryableExtensions
    {
        public static IOrderedQueryable<TSource> OrderBy<TSource>(
            this IQueryable<TSource> source,
            string propertyOrFieldName) where TSource : class
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

        public static IOrderedQueryable<TSource> OrderByDescending<TSource>(
            this IQueryable<TSource> source,
            string propertyOrFieldName) where TSource : class
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

        public static IOrderedQueryable<TSource> ThenBy<TSource>(
            this IOrderedQueryable<TSource> source,
            string propertyOrFieldName) where TSource : class
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

        public static IOrderedQueryable<TSource> ThenByDescending<TSource>(
            this IOrderedQueryable<TSource> source,
            string propertyOrFieldName) where TSource : class
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
    }
}