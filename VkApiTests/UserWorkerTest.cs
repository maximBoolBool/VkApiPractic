using Microsoft.EntityFrameworkCore;
using VkAPI;
using VkAPI.Models;
using VkAPI.Services.UserService;
using Xunit.Sdk;

namespace VkApiTests;

public class UserWorkerTest
{

    [Fact]
    public void AddNewUser()
    {
        //Arrange
        UserService service = UserServicesFabric.CreateUserService();

        DtoUser firstUser = new DtoUser(){Login = "1" , Password = "1" , UserGroup = "User"};
        DtoUser secondUser = new DtoUser(){Login = "1" , Password = "432", UserGroup = "User"};
        DtoUser firstAdmin = new DtoUser(){Login = "Admin", Password = "Admin" , UserGroup = "Admin"};
        DtoUser secondAdmin = new DtoUser() { Login = "kak", Password = "dsfsdf", UserGroup = "Admin" };
        
        //Act
        bool userFlag = service.AddNewUserAsync(firstUser).Result;
        bool secondUserFlag =  service.AddNewUserAsync(secondUser).Result;
        bool adminUserFlag =  service.AddNewUserAsync(firstAdmin).Result;
        bool secondAdminFlag = service.AddNewUserAsync(secondAdmin).Result;

        //Assert
        Assert.True(userFlag);
        Assert.False(secondUserFlag);
        Assert.True(adminUserFlag);
        Assert.False(secondAdminFlag);
    }
    
    [Fact]
    public void DeleteUser()
    {
        //Arrange
        UserService service = UserServicesFabric.CreateUserService();
        
        DtoUser firstUser = new DtoUser(){Login = "1" , Password = "1" , UserGroup = "User"};
        DtoUser secondUser = new DtoUser(){Login = "2" , Password = "432", UserGroup = "User"};
        DtoUser firstAdmin = new DtoUser(){Login = "Admin", Password = "Admin" , UserGroup = "Admin"};
        bool userFlag = service.AddNewUserAsync(firstUser).Result;
        bool secondUserFlag =  service.AddNewUserAsync(secondUser).Result;
        bool adminUserFlag =  service.AddNewUserAsync(firstAdmin).Result;
        
        //Act
        bool firstDeleteFlag = service.DeleteUserAsync("1").Result;
        bool secondDeleteFlag = service.DeleteUserAsync("4").Result;
        
        
        //Assert
        Assert.True(firstDeleteFlag);
        Assert.False(secondDeleteFlag);
    }

    [Fact]
    public void GetUser()
    {
        //Arrange
        UserService service = UserServicesFabric.CreateUserService();
        
        DtoUser firstUser = new DtoUser(){Login = "1" , Password = "1" , UserGroup = "User"};
        DtoUser secondUser = new DtoUser(){Login = "2" , Password = "432", UserGroup = "User"};
        DtoUser firstAdmin = new DtoUser(){Login = "Admin", Password = "Admin" , UserGroup = "Admin"};
        bool userFlag = service.AddNewUserAsync(firstUser).Result;
        bool secondUserFlag =  service.AddNewUserAsync(secondUser).Result;
        bool adminUserFlag =  service.AddNewUserAsync(firstAdmin).Result;
        
        //Act
        User? firstUserResponse = service.GetUserAsync("1").Result;
        User? secondUserResponse = service.GetUserAsync("32432").Result;


        //Assert
        Assert.Null(secondUserResponse);
        Assert.NotNull(firstUserResponse);
    }

    [Fact]
    public void GetAllUsers()
    {
        //Arrange
        UserService service = UserServicesFabric.CreateUserService();
        
        DtoUser firstUser = new DtoUser(){Login = "1" , Password = "1" , UserGroup = "User"};
        DtoUser secondUser = new DtoUser(){Login = "2" , Password = "432", UserGroup = "User"};
        DtoUser firstAdmin = new DtoUser(){Login = "Admin", Password = "Admin" , UserGroup = "Admin"};
        bool userFlag = service.AddNewUserAsync(firstUser).Result;
        bool secondUserFlag =  service.AddNewUserAsync(secondUser).Result;
        bool adminUserFlag =  service.AddNewUserAsync(firstAdmin).Result;
        
    }
    
}