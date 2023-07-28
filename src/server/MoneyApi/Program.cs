using MoneyApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCoreServices(builder.Configuration);
builder.Services.AddJwt(builder.Configuration);

builder.Services.EnableCORS();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();
var isDevelopment = app.Environment.IsDevelopment();

// Configure the HTTP request pipeline.
if (isDevelopment)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.UseApis();
app.UseCors(Development.CORS_POLICY);
app.UseAuthentication();
app.UseAuthorization();

app.Run();

