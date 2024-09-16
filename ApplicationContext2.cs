using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_007_09_09_2024
{
    public class ApplicationContext2 : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<CustomerCategory> CustomerCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=your_server;Database=MailingList;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerCategory>()
                .HasKey(cc => new { cc.Id, cc.CategoryId });

            modelBuilder.Entity<CustomerCategory>()
                .HasOne(cc => cc.Customer)
                .WithMany(c => c.CustomerCategories)
                .HasForeignKey(cc => cc.Id);

            modelBuilder.Entity<CustomerCategory>()
                .HasOne(cc => cc.Category)
                .WithMany(c => c.CustomerCategories)
                .HasForeignKey(cc => cc.Id);
        }
    }
}