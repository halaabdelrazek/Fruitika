using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ecommerceProjectMVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Image { get; set; }
        public string Address { get; set; }

        public virtual List<Order> Orders { get; set; }
        public virtual List<Cart> Carts { get; set; }




    }
    public class ContextEntities : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ContextEntities() : base()
        {

        }
        public ContextEntities(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ECOMVC;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }

    }
}
