using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_007_09_09_2024
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Client> Clients {  get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-C317JNM;Database=HWOrdersAndProducts;Trusted_Connection=True;TrustServerCertificate=True;");
        }

    }
}
