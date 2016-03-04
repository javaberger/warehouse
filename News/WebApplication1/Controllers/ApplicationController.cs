using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ApplicationController : Controller
    {
        public ApplicationDbContext nw;
 
    public ApplicationController()
    {
        nw = new ApplicationDbContext();
        ViewData["categories"] = from c in nw.Categories
                                 select c;
    }
    }
}