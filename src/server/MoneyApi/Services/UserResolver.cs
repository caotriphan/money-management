using Microsoft.Net.Http.Headers;

namespace MoneyApi.Services;

public record UserRecord(int Id, string FullName);

public interface IUserResolver
{
    UserRecord Resolve();
}

internal class UserResolver : IUserResolver
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly ITokenService tokenService;

    public UserResolver(IHttpContextAccessor httpContextAccessor, ITokenService tokenService)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.tokenService = tokenService;
    }

    public UserRecord Resolve()
    {
        var hasToken = httpContextAccessor!.HttpContext!.Request.Headers.TryGetValue(HeaderNames.Authorization, out var authorization);
        if (!hasToken)
        {
            return new UserRecord(0, null!);
        }

        var claimResult = tokenService.ReadToken(authorization.ToString()["Bearer ".Length..]);
        if (claimResult == null || !claimResult.Success)
        {
            return new UserRecord(0, null!);
        }

        return new UserRecord(claimResult.Data.Id, claimResult.Data.Name);
    }
}
