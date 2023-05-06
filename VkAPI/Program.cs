using Microsoft.EntityFrameworkCore;
using VkAPI;
using VkAPI.Services.UserService;

var builder = WebApplication.CreateBuilder();
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationContext>(opt=>opt.UseNpgsql(connectionString));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddControllers();
var app = builder.Build();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyOrigin());
app.MapControllers();
app.Run();