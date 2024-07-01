using Bingogo.Services.Specifications.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bingogo.Services.Specifications.Implementations;

public class ListQuery : ResourceQuery, ISpecification
{
    [FromQuery(Name = Constants.Limit)]
    public int Limit { get; set; }

    [FromQuery(Name = Constants.Offset)]
    public int Offset { get; set; }

    public IQueryable<T> Visit<T>(IQueryable<T> query)
    {
        if (Offset > 0)
            query = query.Skip(Offset);

        if (Limit > 0)
            query = query.Take(Limit);

        return query;
    }
}
