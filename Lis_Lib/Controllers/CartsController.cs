using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lis_Lib.Models;
using Microsoft.AspNet.Identity;

namespace Lis_Lib.Controllers
{
    public class CartsController : Controller
    {
        Listen_Entities db = new Listen_Entities();

        public ActionResult Index()
        {
            var myCart = Cart.GetCart(this.HttpContext);

            var viewModel = new ShoppingCartViewModel
            {
                CartItems = myCart.GetCartItems(),
                CartTotal = myCart.GetTotal()
            };

            return View(viewModel); 
        }

        public ActionResult AddToCart(int id)
        {
            // Retrieve the audiobook from the database
            var addAudiobook = db.Audiobooks.Single(audiobook => audiobook.Id == id);

            var cart = Cart.GetCart(this.HttpContext);

            // Add audiobook to the cart
            cart.Add(addAudiobook);

            // Return user to browsing
            return RedirectToAction(actionName: "Index", controllerName: "Browse");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var cart = Cart.GetCart(this.HttpContext);

            var removeAudiobook = db.Audiobooks.Single(audiobook => audiobook.Id == id);

            cart.Remove(removeAudiobook);

            return RedirectToAction(actionName: "Index", controllerName: "Carts");
        }

        // End of ActionResults
        // Beggining of Carts logic
    }

}
