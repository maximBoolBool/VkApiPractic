﻿using Microsoft.EntityFrameworkCore;
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
        User? responseUser = await db.Users
            .Include(u=> u.UserState)
            .Include(u => u.UserGroup)
            .FirstOrDefaultAsync( u => u.Login.Equals(login));

        if (responseUser is null)
            return null;
        
        responseUser.UserGroup = new UserGroup()
        {
            Id = responseUser.UserGroup.Id,
            Code = responseUser.UserGroup.Code,
            Description = responseUser.UserGroup.Description,
        };

        responseUser.UserState = new UserState()
        {
            Id = responseUser.UserState.Id,
            Code = responseUser.UserState.Code,
            Description = responseUser.UserState.Description,
        };
        
        return responseUser;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        List<User>? users = await db.Users.Select(p => new User()
        {
            Id = p.Id,
            Login = p.Login,
            Password = p.Password,
            CreatedDate = p.CreatedDate,
            UserGroupId = p.UserGroupId,
            UserStateId = p.UserStateId,
            UserGroup = new UserGroup()
            {
                Id = p.UserGroup.Id,
                Code = p.UserGroup.Code,
                Description = p.UserGroup.Description
            },
            UserState = new UserState()
            {
                Id = p.UserState.Id,
                Code = p.UserState.Code,
                Description = p.UserState.Description
            }
        }).ToListAsync();
        return users;
    }

    public async Task<List<DtoUser>> GetFullAllUserInfoAsync()
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

    public async Task<DtoUser?> GetFullUserAsync(string login)
    {
        User? buff = await db.Users.FirstOrDefaultAsync(u => u.Login.Equals(login));
        
        if (buff is null)
            return null;

        DtoUser response = new DtoUser()
        {
            Id = buff.Id,
            Login = buff.Login,
            Password = buff.Login,
            CreateDate = buff.CreatedDate,
            UserGroup = db.UserGroups.FirstOrDefault( ug => ug.Id.Equals(buff.UserGroupId)).Code.ToString(),
            UserState =  db.UserStates.FirstOrDefault(us => us.Id.Equals(buff.UserStateId)).Code.ToString()
        };

        return response;
    }
}