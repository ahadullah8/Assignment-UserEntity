using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Assignment_UserEntity.Models
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasData(
                    new User
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = "admin@gmail.com",
                        FullName = "adminUser",
                        UserName = "admin",
                        PasswordHash = "AQAAAAIAAYagAAAAEH9RlPPmwRwIk3n7qh9QlKuuLFho8eMueN1GHAR61ioSLH9KuUfJVlCpyDf1LONnrw==",
                        NormalizedEmail = "ADMIN@GMAIL.COM",
                        NormalizedUserName = "ADMIN",
                        SecurityStamp = Guid.NewGuid().ToString(),
                        ConcurrencyStamp = "cbddf737-ee1b-47e1-99b0-a38d137213cb"
                    }
                );
            base.OnModelCreating( builder );
        }
    }
}