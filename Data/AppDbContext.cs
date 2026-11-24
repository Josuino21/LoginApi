using LoginApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder b)
        => b.Entity<User>().HasIndex(u => u.Email).IsUnique();
}