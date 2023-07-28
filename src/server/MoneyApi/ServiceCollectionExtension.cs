using Microsoft.EntityFrameworkCore;
using MoneyApi.Db;
using MoneyApi.Models;
using MoneyApi.Services;

namespace MoneyApi;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
        });

        services.Configure<JwtSetting>(configuration.GetSection("Jwt"));
        services.AddTransient<IAuthService, AuthService>()
            .AddTransient<IUserService, UserService>()
            .AddTransient<ITransactionService, TransactionService>()
            .AddTransient<ITokenService, TokenService>()
            .AddTransient<IUserResolver, UserResolver>();

        return services;
    }
}
