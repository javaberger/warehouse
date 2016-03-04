using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;

namespace WebApplication1.Controllers
{
    public class ArticlesController : ApplicationController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Articles
        [Authorize]
        public ActionResult Index(int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            ViewBag.User = User.Identity.Name;
            return View(db.Articles.OrderByDescending(p=>p.ID).ToPagedList(pageNumber,pageSize));
        }

        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Articles/Create
        [Authorize]
        public ActionResult Create()
        {
            Article article = new Article();
            ViewBag.Categories = GetOptions();
            return View(article);
        }


        private List<Categories> GetOptions()
        {
            return db.Categories.ToList();
        }


        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,Name,Content, DateCreate, Cover, CoverType, Description, CategoriesID")] Article article, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(
                                           Server.MapPath("~/images/"), pic);
                    // file is uploaded

                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                        article.Cover = array;
                        article.CoverType = file.ContentType;
                    }
                    file.SaveAs(path);
                }

               
                String serverTime = DateTime.Now.Month.ToString("0#") + "/"
                 + DateTime.Now.Day.ToString("0#") + "/"
                 + DateTime.Now.Year.ToString();

                article.DateCreate = serverTime;

                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                var currentUser = manager.FindById(User.Identity.GetUserId());

                article.UserID = User.Identity.GetUserId();
                db.Articles.Add(article);//Add
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(article);
        }

        [AllowAnonymous]
        public FileContentResult GetCoverImage(int id)
        {
            var cover = db.Articles.FirstOrDefault(p => p.ID == id);
            if (cover != null)
            {
                return File(cover.Cover, cover.CoverType);
            }

            else
            {
                return null;
            }
        }

        [AllowAnonymous]
        public FileContentResult GetImageTop(int id)
        {

            var result = db.Articles.First(a=>a.ID==id);

            
            if (result != null)
            {
                return File(result.Cover, result.CoverType);
            }

            else
            {
                return null;
            }
        }



        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            ViewBag.Categories = GetOptions();
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID, Name,Content, CategoriesID, Description, Cover, CoverType")] Article article, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                        article.Cover = array;
                        article.CoverType = file.ContentType;
                    }
                }
                db.Entry(article).State = EntityState.Modified;

                if (file != null)
                {
                    db.Entry(article).Property(r => r.Cover).IsModified = true;
                    db.Entry(article).Property(r => r.CoverType).IsModified = true;
                }
                else
                {
                    db.Entry(article).Property(r => r.Cover).IsModified = false;
                    db.Entry(article).Property(r => r.CoverType).IsModified = false;
                }
                db.Entry(article).Property(r => r.DateCreate).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                
                return HttpNotFound();
            }
            
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
           
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
