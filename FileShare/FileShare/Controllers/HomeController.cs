using FileShare.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FileShare.Controllers
{
    public class HomeController : Controller
    {
        FilesStorageContext db;
        public HomeController()
        {
            db = new FilesStorageContext();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Multiple(IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var file in files)
            {
                if (file != null && file.ContentLength > 0)
                {
                    file.SaveAs(Path.Combine(Server.MapPath("/uploads"), Guid.NewGuid() + Path.GetExtension(file.FileName)));
                }
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveFile([Bind(Include = "ID, Name, Description, File, KeyAccess")] AnonymousFile afile, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        file.SaveAs(Path.Combine(Server.MapPath("/uploads"), Guid.NewGuid() + Path.GetExtension(file.FileName)));
                    }
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    foreach (var file in files)
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                        afile.File = array;
                        afile.KeyAccess = "123";
                    }
                }
                db.AnonymousFile.Add(afile);
                db.SaveChanges();
            }
            return View("Index");
        }
         


        //Random MD5 Key
        public static string GenerateMD5Uniq()
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(Guid.NewGuid().ToString());
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }


        public ActionResult Storage(string id)
        {
            return PartialView("_storage");
        }


        public ActionResult Create()
        {
            ViewBag.HashCode = GenerateMD5Uniq();
            return View("add_storage");
        }
    }
}