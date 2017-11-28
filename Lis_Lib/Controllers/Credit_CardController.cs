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
    public class Credit_CardController : Controller
    {
        private Listen_Entities db = new Listen_Entities();

        // GET: Credit_Card
        public ActionResult Index()
        {
            var credit_Card = db.Credit_Card.Include(c => c.AspNetUser);
            return View(credit_Card.ToList());
        }

        // GET: Credit_Card/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credit_Card credit_Card = db.Credit_Card.Find(id);
            if (credit_Card == null)
            {
                return HttpNotFound();
            }
            return View(credit_Card);
        }

        // GET: Credit_Card/Create
        public ActionResult Create()
        {
            ViewBag.User_Id = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Credit_Card/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,User_Id,State,City,Zip_code,Card_number")] Credit_Card credit_Card)
        {
            if (ModelState.IsValid)
            {
                db.Credit_Card.Add(credit_Card);
                db.SaveChanges();
                if (User.IsInRole("Administrator"))
                    return RedirectToAction(actionName: "Index");
                else
                    return RedirectToAction(actionName: "Index", controllerName: "Manage", routeValues: null);
            }

            ViewBag.User_Id = new SelectList(db.AspNetUsers, "Id", "Email", credit_Card.User_Id);
            return View(credit_Card);
        }

        // GET: Credit_Card/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credit_Card credit_Card = db.Credit_Card.Find(id);
            if (credit_Card == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_Id = new SelectList(db.AspNetUsers, "Id", "Email", credit_Card.User_Id);
            return View(credit_Card);
        }

        // POST: Credit_Card/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,User_Id,State,City,Zip_code,Card_number")] Credit_Card credit_Card)
        {
            if (ModelState.IsValid)
            {
                db.Entry(credit_Card).State = EntityState.Modified;
                db.SaveChanges();
                if (User.IsInRole("Administrator"))
                    return RedirectToAction(actionName: "Index");
                else
                    return RedirectToAction(actionName: "Index", controllerName: "Manage", routeValues: null);
            }
            ViewBag.User_Id = new SelectList(db.AspNetUsers, "Id", "Email", credit_Card.User_Id);
            return View(credit_Card);
        }

        // GET: Credit_Card/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credit_Card credit_Card = db.Credit_Card.Find(id);
            if (credit_Card == null)
            {
                return HttpNotFound();
            }
            return View(credit_Card);
        }

        // POST: Credit_Card/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Credit_Card credit_Card = db.Credit_Card.Find(id);
            db.Credit_Card.Remove(credit_Card);
            db.SaveChanges();
            if (User.IsInRole("Administrator"))
                return RedirectToAction(actionName: "Index");
            else
                return RedirectToAction(actionName: "Index", controllerName: "Manage", routeValues: null);
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
