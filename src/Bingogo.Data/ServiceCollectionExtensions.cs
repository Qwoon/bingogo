using Bingogo.Data.Context;
using Bingogo.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Bingogo.Data;

public static class ServiceCollectionExtensions
{
    private const string DefaultConnection = "name=MySql";

    public static IServiceCollection AddAppDb(this IServiceCollection services)
    {
        services.AddDbContextPool<DbContext, BingogoContext>(SetupMySql);

        services.AddIdentityCore<User>()
            .AddRoles<IdentityRole<long>>()
            .AddRoleManager<RoleManager<IdentityRole<long>>>()
            .AddEntityFrameworkStores<BingogoContext>();

        return services;
    }

    internal static void SetupMySql(IServiceProvider provider, DbContextOptionsBuilder builder)
    {
        SetupMySql(provider, builder, DefaultConnection);
    }

    internal static void SetupMySql(IServiceProvider provider, DbContextOptionsBuilder builder, string connection, ServerVersion version = default)
    {
        if (TryGetConnectionName(connection, out var name))
            connection = provider.GetRequiredService<IConfiguration>().GetConnectionString(name)
                         ?? throw new("Unable to resolve connection string: " + name);

        var dataSource = BuildDataSource(connection);
        builder.UseMySql(dataSource, version ?? GetServerVersion(dataSource), ConfigureMySql);

        EnableLogs(builder);
    }

    [Conditional("DEBUG")]
    private static void EnableLogs(DbContextOptionsBuilder builder)
    {
        builder
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging();
    }

    private static void UpdateDatabase(IServiceCollection services)
    {
        using var scope = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>().CreateScope();
        using var context = scope.ServiceProvider.GetService<BingogoContext>();
        context.Database.Migrate();
    }

    private static ServerVersion GetServerVersion(MySqlDataSource dataSource)
    {
        using var connection = dataSource.OpenConnection();
        return ServerVersion.AutoDetect(connection);
    }

    private static MySqlDataSource BuildDataSource(string connection)
    {
        var builder = new MySqlDataSourceBuilder(connection)
        {
            ConnectionStringBuilder =
                {
                    Pooling = false, // use connection pooling from EF Core
                    UseAffectedRows = false, // required by Pomelo.EntityFrameworkCore.MySql
                    AllowUserVariables = true, // required by Pomelo.EntityFrameworkCore.MySql
                    SslMode = MySqlSslMode.Required
                }
        };

        return builder.Build();
    }

    private static void ConfigureMySql(MySqlDbContextOptionsBuilder builder)
    {
        builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
    }

    private static bool TryGetConnectionName(ReadOnlySpan<char> connection, [MaybeNullWhen(false)] out string name)
    {
        name = default;
        var index = connection.IndexOf('=');
        if (index <= 0)
            return false;

        var key = connection[..index].Trim();
        var value = connection[(index + 1)..].Trim();
        if (!key.Equals("name", StringComparison.OrdinalIgnoreCase) || value.Contains('='))
            return false;

        name = value.ToString();
        return true;
    }
}
