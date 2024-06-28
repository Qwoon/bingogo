using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bingogo.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureService(this IServiceCollection services, IConfiguration configuration)
    {
        // TODO: Register MySQL 
        return services;
    }
}
