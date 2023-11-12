using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CPDatabase.Models;
using CPDatabase.Services.Sorting;

namespace CPDatabase.Controllers
{
    public static class QueryExtensions
    {
        public static IQueryable<T> FilterByLeagueId<T>(this IQueryable<T> query, int id, Expression<Func<T, int>> leagueIdSelector)
        {
            return query.Where(t => leagueIdSelector.Compile()(t) == id);
        }

        public static IQueryable<T> ApplySort<T>(this IQueryable<T> query, Enum sortOrder, Dictionary<Enum, Expression<Func<T, object>>> sortSelectors)
        {
            if (sortSelectors.TryGetValue(sortOrder, out var selector)) return query.OrderBy(selector);
            return query.OrderBy(s => s);
        }

        public static IQueryable<T> ApplySorting<T, TInput>(
            this IQueryable<T> queryable, 
            ISortByMapper<T, TInput> sortByMapper,
            TInput input,
            SortOrder sortOrder = SortOrder.Asc)
        {
            var expression = sortByMapper.GetSelector(input);

            return sortOrder == SortOrder.Desc
                ? queryable.OrderByDescending(expression)
                : queryable.OrderBy(expression);
        }

        public static IQueryable<T> Paginate<T>(
            this IQueryable<T> queryable,
            PaginationData pagination)
        {
            var skip = pagination.Page * pagination.PageSize;
            var take = pagination.PageSize;

            return queryable.Skip(skip).Take(take);
        }
    }
}