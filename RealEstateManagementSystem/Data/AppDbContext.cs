using Microsoft.EntityFrameworkCore;
using RealEstateManagementSystem.Models;

namespace RealEstateManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<RealEstateProperty> RealEstateProperty { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Your Connection String Here", options =>
                {
                    options.EnableRetryOnFailure();
                });
            }
        }
    }
}
