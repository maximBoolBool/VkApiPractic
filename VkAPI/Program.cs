using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using VkAPI;
using VkAPI.Handlers;
using VkAPI.Services.UserService;

var builder = WebApplication.CreateBuilder();
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationContext>(opt=>opt.UseNpgsql(connectionString));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddControllers();
builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions,BasicAuthenticationHandler>("BasicAuthentication",null);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyOrigin());
app.MapControllers();
app.Run();