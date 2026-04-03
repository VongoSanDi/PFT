namespace server.Common;

public static class QueryableExtension
{
    public static IQueryable<T> ApplyPagination<T>(
        this IQueryable<T> query,
        int pageNumber,
        int pageSize
    )
    {
        return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }
}
