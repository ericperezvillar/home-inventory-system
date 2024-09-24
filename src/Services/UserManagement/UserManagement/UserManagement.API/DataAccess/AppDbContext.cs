using Microsoft.EntityFrameworkCore;
using UserManagement.API.Entities;

namespace UserManagement.API.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure User table if necessary
            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}
