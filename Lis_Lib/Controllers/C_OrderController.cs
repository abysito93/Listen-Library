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
    public class C_OrderController : Controller
    {
        private Listen_Entities db = new Listen_Entities();

        // GET: C_Order
        public ActionResult Index()
        {
            var c_Order = db.C_Order.Include(c => c.AspNetUser);
            return View(c_Order.ToList());
        }

        // GET: C_Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_Order c_Order = db.C_Order.Find(id);
            if (c_Order == null)
            {
                return HttpNotFound();
            }
            return View(c_Order);
        }

        // GET: C_Order/Create
        public ActionResult Create()
        {
            ViewBag.User_Id = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: C_Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,User_Id,Order_date")] C_Order c_Order)
        {
            if (ModelState.IsValid)
            {
                db.C_Order.Add(c_Order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.User_Id = new SelectList(db.AspNetUsers, "Id", "Email", c_Order.User_Id);
            return View(c_Order);
        }

        // GET: C_Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_Order c_Order = db.C_Order.Find(id);
            if (c_Order == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_Id = new SelectList(db.AspNetUsers, "Id", "Email", c_Order.User_Id);
            return View(c_Order);
        }

        // POST: C_Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,User_Id,Order_date")] C_Order c_Order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_Order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_Id = new SelectList(db.AspNetUsers, "Id", "Email", c_Order.User_Id);
            return View(c_Order);
        }

        // GET: C_Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_Order c_Order = db.C_Order.Find(id);
            if (c_Order == null)
            {
                return HttpNotFound();
            }
            return View(c_Order);
        }

        // POST: C_Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            C_Order c_Order = db.C_Order.Find(id);
            db.C_Order.Remove(c_Order);
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
