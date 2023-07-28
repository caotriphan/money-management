using MoneyApi.Models;
using MoneyApi.Services;
using MoneyApi.Extensions;
using System.ComponentModel.DataAnnotations;

namespace MoneyApi;

public static class ApisExtension
{
    public static WebApplication UseApis(this WebApplication app)
    {
        var userApi = app.MapGroup("users");

        userApi.MapGet("{username}", async (string username, IUserService userService) => await userService.GetUserAsync(username));
        userApi.MapPost("login", async Task<IResult>(UserModel.UserLogin request, IAuthService authService) =>
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return TypedResults.BadRequest("Username and password are required");
            }

            var valid = await authService.IsValidAccountAsync(request.Username, request.Password);
            return TypedResults.Ok(valid);
        });
        userApi.MapPost("register", async Task<IResult>(UserModel.NewUser request, IUserService userService) =>
        {
            var result = await userService.CreateUserAsync(request);
            return TypedResults.Ok(result);
        });

        var transactionApi = app.MapGroup("transactions");
        transactionApi.MapGet("", async Task<IResult> (ITransactionService transactionService, DateTime? from, DateTime? to, TransactionFilterType? filterType) =>
        {
            var fromDate = from ?? DateTime.Now.ToStartOfDate();
            var toDate = to ?? DateTime.Now.ToEndOfDate();

            return TypedResults.Ok(await transactionService.GetTransactions(fromDate, toDate, filterType));
        });

        return app;
    }
}
