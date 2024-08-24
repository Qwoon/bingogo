namespace Bingogo.Services.Specifications.Interfaces;

public interface ISpecification
{
    IQueryable<T> Visit<T>(IQueryable<T> query);
}

public interface ISpecification<T>
{
    IQueryable<T> Visit(IQueryable<T> query);
}
