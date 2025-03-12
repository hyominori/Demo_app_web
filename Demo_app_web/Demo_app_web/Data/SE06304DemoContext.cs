
using Microsoft.EntityFrameworkCore;
using Demo_app_web.Models;

namespace Demo_app_web.Data
{
    public class SE06304DemoContext:DbContext
    {
        public SE06304DemoContext(DbContextOptions<SE06304DemoContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Password = "admin",
                    Email = "admin@gmail.com"
                },
                new User
                {
                    Id = 2,
                    Username="admin2",
                    Password = "admin2",
                    Email = "admin2@gmail.com"
                },
                new User
                {
                    Id = 3,
                    Username = "user2",
                    Password = "user2",
                    Email = "user2@gmail.com"
                });
        }
    }
}
