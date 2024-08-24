using Microsoft.AspNetCore.Mvc;

namespace Bingogo.Services.Specifications.Implementations;

public class ResourceQuery
{
    [FromQuery(Name = Constants.Include)]
    public string[] Include { get; set; } = Array.Empty<string>();
}
