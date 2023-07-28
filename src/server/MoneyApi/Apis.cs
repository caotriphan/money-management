using MoneyApi.Extensions;
using MoneyApi.Models;
using MoneyApi.Services;

namespace MoneyApi;

public static class ApisExtension
{
    public static WebApplication UseApis(this WebApplication app)
    {
        var userApi = app.MapGroup("users").RequireAuthorization();

        userApi.MapGet("{username}", async (string username, IUserService userService) => await userService.GetUserAsync(username));
        userApi.MapPost("login", async Task<IResult> (UserModel.UserLogin request, IAuthService authService, ITokenService tokenService) =>
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return TypedResults.BadRequest("Username and password are required");
            }

            var valid = await authService.IsValidAccountAsync(request.Username, request.Password);
            if (!valid.Success)
            {
                return TypedResults.BadRequest("Invalid username or password");
            }

            var token = tokenService.GenerateToken(new TokenClaim(valid.Data.Id, valid.Data.FullName));
            return TypedResults.Ok(new Result<string>(token.Token));
        }).AllowAnonymous();
        userApi.MapPost("register", async Task<IResult> (UserModel.NewUser request, IUserService userService) =>
        {
            var result = await userService.CreateUserAsync(request);
            return TypedResults.Ok(result);
        }).AllowAnonymous();

        var transactionApi = app.MapGroup("transactions").RequireAuthorization();
        transactionApi.MapGet("", async Task<IResult> (DateTime? from, DateTime? to, TransactionFilterType? filterType, ITransactionService transactionService) =>
        {
            var fromDate = from ?? DateTime.Now.ToStartOfDate();
            var toDate = to ?? DateTime.Now.ToEndOfDate();

            return TypedResults.Ok(await transactionService.GetTransactions(fromDate, toDate, filterType));
        });
        transactionApi.MapPost("", async Task<IResult> (TransactionModel.NewTransaction request, ITransactionService transactionService) =>
        {
            var result = await transactionService.CreateTransactionAsync(request);
            return TypedResults.Ok(result);
        });

        return app;
    }
}
