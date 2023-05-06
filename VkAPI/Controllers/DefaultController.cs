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
    public async Task<IActionResult> AddNewUser(DtoUser newUser)
    {
        bool flag = await userWorker.AddNewUserAsync(newUser);
        return Json(new {Response = flag});
    }

    //check
    [HttpDelete]
    [Route("DeleteUser")]
    public async Task<IActionResult> DeleteUser(string login)
    {
        bool flag = await userWorker.DeleteUserAsync(login);
        return Json(new { Response = flag });
    }

    //check
    [HttpPost]
    [Route("GetUser")]
    public async Task<IActionResult> GetUser(string login)
    {
        User? user = await userWorker.GetUserAsync(login);
        return Json( new {Response = user} );
    }

    //check
    [HttpGet]
    [Route("GetAllUser")]
    public async Task<IActionResult> GetAllUser()
    {
        List<User> users = await userWorker.GetAllUsersAsync();
        return Json(users);
    }

    //check
    [HttpGet]
    [Route("GetFullUserInfo")]
    public async Task<IActionResult> GetFullUserInfo()
    {
        var responseList = await userWorker.GetFullUserInfoAsync();
        return Json(responseList);
    }

}