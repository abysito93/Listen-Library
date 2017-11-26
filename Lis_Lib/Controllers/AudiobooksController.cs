using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lis_Lib.Models;

namespace Lis_Lib.Controllers
{
    public class AudiobooksController : Controller
    {
        private Listen_Entities db = new Listen_Entities();

        // GET: Audiobooks
        public ActionResult Index(string searchItem)
        {
            var audiobooks = from a in db.Audiobooks
                             select a;

            if (!String.IsNullOrEmpty(searchItem))
            {
                audiobooks = audiobooks.Where(s => s.Title.Contains(searchItem));
            }

            return View(audiobooks.ToList());
        }

        // GET: Audiobooks/Details/5
        public ActionResult Details(int? id)
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

        // GET: Audiobooks/Create
        public ActionResult Create()
        {
            ViewBag.Author_Id = new SelectList(db.Authors, "Id", "Author1");
            ViewBag.Genre_Id = new SelectList(db.Genres, "Id", "Genre1");
            ViewBag.Narrator_Id = new SelectList(db.Narrators, "Id", "Narrator1");
            ViewBag.Publisher_Id = new SelectList(db.Publishers, "Id", "Publisher1");
            return View();
        }

        // POST: Audiobooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,Price,Cost,Running_time,Abridged,Language,Release_date,Image,Author_Id,Genre_Id,Narrator_Id,Publisher_Id")] Audiobook audiobook)
        {
            if (ModelState.IsValid)
            {
                db.Audiobooks.Add(audiobook);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Author_Id = new SelectList(db.Authors, "Id", "Author1", audiobook.Author_Id);
            ViewBag.Genre_Id = new SelectList(db.Genres, "Id", "Genre1", audiobook.Genre_Id);
            ViewBag.Narrator_Id = new SelectList(db.Narrators, "Id", "Narrator1", audiobook.Narrator_Id);
            ViewBag.Publisher_Id = new SelectList(db.Publishers, "Id", "Publisher1", audiobook.Publisher_Id);
            return View(audiobook);
        }

        // GET: Audiobooks/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.Author_Id = new SelectList(db.Authors, "Id", "Author1", audiobook.Author_Id);
            ViewBag.Genre_Id = new SelectList(db.Genres, "Id", "Genre1", audiobook.Genre_Id);
            ViewBag.Narrator_Id = new SelectList(db.Narrators, "Id", "Narrator1", audiobook.Narrator_Id);
            ViewBag.Publisher_Id = new SelectList(db.Publishers, "Id", "Publisher1", audiobook.Publisher_Id);
            return View(audiobook);
        }

        // POST: Audiobooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Price,Cost,Running_time,Abridged,Language,Release_date,Image,Author_Id,Genre_Id,Narrator_Id,Publisher_Id")] Audiobook audiobook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(audiobook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Author_Id = new SelectList(db.Authors, "Id", "Author1", audiobook.Author_Id);
            ViewBag.Genre_Id = new SelectList(db.Genres, "Id", "Genre1", audiobook.Genre_Id);
            ViewBag.Narrator_Id = new SelectList(db.Narrators, "Id", "Narrator1", audiobook.Narrator_Id);
            ViewBag.Publisher_Id = new SelectList(db.Publishers, "Id", "Publisher1", audiobook.Publisher_Id);
            return View(audiobook);
        }

        // GET: Audiobooks/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Audiobooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Audiobook audiobook = db.Audiobooks.Find(id);
            db.Audiobooks.Remove(audiobook);
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
