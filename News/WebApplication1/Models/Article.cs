using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System;

namespace WebApplication1.Models
{
    public class Article
    {
        public int ID { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        [AllowHtml]
        [UIHint("tinymce_full_compressed")]
        [Display(Name = "Page Content")]
        public string Content { get; set; }

        public DateTime DateCreate { get; set; }

        public string CoverType { get; set; }

        public byte[] Cover { get; set; }

        public int  ViewCount { get; set; }

        public virtual int CategoriesID { get; set; }

        public virtual Categories Categories { get; set; }

        public virtual string UserID { get; set; }
    }
}