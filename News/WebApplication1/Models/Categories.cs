using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class Categories
    {
        public Categories()
        {
            Articles = new List<Article>();
        }
        public int ID { get; set; }

        public string Name { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}