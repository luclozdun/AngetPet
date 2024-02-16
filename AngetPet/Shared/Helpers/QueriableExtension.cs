using System.Linq.Expressions;

namespace AngetPet.Shared.Helpers
{
    public static class QueriableExtension
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> queryable, bool condition, Expression<Func<T, bool>> predicate)
        {
            if(condition) return queryable.Where(predicate);
            return queryable;
        }
    }
}
