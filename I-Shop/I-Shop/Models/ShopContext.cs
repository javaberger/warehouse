using I_Shop.Models.ShopModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace I_Shop.Models
{
    public class ShopContext:DbContext
    {
        public ShopContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ShopContext>());
        }
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<ProductCategory> Category { get; set; }

    }
}