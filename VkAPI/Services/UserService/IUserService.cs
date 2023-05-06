using Microsoft.AspNetCore.Mvc;
using VkAPI.Models;

namespace VkAPI.Services.UserService;

public interface IUserService
{
    public Task<bool> AddNewUserAsync(DtoUser newUser);
    public Task<bool> DeleteUserAsync(string login);
    public Task<User> GetUserAsync(string login);
    public Task<List<User>> GetAllUsersAsync();
    public Task<List<DtoUser>> GetFullUserInfoAsync();
}