namespace MoneyApi;

public static class Authentication
{
    public static IServiceCollection AddJwt(this IServiceCollection services)
    {
        // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/security?view=aspnetcore-7.0#enabling-authentication-in-minimal-apps
        services.AddAuthentication()
            .AddJwtBearer();
        return services;
    }
}
