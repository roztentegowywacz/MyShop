using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Products;
using MyShop.Core.Types;

namespace MyShop.Infrastructure.Mongo
{
    public static class Pagination
    {
        public static async Task<PagedResults<TEntity>> PaginateAsync<TEntity, TQuery>(this IMongoQueryable<TEntity> collection,
            TQuery query) where TQuery : IPagedQuery 
            => await collection.PaginateAsync<TEntity, TQuery>(query.Page, query.ResultsPerPage, query.OrderBy, query.SortOrder);

        private static async Task<PagedResults<TEntity>> PaginateAsync<TEntity, TQuery>(this IMongoQueryable<TEntity> collection,
            int page, int resultsPerPage, string orderbyKey, SortOrder sortOrder)
        {
            page = page <= 0 ? 1 : page;
            resultsPerPage = resultsPerPage <= 0 ? 10 : resultsPerPage;

            var isEmpty = await collection.AnyAsync() == false;
            if (isEmpty)
            {
                return PagedResults<TEntity>.Empty;
            }


            var totalResults = await collection.CountAsync();
            var totalPages = (int)(totalResults / resultsPerPage) + 1;  

            var skip = (page - 1) * resultsPerPage;

            var data = await collection.OrderData(orderbyKey, sortOrder).Skip(skip).Take(resultsPerPage).ToListAsync();

            return PagedResults<TEntity>.Create(data, page, resultsPerPage, totalPages, totalResults);
        }

        private static IOrderedMongoQueryable<TSource> OrderData<TSource>(this IMongoQueryable<TSource> data, 
            string filterPropertyName, SortOrder sortOrder)
        {
            var filterProperty = typeof(TSource).GetProperty(filterPropertyName, 
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase);
            if (filterProperty is null)
            {
                throw new MissingMemberException();
            }
            var filterExpression = BuildFilterExpression<TSource, object>(filterProperty);

            switch (sortOrder)
            {
                case SortOrder.desc:
                    return data.OrderByDescending(filterExpression);
                case SortOrder.asc:
                default:
                    return data.OrderBy(filterExpression);
            }
        }

        private static Expression<Func<TSource, TKey>> BuildFilterExpression<TSource, TKey>(PropertyInfo classProperty)
        {
            var parameter = Expression.Parameter(typeof(TSource), "x");
            var member = Expression.Property(parameter, classProperty.Name);
            var objectTypeMember = Expression.Convert(member, typeof(object));

            return Expression.Lambda<Func<TSource, TKey>>(objectTypeMember, parameter);
        }
    }
}