namespace VkAPI.Models;

public class DtoUser
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public DateOnly? CreateDate { get; set; }
    public string UserGroup { get; set; }
    public string? UserState { get; set; }
    
}