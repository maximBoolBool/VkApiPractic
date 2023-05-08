using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VkAPI.Models;
using VkAPI.Services.UserService;
namespace VkAPI.Controllers;

public class DefaultController : Controller
{
    private IUserService userWorker;

    public DefaultController(IUserService _userWorker)
    {
        userWorker = _userWorker;
    }
    
    //check
    [HttpPost]
    [Route("AddNewUser")]
    public async Task<JsonResult> AddNewUser(DtoUser newUser)
    {
        bool flag = await userWorker.AddNewUserAsync(newUser);
        return Json(new {Response = flag});
    }
    
    //check
    [HttpDelete]
    [Route("DeleteUser")]
    public async Task<JsonResult> DeleteUser(string login)
    {
        bool flag = await userWorker.DeleteUserAsync(login);
        return Json(new { Response = flag });
    }
    //??
    [HttpPost]
    [Route("GetUser")]
    public async Task<JsonResult> GetUser(string login)
    {
        User? user = await userWorker.GetUserAsync(login);
        return Json( new {Response = user} );
    }
    //??
    [HttpGet]
    [Route("GetAllUser")]
    public async Task<JsonResult> GetAllUser()
    {
        List<User> users = await userWorker.GetAllUsersAsync();
        return Json(users);
    }
    
    //check
    [HttpGet]
    [Route("GetFullAllUserInfo")]
    public async Task<JsonResult> GetFullAllUserInfo()
    {
        var responseList = await userWorker.GetFullAllUserInfoAsync();
        return Json(responseList);
    }
    
    //check
    [HttpPost]
    [Route("GetFullUserInfo")]
    public async Task<JsonResult> GetFullUserInfo(string login)
    {
        var responseUser = await userWorker.GetFullUserAsync(login);
        return Json(responseUser);
    }
}