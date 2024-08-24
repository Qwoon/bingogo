using Bingogo.Core.Data;

namespace Bingogo.Core;

public static class SoftDelete
{
    public static IEnumerable<T> WhereIsDeleted<T>(this IEnumerable<T> source, bool deleted) where T : IHistorical
        => source.Where(deleted ? SoftDelete<T>.IsDeleted : SoftDelete<T>.IsNotDeleted);

    public static IEnumerable<T> WhereIsNotDeleted<T>(this IEnumerable<T> source) where T : IHistorical
        => source.Where(SoftDelete<T>.IsNotDeleted);

    public static IQueryable<T> WhereIsDeleted<T>(this IQueryable<T> query, bool deleted) where T : IHistorical
        => query.Where(deleted ? SoftDelete<T>.IsDeletedExpression : SoftDelete<T>.IsNotDeletedExpression);

    public static IQueryable<T> WhereIsNotDeleted<T>(this IQueryable<T> query) where T : IHistorical
        => query.Where(SoftDelete<T>.IsNotDeletedExpression);
}

public static class SoftDelete<T> where T : IHistorical
{
    public static readonly Func<T, bool> IsDeleted = x => x.DeletedById != null;
    public static readonly Func<T, bool> IsNotDeleted = x => x.DeletedById == null;
    public static readonly Expression<Func<T, bool>> IsDeletedExpression = x => x.DeletedById != null;
    public static readonly Expression<Func<T, bool>> IsNotDeletedExpression = x => x.DeletedById == null;
}

