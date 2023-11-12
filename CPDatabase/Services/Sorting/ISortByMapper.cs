using System;
using System.Linq.Expressions;

namespace CPDatabase.Services.Sorting
{
    public interface ISortByMapper<TEntity, TInput>
    {
        Expression<Func<TEntity, object>> GetSelector(TInput input);
    }
}