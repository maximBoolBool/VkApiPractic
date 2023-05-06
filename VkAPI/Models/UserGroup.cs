using System.ComponentModel.DataAnnotations.Schema;

namespace VkAPI.Models;

[Table("user_group")]
public class UserGroup
{
    [Column("id")]
    public int Id { get; set; }
    [Column("code")]
    public GroupEnum Code { get; set; }
    [Column("description")]
    public string Description { get; set; }

    public List<User> User { get; set; } = new();
}