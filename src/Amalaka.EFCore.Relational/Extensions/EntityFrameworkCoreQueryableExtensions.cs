using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Linq.Expressions;

namespace Microsoft.EntityFrameworkCore
{
    public static partial class EntityFrameworkCoreQueryableExtensions
    {
        public static Task<bool> AnyAsync<TSource>(
            this IQueryable<TSource> source,
            bool condition,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default)
            => condition ? source.AnyAsync(predicate, cancellationToken) : source.AnyAsync(cancellationToken);

        public static Task<int> CountAsync<TSource>(
            this IQueryable<TSource> source,
            bool condition,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default)
            => condition ? source.CountAsync(predicate, cancellationToken) : source.CountAsync(cancellationToken);

        public static Task<TSource> SingleAsync<TSource>(
            this IQueryable<TSource> source,
            bool condition,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default)
            => condition ? source.SingleAsync(predicate, cancellationToken) : source.SingleAsync(cancellationToken);

        public static Task<TSource?> SingleOrDefaultAsync<TSource>(
            this IQueryable<TSource> source,
            bool condition,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default)
            => condition ? source.SingleOrDefaultAsync(predicate, cancellationToken) : source.SingleOrDefaultAsync(cancellationToken);

        public static Task<TSource> FirstAsync<TSource>(
            this IQueryable<TSource> source,
            bool condition,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default)
            => condition ? source.FirstAsync(predicate, cancellationToken) : source.FirstAsync(cancellationToken);

        public static Task<TSource?> FirstOrDefaultAsync<TSource>(
            this IQueryable<TSource> source,
            bool condition,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default)
            => condition ? source.FirstOrDefaultAsync(predicate, cancellationToken) : source.FirstOrDefaultAsync(cancellationToken);

        public static readonly MethodInfo IgnoreQueryFilterMethodInfo
            = typeof(EntityFrameworkCoreQueryableExtensions)
                .GetTypeInfo().GetDeclaredMethods(nameof(IgnoreQueryFilter))
                .Single(mi => mi.GetParameters().Any(
                    pi => pi.Name == "filter" && pi.ParameterType == typeof(string[])))!;

        public static IQueryable<TEntity> IgnoreQueryFilter<TEntity>(this IQueryable<TEntity> source, [NotParameterized] params string[] filter) where TEntity : class
        {
            return
                source.Provider is EntityQueryProvider
                    ? source.Provider.CreateQuery<TEntity>(
                        Expression.Call(
                            instance: null,
                            method: IgnoreQueryFilterMethodInfo.MakeGenericMethod(typeof(TEntity)),
                            arg0: source.Expression,
                            arg1: Expression.Constant(filter)))
                    : source;
        }

        #region OrderBy

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

        public static IOrderedQueryable<TSource> OrderBy<TSource>(
            this IQueryable<TSource> source,
            bool condition,
            string propertyOrFieldName) where TSource : class
        {
            return condition ? source.OrderBy(propertyOrFieldName) : (IOrderedQueryable<TSource>)source;
        }

        public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(
            this IQueryable<TSource> source,
            bool condition,
            Expression<Func<TSource, TKey>> keySelector) where TSource : class
        {
            return condition ? source.OrderBy(keySelector) : (IOrderedQueryable<TSource>)source;
        }

        #endregion

        #region ThenBy

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

        public static IOrderedQueryable<TSource> ThenBy<TSource>(
            this IOrderedQueryable<TSource> source,
            bool condition,
            string propertyOrFieldName) where TSource : class
        {
            return condition ? source.ThenBy(propertyOrFieldName) : source;
        }

        public static IOrderedQueryable<TSource> ThenBy<TSource, TKey>(
            this IOrderedQueryable<TSource> source,
            bool condition,
            Expression<Func<TSource, TKey>> keySelector) where TSource : class
        {
            return condition ? source.ThenBy(keySelector) : source;
        }

        #endregion

        #region OrderByDescending

        public static IOrderedQueryable<TSource> OrderByDescending<TSource>(
            this IQueryable<TSource> source,
            string propertyOrFieldName) where TSource : class
        {
            var parameter = Expression.Parameter(typeof(TSource));
            var member = Expression.PropertyOrField(parameter, propertyOrFieldName);

            return (IOrderedQueryable<TSource>)
                (source.Provider is EntityQueryProvider
                    ? source.Provider.CreateQuery<TSource>(
                        Expression.Call(null,
                        QueryableMethods.OrderByDescending.MakeGenericMethod(parameter.Type, member.Type),
                        [source.Expression, Expression.Lambda(member, parameter)]))
                    : source);
        }

        public static IOrderedQueryable<TSource> OrderByDescending<TSource>(
            this IQueryable<TSource> source,
            bool condition,
            string propertyOrFieldName) where TSource : class
        {
            return condition ? source.OrderByDescending(propertyOrFieldName) : (IOrderedQueryable<TSource>)source;
        }

