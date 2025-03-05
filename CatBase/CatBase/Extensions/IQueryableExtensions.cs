using CatBase.Models;

namespace CatBase.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<Cat> SearchCat(this IQueryable<Cat> query, string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(cat => cat.CatsName.Contains(searchString) ||
                cat.Breeds.Contains(searchString));
            }
            return query;
        }
    }
}
