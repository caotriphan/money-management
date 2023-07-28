using Microsoft.EntityFrameworkCore;
using MoneyApi.Db;
using MoneyApi.Models;

namespace MoneyApi.Services;

public enum TransactionFilterType
{
    In = 1,
    Out = 2,
}

public interface ITransactionService
{
    Task<Result<TransactionModel>> CreateTransactionAsync(TransactionModel.NewTransaction request);
    Task<List<TransactionModel>> GetTransactions(DateTime from, DateTime to, TransactionFilterType? filterType);
}

internal class TransactionService : ITransactionService
{
    private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
    private readonly IUserResolver userResolver;

    public TransactionService(IDbContextFactory<ApplicationDbContext> dbContextFactory, IUserResolver userResolver)
    {
        this.dbContextFactory = dbContextFactory;
        this.userResolver = userResolver;
    }

    private ApplicationDbContext UseDb() => dbContextFactory.CreateDbContext();
    private UserRecord UseCurrentUser(bool required = true)
    {
        var user = userResolver.Resolve();
        if (user == null || user.Id == 0)
        {
            if (required)
                throw new Exception("User is not authorized");
        }

        return user!;
    }

    public async Task<Result<TransactionModel>> CreateTransactionAsync(TransactionModel.NewTransaction request)
    {
        if (request.Amount == 0)
        {
            return Result<TransactionModel>.Fail("Amount must not be 0");
        }

        var currentUser = UseCurrentUser();

        using var db = UseDb();
        var transaction = db.Transactions.Add(new Transaction
        {
            Amount = request.Amount,
            CreatedAt = DateTime.Now,
            Note = request.Note,
            TransactionDate = request.TransactionDate,
            UserId = currentUser.Id,
        }).Entity;

        await db.SaveChangesAsync();
        return new Result<TransactionModel>(new TransactionModel
        {
            Id = transaction.Id,
            Amount = transaction.Amount,
            CreatedAt = transaction.CreatedAt,
            DeletedAt = transaction.DeletedAt,
            Note = transaction.Note,
            TransactionDate = transaction.TransactionDate,
        });
    }

    public async Task<List<TransactionModel>> GetTransactions(DateTime from, DateTime to, TransactionFilterType? filterType)
    {
        using var db = UseDb();
        if (from >= to)
        {
            return new List<TransactionModel>();
        }

        var currentUser = UseCurrentUser();

        var filter = PredicateBuilder.Create<Transaction>(x => x.UserId == currentUser.Id && x.TransactionDate >= from && x.TransactionDate <= to);
        if (filterType.HasValue)
        {
            if (filterType == TransactionFilterType.In)
            {
                filter = filter.And(t => t.Amount > 0);
            }
            else
            {
                filter = filter.And(t => t.Amount < 0);
            }
        }

        var result = await db.Transactions
            .Where(filter)
            .OrderByDescending(t => t.TransactionDate)
            .Select(t => new TransactionModel
            {
                Id = t.Id,
                Amount = t.Amount,
                CreatedAt = t.CreatedAt,
                DeletedAt = t.DeletedAt,
                Note = t.Note,
                TransactionDate = t.TransactionDate,
            })
            .ToListAsync();

        return result;
    }
}
