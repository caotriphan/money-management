using Microsoft.EntityFrameworkCore;
using MoneyApi.Db;
using MoneyApi.Extensions;

namespace MoneyApi.Services;

public record AccountValidationRecord(int Id, string FullName);

public interface IAuthService
{
    Task<Result<AccountValidationRecord>> IsValidAccountAsync(string username, string password);
}

internal class AuthService : IAuthService
{
    private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;

    public AuthService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    private ApplicationDbContext UseDb() => dbContextFactory.CreateDbContext();

    public async Task<Result<AccountValidationRecord>> IsValidAccountAsync(string username, string password)
    {
        using var db = UseDb();
        var account = await db.Users.Where(u => u.Username == username)
            .Select(u => new { u.Password, u.Id, u.FullName })
            .SingleOrDefaultAsync();

        if (account == null || !account.Password.IsHashedMatches(password))
        {
            return Result<AccountValidationRecord>.Fail("Invalid username or password");
        }

        return Result<AccountValidationRecord>.Ok(new(account.Id, account.FullName));
    }
}

