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
    public class NarratorsController : Controller
    {
        private Listen_Entities db = new Listen_Entities();

        // GET: Narrators
        public ActionResult Index()
        {
            return View(db.Narrators.ToList());
        }

        // GET: Narrators/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Narrator narrator = db.Narrators.Find(id);
            if (narrator == null)
            {
                return HttpNotFound();
            }
            return View(narrator);
        }

        // GET: Narrators/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Narrators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Narrator1")] Narrator narrator)
        {
            if (ModelState.IsValid)
            {
                db.Narrators.Add(narrator);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(narrator);
        }

        // GET: Narrators/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Narrator narrator = db.Narrators.Find(id);
            if (narrator == null)
            {
                return HttpNotFound();
            }
            return View(narrator);
        }

        // POST: Narrators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Narrator1")] Narrator narrator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(narrator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(narrator);
        }

        // GET: Narrators/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Narrator narrator = db.Narrators.Find(id);
            if (narrator == null)
            {
                return HttpNotFound();
            }
            return View(narrator);
        }

        // POST: Narrators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Narrator narrator = db.Narrators.Find(id);
            db.Narrators.Remove(narrator);
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
