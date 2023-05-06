using System.ComponentModel.DataAnnotations.Schema;

namespace VkAPI.Models;

[Table("user")]
public class User
{
    [Column("id")]
    public int Id { get; set; }
    [Column("login")]
    public string Login { get; set; }
    [Column("password")]
    public string Password { get; set; }
    [Column("created_date")]
    public DateOnly CreatedDate { get; set; }
    [Column("user_group_id")]
    public int UserGroupId { get; set; }
    [Column("user_state_id")]
    public int UserStateId { get; set; }
    
    [ForeignKey("UserGroupId")]
    public UserGroup userGroup { get; set; }
    [ForeignKey("UserStateId")]
    public UserState UserState { get; set; }
}