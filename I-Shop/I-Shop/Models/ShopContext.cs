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
    }
}