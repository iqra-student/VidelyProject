using Microsoft.EntityFrameworkCore;

using WebApplication3.Controllers;

namespace WebApplication3.Models
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) 
        {
          
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MembershipType>().HasData(
                new MembershipType { Id = 1, Name = "Pay as You Go" },
                new MembershipType { Id = 2, Name = "Monthly" },
                new MembershipType { Id = 3, Name = "Quarterly" },
                new MembershipType { Id = 4, Name = "Annual" }
            );
        }

        public DbSet<Rental> Rentals { get; set; }



    }
}
