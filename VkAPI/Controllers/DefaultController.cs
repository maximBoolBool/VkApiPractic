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
        Console.WriteLine($"{newUser.Login}---{newUser.Password}---{newUser.UserGroup}");
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
        return Json(new { Response = user });
    }

    //check
    [HttpGet]
    [Route("GetAllUser")]
    public async Task<IActionResult> GetAllUser()
    {
        List<User> users = await userWorker.GetAllUsersAsync();
        foreach (var VARIABLE in users)
        {
            Console.WriteLine($"{VARIABLE.Login}--{VARIABLE.Password}");
        }

        return Json(users);
    }
}