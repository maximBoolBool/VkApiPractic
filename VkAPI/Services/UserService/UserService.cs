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
            UserGroupId = (newUser.UserGroup.Equals("User"))? 
                db.UserGroups.First(ug => ug.Code.Equals(GroupEnum.User)).Id : 
                db.UserGroups.First(ug => ug.Code.Equals(GroupEnum.Admin)).Id,
            UserStateId = db.UserStates.First(us => us.Code.Equals(StateEnum.Active)).Id,
        });
        await db.SaveChangesAsync();
        
        
        
        return true;
    }

    public async Task<bool> DeleteUserAsync(string login)
    {
        User? deleteUser = await db.Users.FirstOrDefaultAsync(u => u.Login.Equals(login));

        if (deleteUser is null)
            return false;

        deleteUser.UserStateId = db.UserStates.First(us => us.Code.Equals(StateEnum.Deactive)).Id;
        
        db.Update(deleteUser);
        
        await db.SaveChangesAsync();
        
        return true;
    }
    
    public async Task<User?> GetUserAsync(string login)
    {
        User? responseUser = await db.Users.FirstOrDefaultAsync( u => u.Login.Equals(login));

        return responseUser;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        List<User>? users = await db.Users.ToListAsync();
        return users;
    }

    public async Task<List<DtoUser>> GetFullUserInfoAsync()
    {
        var responseList = from user in db.Users
            join userGroup in db.UserGroups on user.UserGroupId equals userGroup.Id
            join userState in db.UserStates on user.UserStateId equals userState.Id
            select new DtoUser()
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                CreateDate = user.CreatedDate,
                UserGroup = userGroup.Code.ToString(),
                UserState = userState.Code.ToString(),
            };
        return await responseList.ToListAsync();
    }
}