using Microsoft.EntityFrameworkCore;
using MoneyApi.Db;

namespace MoneyApi;

public static class Development
{
    public static readonly string CORS_POLICY = "allow_cors";

    public static IServiceCollection EnableCORS(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(CORS_POLICY, config =>
            {
                config.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
            });
        });

        return services;
    }
}

public class DesignTimeDbContextFactory
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json", optional: true);

        var configuration = configurationBuilder.Build();
        var connectionString = configuration.GetConnectionString("DbConnection");

        var dbContextOptionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        dbContextOptionBuilder.UseSqlServer(connectionString);

        return new ApplicationDbContext(dbContextOptionBuilder.Options);
    }
}

