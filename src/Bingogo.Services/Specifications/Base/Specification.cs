namespace Bingogo.Services.Specifications.Base;

public abstract class Specification<T>
{
    protected abstract Expression<Func<T, bool>> GetPredicate();

    public IQueryable<T> Visit(IQueryable<T> query)
    {
        return query.Where(GetPredicate());
    }

    public bool IsSatisfiedBy(T entity)
    {
        var predicate = GetPredicate().Compile();
        return predicate(entity);
    }
}