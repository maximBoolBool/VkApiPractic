using Microsoft.EntityFrameworkCore;
using VkAPI.Models;

namespace VkAPI.Services.UserService;

public class UserService : IUserService
{
    private ApplicationContext db;

    public UserService(ApplicationContext _db)
    {
        db = _db;
    }

    public async Task<bool> AddNewUserAsync(DtoUser newUser)
    {
        User? checkNameUser = await db.Users.FirstOrDefaultAsync(u => u.Login.Equals(newUser.Login));

        if (checkNameUser is not null)
            return false;

        if (newUser.UserGroup.Equals(GroupEnum.Admin.ToString()))
        {
            User? checkAdminUser = await db.Users.FirstOrDefaultAsync(u => u.UserGroupId.Equals(1));
            if (checkAdminUser is not null)
                return false;
        }
        await db.Users.AddAsync(new User()
        {
            Login = newUser.Login,
            Password = newUser.Password,
            CreatedDate = DateOnly.FromDateTime(DateTime.Today),
            UserGroupId = (newUser.UserGroup.Equals("User"))? 2:1,
            UserStateId = 1,
        });
        await db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUserAsync(string login)
    {
        User? deleteUser = await db.Users.FirstOrDefaultAsync();

        if (deleteUser is null)
            return false;

        deleteUser.UserStateId = 2;
        
        db.Update(deleteUser);
        
        await db.SaveChangesAsync();
        
        return true;
    }
    
    public async Task<User?> GetUserAsync(string login)
    {
        User? responseUser = await db.Users.FirstAsync( u => u.Login.Equals(login));

        return responseUser;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        List<User>? users = await db.Users.ToListAsync();
        return users;
    }
}