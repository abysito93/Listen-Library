using Lis_Lib.Models;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lis_Lib.Controllers
{
    public class HomeController : Controller
    {

        private Listen_Entities db = new Listen_Entities();

        public ActionResult Index()
        {
            var audiobooks = from a in db.Audiobooks
                             select a;
            
            return View(audiobooks.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}