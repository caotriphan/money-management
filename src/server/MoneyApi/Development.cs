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
