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
using System.Diagnostics;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class ArticlesController : ApplicationController
    {
        private ArticleRepository repo;

        public ArticlesController()
        {
            repo = new ArticleRepository();
        }

        // GET: Articles
        [Authorize]
        public ActionResult Index(int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            ViewBag.User = User.Identity.Name;
            return View("Index",repo.List().OrderByDescending(p=>p.ID).ToPagedList(pageNumber,pageSize));
        }

        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = repo.Get(id);
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
            return repo.CategoriesList();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,Name,Content, DateCreate, Cover, CoverType, Description, CategoriesID")] Article article, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    //string pic = System.IO.Path.GetFileName(file.FileName);
                    //string path = System.IO.Path.Combine(
                    //                       Server.MapPath("~/images/"), pic);
                    // file is uploaded

                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                        article.Cover = array;
                        article.CoverType = file.ContentType;
                    }
                    //file.SaveAs(path);
                }

                article.DateCreate = DateTime.Now;

                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                var currentUser = manager.FindById(User.Identity.GetUserId());

                article.UserID = User.Identity.GetUserId();
                repo.Create(article);//Add
                
                return RedirectToAction("Index");
            }

            return View(article);
        }

        [AllowAnonymous]
        public FileContentResult GetCoverImage(int id)
        {
            var cover = repo.List().FirstOrDefault(p => p.ID == id);
            if (cover.Cover != null && cover.CoverType !=null)
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

            var result = repo.List().First(a=>a.ID==id);

            
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
            Article article = repo.Get(id);
            ViewBag.Categories = GetOptions();
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }


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
                repo.Update(article);

                if (file != null)
                {
                    repo.App().Entry(article).Property(r => r.Cover).IsModified = true;
                    repo.App().Entry(article).Property(r => r.CoverType).IsModified = true;
                }
                else
                {
                    repo.App().Entry(article).Property(r => r.Cover).IsModified = false;
                    repo.App().Entry(article).Property(r => r.CoverType).IsModified = false;
                }
                repo.App().Entry(article).Property(r => r.UserID).IsModified = false;
                repo.App().Entry(article).Property(r => r.DateCreate).IsModified = false;
                repo.App().SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }


        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                repo.Delete(id);
                return Json(new { Success = true});
            }
            catch
            {//TODO: Log error				
                 return Json(new { Success = false });
            }
        }
    }
}
