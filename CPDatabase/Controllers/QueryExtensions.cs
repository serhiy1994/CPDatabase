using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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
    }
}