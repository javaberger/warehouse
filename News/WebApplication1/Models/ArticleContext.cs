using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ArticleContext: DbContext
    {
        public ArticleContext() : base("DefaultConnection")
        {
           // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ArticleContext>());
        }
      
    }
}