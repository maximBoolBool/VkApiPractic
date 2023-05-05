using Microsoft.EntityFrameworkCore;
using VkAPI;

var builder = WebApplication.CreateBuilder();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationContext>(opt=> opt.UseNpgsql(connectionString));
builder.Services.AddControllers();
builder.Services.AddCors();


var app = builder.Build();