        public static IOrderedQueryable<TSource> OrderByDescending<TSource, TKey>(
            this IQueryable<TSource> source,
            bool condition,
            Expression<Func<TSource, TKey>> keySelector) where TSource : class
        {
            return condition ? source.OrderByDescending(keySelector) : (IOrderedQueryable<TSource>)source;
        }

        #endregion

        #region ThenByDescending

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

        public static IOrderedQueryable<TSource> ThenByDescending<TSource>(
            this IOrderedQueryable<TSource> source,
            bool condition,
            string propertyOrFieldName) where TSource : class
        {
            return condition ? source.ThenByDescending(propertyOrFieldName) : source;
        }

        public static IOrderedQueryable<TSource> ThenByDescending<TSource, TKey>(
            this IOrderedQueryable<TSource> source,
            bool condition,
            Expression<Func<TSource, TKey>> keySelector) where TSource : class
        {
            return condition ? source.ThenByDescending(keySelector) : source;
        }

        #endregion

        #region ToPagedList

        public static DbPagination<TSource> ToPagedList<TSource>(
            this IQueryable<TSource> source,
            int pageIndex,
            int pageSize) where TSource : class
        {
            var dbPagination = new DbPagination<TSource>(pageIndex, pageSize)
            {
                TotalCount = source.AsNoTracking().Count()
            };
            if (dbPagination.TotalCount > 0)
            {
                dbPagination.PageData = [.. source.AsNoTracking().Skip(dbPagination.PageSize * (dbPagination.PageIndex - 1)).Take(dbPagination.PageSize)];
            }
            return dbPagination;
        }

        public static DbPagination<TSource> ToPagedList<TSource>(
           this IQueryable<TSource> source,
           Expression<Func<TSource, bool>> predicate,
           int pageIndex,
           int pageSize) where TSource : class
        {
            return source.Where(predicate).ToPagedList(pageIndex, pageSize);
        }

        public static DbPagination<TSource> ToPagedList<TSource>(
           this IQueryable<TSource> source,
           string propertyOrFieldName,
           int pageIndex = 1,
           int pageSize = 10) where TSource : class
        {
            return source.OrderBy(propertyOrFieldName).ToPagedList(pageIndex, pageSize);
        }

        public static DbPagination<TSource> ToPagedList<TSource, TKey>(
           this IQueryable<TSource> source,
           Expression<Func<TSource, TKey>> keySelector,
           int pageIndex = 1,
           int pageSize = 10) where TSource : class
        {
            return source.OrderBy(keySelector).ToPagedList(pageIndex, pageSize);
        }

        public static DbPagination<TSource> ToPagedList<TSource>(
           this IQueryable<TSource> source,
           Expression<Func<TSource, bool>> predicate,
           string propertyOrFieldName,
           int pageIndex = 1,
           int pageSize = 10) where TSource : class
        {
            return source.Where(predicate).OrderBy(propertyOrFieldName).ToPagedList(pageIndex, pageSize);
        }

        public static DbPagination<TSource> ToPagedList<TSource, TKey>(
           this IQueryable<TSource> source,
           Expression<Func<TSource, bool>> predicate,
           Expression<Func<TSource, TKey>> keySelector,
           int pageIndex = 1,
           int pageSize = 10) where TSource : class
        {
            return source.Where(predicate).OrderBy(keySelector).ToPagedList(pageIndex, pageSize);
        }

        #endregion

        #region ToDescendingPagedList

        public static DbPagination<TSource> ToDescendingPagedList<TSource>(
           this IQueryable<TSource> source,
           string propertyOrFieldName,
           int pageIndex = 1,
           int pageSize = 10) where TSource : class
        {
            return source.OrderByDescending(propertyOrFieldName).ToPagedList(pageIndex, pageSize);
        }

        public static DbPagination<TSource> ToDescendingPagedList<TSource, TKey>(
           this IQueryable<TSource> source,
           Expression<Func<TSource, TKey>> keySelector,
           int pageIndex = 1,
           int pageSize = 10) where TSource : class
        {
            return source.OrderByDescending(keySelector).ToPagedList(pageIndex, pageSize);
        }

        public static DbPagination<TSource> ToDescendingPagedList<TSource>(
           this IQueryable<TSource> source,
           Expression<Func<TSource, bool>> predicate,
           string propertyOrFieldName,
           int pageIndex = 1,
           int pageSize = 10) where TSource : class
        {
            return source.Where(predicate).OrderByDescending(propertyOrFieldName).ToPagedList(pageIndex, pageSize);
        }

        public static DbPagination<TSource> ToDescendingPagedList<TSource, TKey>(
           this IQueryable<TSource> source,
           Expression<Func<TSource, bool>> predicate,
           Expression<Func<TSource, TKey>> keySelector,
           int pageIndex = 1,
           int pageSize = 10) where TSource : class
        {
            return source.Where(predicate).OrderByDescending(keySelector).ToPagedList(pageIndex, pageSize);
        }

