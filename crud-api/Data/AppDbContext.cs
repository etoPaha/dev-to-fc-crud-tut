using crud_api.Models;
using Microsoft.EntityFrameworkCore;

namespace crud_api.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
}