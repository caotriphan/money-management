using Microsoft.EntityFrameworkCore;
using MoneyApi.Db;
using MoneyApi.Extensions;
using MoneyApi.Models;

namespace MoneyApi.Services;

public interface IUserService
{
    Task<UserModel> GetUserAsync(int id);
    Task<UserModel> GetUserAsync(string username);
    Task<Result<int>> CreateUserAsync(UserModel.NewUser user);
}

internal class UserService : IUserService
{
    private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;

    public UserService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    private ApplicationDbContext UseDb() => dbContextFactory.CreateDbContext();

    public async Task<UserModel> GetUserAsync(int id)
    {
        using var db = UseDb();
        UserModel? user = await db.Users.Where(u => u.Id == id)
                    .Select(u => new UserModel(u))
                    .FirstOrDefaultAsync();

        return user!;
    }

    public async Task<UserModel> GetUserAsync(string username)
    {
        using var db = UseDb();
        UserModel? user = await db.Users.Where(u => u.Username == username)
                    .Select(u => new UserModel(u))
                    .FirstOrDefaultAsync();

        return user!;
    }

    public async Task<Result<int>> CreateUserAsync(UserModel.NewUser user)
    {
        using var db = UseDb();
        var userExist = await db.Users.Where(u => u.Username == user.Username).AnyAsync();
        if (userExist)
        {
            return Result<int>.Fail("Username already exist");
        }

        var entity = db.Users.Add(new()
        {
            Username = user.Username,
            FullName = user.FullName,
            Password = user.Password.Hash(),
        }).Entity;

        await db.SaveChangesAsync();

        return Result<int>.Ok(entity.Id);
    }
}

