using Bingogo.Services.Specifications.Interfaces;

namespace Bingogo.Services.Specifications;

public static class SpecificationExtensions
{
    /// <summary> Apply given specification to query if the specification is not null. </summary>  
    /// <param name="query">Query to apply specification for.</param>  
    /// <param name="specification">Specification to apply.</param>  
    public static IQueryable<T> Where<T>(this IQueryable<T> query, ISpecification<T> specification)
    {
        return specification != null
            ? specification.Visit(query)
            : query;
    }

    public static IQueryable<T> Where<T>(this IQueryable<T> query, ISpecification specification)
    {
        return specification != null
            ? specification.Visit(query)
            : query;
    }
}
