using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class ArticleRepository:IDisposable
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public ApplicationDbContext App()
        {
            return db;
        }

        public void Create(Article a)
        {
            db.Articles.Add(a);
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
        }

        public IEnumerable<Article> List()
        {
            return db.Articles;
        }

        public void Update(Article a)
        {
            db.Entry(a).State = EntityState.Modified;
        }


        public List<Categories> CategoriesList()
        {
            return db.Categories.ToList();
        }

        public Article Get(int ?id)
        {
            return db.Articles.Find(id);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}