        #endregion

        #region ToPagedListAsync

        public static async Task<DbPagination<TSource>> ToPagedListAsync<TSource>(
            this IQueryable<TSource> source,
            int pageIndex = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default) where TSource : class
        {
            var dbPagination = new DbPagination<TSource>(pageIndex, pageSize)
            {
                TotalCount = await source.AsNoTracking().CountAsync(cancellationToken)
            };
            if (dbPagination.TotalCount > 0)
            {
                dbPagination.PageData = await source.AsNoTracking().Skip(dbPagination.PageSize * (dbPagination.PageIndex - 1)).Take(dbPagination.PageSize).ToListAsync(cancellationToken);
            }
            return dbPagination;
        }

        public static async Task<DbPagination<TSource>> ToPagedListAsync<TSource>(
           this IQueryable<TSource> source,
           Expression<Func<TSource, bool>> predicate,
           int pageIndex = 1,
           int pageSize = 10,
           CancellationToken cancellationToken = default) where TSource : class
        {
            return await source.Where(predicate).ToPagedListAsync(pageIndex, pageSize, cancellationToken);
        }

        public static async Task<DbPagination<TSource>> ToPagedListAsync<TSource>(
           this IQueryable<TSource> source,
           string propertyOrFieldName,
           int pageIndex = 1,
           int pageSize = 10,
           CancellationToken cancellationToken = default) where TSource : class
        {
            return await source.OrderBy(propertyOrFieldName).ToPagedListAsync(pageIndex, pageSize, cancellationToken);
        }

        public static async Task<DbPagination<TSource>> ToPagedListAsync<TSource, TKey>(
           this IQueryable<TSource> source,
           Expression<Func<TSource, TKey>> keySelector,
           int pageIndex = 1,
           int pageSize = 10,
           CancellationToken cancellationToken = default) where TSource : class
        {
            return await source.OrderBy(keySelector).ToPagedListAsync(pageIndex, pageSize, cancellationToken);
        }

        public static async Task<DbPagination<TSource>> ToPagedListAsync<TSource>(
           this IQueryable<TSource> source,
           Expression<Func<TSource, bool>> predicate,
           string propertyOrFieldName,
           int pageIndex = 1,
           int pageSize = 10,
           CancellationToken cancellationToken = default) where TSource : class
        {
            return await source.Where(predicate).OrderBy(propertyOrFieldName).ToPagedListAsync(pageIndex, pageSize, cancellationToken);
        }

        public static async Task<DbPagination<TSource>> ToPagedListAsync<TSource, TKey>(
           this IQueryable<TSource> source,
           Expression<Func<TSource, bool>> predicate,
           Expression<Func<TSource, TKey>> keySelector,
           int pageIndex = 1,
           int pageSize = 10,
           CancellationToken cancellationToken = default) where TSource : class
        {
            return await source.Where(predicate).OrderBy(keySelector).ToPagedListAsync(pageIndex, pageSize, cancellationToken);
        }

        #endregion

        #region ToDescendingPagedListAsync

        public static async Task<DbPagination<TSource>> ToDescendingPagedListAsync<TSource>(
           this IQueryable<TSource> source,
           string propertyOrFieldName,
           int pageIndex = 1,
           int pageSize = 10,
           CancellationToken cancellationToken = default) where TSource : class
        {
            return await source.OrderByDescending(propertyOrFieldName).ToPagedListAsync(pageIndex, pageSize, cancellationToken);
        }

        public static async Task<DbPagination<TSource>> ToDescendingPagedListAsync<TSource, TKey>(
           this IQueryable<TSource> source,
           Expression<Func<TSource, TKey>> keySelector,
           int pageIndex = 1,
           int pageSize = 10,
           CancellationToken cancellationToken = default) where TSource : class
        {
            return await source.OrderByDescending(keySelector).ToPagedListAsync(pageIndex, pageSize, cancellationToken);
        }

        public static async Task<DbPagination<TSource>> ToDescendingPagedListAsync<TSource>(
           this IQueryable<TSource> source,
           Expression<Func<TSource, bool>> predicate,
           string propertyOrFieldName,
           int pageIndex = 1,
           int pageSize = 10,
           CancellationToken cancellationToken = default) where TSource : class
        {
            return await source.Where(predicate).OrderByDescending(propertyOrFieldName).ToPagedListAsync(pageIndex, pageSize, cancellationToken);
        }

        public static async Task<DbPagination<TSource>> ToDescendingPagedListAsync<TSource, TKey>(
           this IQueryable<TSource> source,
           Expression<Func<TSource, bool>> predicate,
           Expression<Func<TSource, TKey>> keySelector,
           int pageIndex = 1,
           int pageSize = 10,
           CancellationToken cancellationToken = default) where TSource : class
        {
            return await source.Where(predicate).OrderByDescending(keySelector).ToPagedListAsync(pageIndex, pageSize, cancellationToken);
        }

        #endregion
    }
}