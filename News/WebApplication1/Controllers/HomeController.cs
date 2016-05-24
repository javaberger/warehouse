using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Diagnostics;
using System.Globalization;

namespace WebApplication1.Controllers
{
    public class HomeController : ApplicationController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public static int Counter { get; set; }

        [HttpGet]
        public ActionResult AjaxMostPopular()
        {
            var model = (from a in db.Articles orderby a.ViewCount descending select a).Take(3);
            Counter = 2;
            return PartialView("_AjaxMostPopular", model);
        }

        [HttpPost]
        [ActionName("AjaxMostPopular")]
        public ActionResult PartialGridPost(int count = 3)
        {
            count =  count + Counter; 
            var model = (from a in db.Articles orderby a.ViewCount descending select a).Take(count);
            Counter += 2;
            return PartialView("_AjaxMostPopular", model);
        }

        [HttpGet]
        public PartialViewResult Create()
        {
            return PartialView("_Login", new LoginViewModel());
        }


        public ActionResult Index(int? page , string category, string search)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            var now = System.DateTime.Now;

            var MondayDate = now.AddDays(-((now.DayOfWeek - System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek + 7) % 7)).Date;

            //TopNews(3)
            ViewBag.TopThree = (from a in db.Articles where a.DateCreate >= MondayDate && a.DateCreate <= now orderby a.ViewCount descending select a).Take(3);

            ViewBag.WeekCount = (from a in db.Articles where a.DateCreate >= MondayDate && a.DateCreate <= now orderby a.ViewCount descending select a).Count();
            //Categories
            ViewBag.Categories = db.Categories.ToList();

            //Ajax model
            ViewBag.Ajax = (from a in db.Articles orderby a.ViewCount descending select a).Take(2);

            //Search
            if (search != null)
            {
                var query = from article in db.Articles where article.Name.Contains(search) || 
                            article.Description.Contains(search) || 
                            article.Content.Contains(search) orderby article.ID descending select article;

                var Users = from news in db.Articles
                              join users in db.Users on news.UserID equals users.Id
                              where news.Name.Contains(search) ||
                                    news.Description.Contains(search) ||
                                    news.Content.Contains(search)
                                        orderby news.ID descending
                              select users;
                ViewBag.Query = Users.ToList().ToPagedList(pageNumber, pageSize);
                
                ViewBag.SearchString = "По вашому запиту '"+search+"' знайдено ["+query.Count()+"] статті";
                return View(query.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                /*None*/
            }
            //CategorySort
            if (category != null)
            {
                var query = from articless in db.Articles where articless.Categories.Name == category orderby articless.ID descending select articless;
                ViewBag.SearchString = "По вашому запиту '"+category+"' знайдено [" + query.Count() + "] статті";

                var newsCat = from news in db.Articles join users in db.Users on news.UserID equals users.Id where news.Categories.Name == category orderby news.ID descending select users;
                if (query != null)
                {
                    ViewBag.Query = newsCat.ToList().ToPagedList(pageNumber, pageSize);
                }
                return View(query.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                            
                //Main model
                var query = from news in db.Articles join users in db.Users on news.UserID equals users.Id orderby news.ID descending select users;
                if (query != null)
                {
                    ViewBag.Query = query.ToList().ToPagedList(pageNumber, pageSize);
                }
                var articles = db.Articles.OrderByDescending(p=>p.ID).ToList();
                return View(articles.ToPagedList(pageNumber, pageSize));
            }
        }

        [AllowAnonymous]
        public FileContentResult GetUserImage(string id)
        {
            var cover = db.Users.FirstOrDefault(p => p.Id == id);
            if (cover.Cover != null && cover.CoverType!=null)
            {
                return File(cover.Cover, cover.CoverType);
            }
            else
            {
                string imageFile = System.Web.HttpContext.Current.Server.MapPath("~/Content/user.png");
                var srcImage = Image.FromFile(imageFile);
                var stream = new MemoryStream();
                srcImage.Save(stream, ImageFormat.Png);
                return File(stream.ToArray(), "image/png");
            }
        }


        [AllowAnonymous]
        public ActionResult news([Bind(Include = "ViewCount")] int? id)
        {
            Article article = db.Articles.Find(id);
                
            if (article != null)
            {
                //TopNews(3)
                ViewBag.TopThree = (from a in db.Articles orderby a.ViewCount descending select a).Take(3);

                //Categories
                ViewBag.Categories = db.Categories.ToList();

                article.ViewCount += 1;
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return View("news", article);
            }
            else
            {
                return null;
            }
        }

        [Authorize]
        public ActionResult Admin()
        {
            ViewBag.admin = "Only Admin Success";
            return View("Admin");
        }


        public ActionResult Error()
        {
            return View("Lockout");
        }
    }
}