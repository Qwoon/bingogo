using Bingogo.Services.Implementations;
using Bingogo.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Bingogo.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services
            .AddAutoMapper()
            .RegisterResourceServices();

        return services;
    }

    public static IServiceCollection RegisterResourceServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IBoardService, BoardService>()
            .AddTransient<IBoardTileService, BoardTileService>();
    }

    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        var assemblyTypes = new[]
        {
            typeof(ServiceCollectionExtensions)
        };
        services.AddAutoMapper(assemblyTypes);

        return services;
    }
}
