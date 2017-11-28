using Lis_Lib.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lis_Lib.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        Listen_Entities db = new Listen_Entities();

        // GET: /Checkout/Payment
        public ActionResult Payment()
        {
            return View();
        }

        // POST: /Checkout/Payment
        [HttpPost]
        public ActionResult Payment(FormCollection values)
        {
            var order = new C_Order();
            TryUpdateModel(order);

            var user = db.AspNetUsers.Single(
                u => u.Id == User.Identity.GetUserId()
                && u.Credit_Card != null);

            try
            {
                if( user.Credit_Card != null)
                {
                    order.User_Id = User.Identity.GetUserId();
                    order.Order_date = DateTime.Now;
                    order.Cart_Id = Cart.GetCartId(this.HttpContext);

                    db.C_Order.Add(order);
                    db.SaveChanges();

                    var cart = Cart.GetCart(this.HttpContext);

                    return RedirectToAction(actionName: "Complete", routeValues: new { id = order.Id });
                }
                else
                {
                    return RedirectToAction(actionName: "Create", controllerName: "Credit_Card", routeValues: null);
                }
                
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }

        public ActionResult Complete(int id)
        {
            // Validate that the customer owns this order
            bool isValid = db.C_Order.Any(
                o => o.Id == id
                && o.User_Id == User.Identity.GetUserId());

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error"); 
            }
        }
    }
}