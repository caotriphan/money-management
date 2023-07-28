namespace MoneyApi;

public record Result
{
    public string Code { get; set; }=null!;

    public static Result Ok() => new();

    public bool Success => string.IsNullOrWhiteSpace(Code);
}

public record Result<T>(T Data) : Result
{
    public static Result<T> Default => new(default(T)!);
    public static Result<T> Ok(T data) => new(data);
    public static Result<T> Fail(string code) => new(default(T)!) { Code = code };
}
