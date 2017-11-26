using Lis_Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace Lis_Lib.Controllers
{
    public class BrowseController : Controller
    {

        private Listen_Entities db = new Listen_Entities();
        // GET: Browse
        public ActionResult Index(string searchItem, 
            bool checkAuthorAsc = false, bool checkAuthorDesc = false, 
            bool checkPublisherAsc = false, bool checkPublisherDesc = false,
            bool checkNarratorAsc = false, bool checkNarratorDesc = false, 
            bool checkPriceAsc = false, bool checkPriceDesc = false, 
            bool checkDateAsc = false, bool checkDateDesc = false, 
            bool checkTitleAsc = false, bool checkTitleDesc = false)
        {
            var audiobooks = from a in db.Audiobooks
                             select a;

            if (!String.IsNullOrEmpty(searchItem))
            {
                audiobooks = audiobooks.Where(s => s.Title.Contains(searchItem) || s.Author.Author1.Contains(searchItem) || s.Narrator.Narrator1.Contains(searchItem) || s.Publisher.Publisher1.Contains(searchItem));
            }

            if (checkAuthorAsc == true)
                return View(audiobooks.OrderBy(a => a.Author.Author1).ToList());
            if (checkAuthorDesc == true)
                return View(audiobooks.OrderByDescending(a => a.Author.Author1).ToList());
            if (checkNarratorAsc == true)
                return View(audiobooks.OrderBy(n => n.Narrator.Narrator1).ToList());
            if (checkNarratorDesc == true)
                return View(audiobooks.OrderByDescending(n => n.Narrator.Narrator1).ToList());
            if (checkPublisherAsc == true)
                return View(audiobooks.OrderBy(p => p.Publisher.Publisher1).ToList());
            if (checkPublisherDesc == true)
                return View(audiobooks.OrderBy(p => p.Publisher.Publisher1).ToList());
            if (checkPriceAsc == true)
                return View(audiobooks.OrderBy(pr => pr.Price).ToList());
            if (checkPriceDesc == true)
                return View(audiobooks.OrderByDescending(pr => pr.Price).ToList());
            if (checkDateAsc == true)
                return View(audiobooks.OrderBy(d => d.Release_date).ToList());
            if (checkDateDesc == true)
                return View(audiobooks.OrderByDescending(d => d.Release_date).ToList());
            if (checkTitleAsc == true)
                return View(audiobooks.OrderBy(t => t.Title).ToList());
            if (checkTitleDesc == true)
                return View(audiobooks.OrderByDescending(t => t.Title).ToList());

            return View(audiobooks.ToList());
        }

        public ActionResult ViewItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Audiobook audiobook = db.Audiobooks.Find(id);
            if (audiobook == null)
            {
                return HttpNotFound();
            }
            return View(audiobook);
        }
    }
}