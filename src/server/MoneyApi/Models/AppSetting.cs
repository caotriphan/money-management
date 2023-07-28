namespace MoneyApi.Models;

public record AppSetting
{
    public string ConnectionString { get; set; } = null!;
}
