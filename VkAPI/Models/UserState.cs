using System.ComponentModel.DataAnnotations.Schema;

namespace VkAPI.Models;

public class UserState
{
    [Column("id")]
    public int Id { get; set; }
    [Column("code")]
    public string Code { get; set; }
    [Column("description")]
    public string Description { get; set; }
    
    public List<User> Users { get; set; } = new();
}