using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FileShare.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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