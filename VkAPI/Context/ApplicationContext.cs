﻿using Microsoft.EntityFrameworkCore;
using VkAPI.Models;

namespace VkAPI;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserGroup> UserGroups { get; set; } = null!;
    public DbSet<UserState> UserStates { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
    {
        Database.EnsureCreated();
    }
}