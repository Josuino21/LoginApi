using LoginApi.Data;
using Microsoft.EntityFrameworkCore;
using LoginApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Serviços
builder.Services.AddScoped<TokenService>();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseSqlite(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

// Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();