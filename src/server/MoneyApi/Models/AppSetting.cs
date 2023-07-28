namespace MoneyApi.Models;

public record AppSetting
{
    public string ConnectionString { get; set; } = null!;
}

public record JwtSetting
{
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public string Secret { get; set; } = null!;
    public int ExpireMinutes { get; set; }
}
