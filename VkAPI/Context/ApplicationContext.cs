using Microsoft.EntityFrameworkCore;
using VkAPI.Models;

namespace VkAPI;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserGroup> UserGroups { get; set; } = null!;
    public DbSet<UserState> UserStates { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserGroup>().HasData(
            new UserGroup() { Id = 1, Code = GroupEnum.Admin,Description = "DefaultAdminDescription", },
            new UserGroup() { Id =2, Code = GroupEnum.User, Description = "DefaultUserDescription", }
            );
        modelBuilder.Entity<UserState>().HasData(
            new UserState(){ Id = 1,Code = StateEnum.Active, Description = "Active User"},
            new UserState(){ Id =2, Code = StateEnum.Deactive, Description = "Deactive User"});
    }
}