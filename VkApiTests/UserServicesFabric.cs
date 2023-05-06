using Microsoft.EntityFrameworkCore;
using VkAPI;
using VkAPI.Services.UserService;

namespace VkApiTests;

public class UserServicesFabric
{
    public static UserService CreateUserService()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        var options  = optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=VkApiDbTest;Username=postgres;Password=panzer117").Options;
        ApplicationContext db = new ApplicationContext(options,"TestString");
        return new UserService(db);
    }
}