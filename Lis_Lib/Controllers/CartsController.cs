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
        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";

        public ActionResult Index()
        {
            var myCart = GetCartItems();
            
            return View(myCart); 
        }

        public static Cart GetCart(HttpContextBase context)
        {
            var cart = new Cart();
            cart.Id = GetCartId(context);
            return cart;
        }
        // Helper method to simplify shopping cart calls
        public static Cart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public ActionResult AddToCart(int id)
        {
            // Retrieve the audiobook from the database
            var addAudiobook = db.Audiobooks.Single(audiobook => audiobook.Id == id);

            // Add audiobook to the cart
            Add(addAudiobook);

            // Return user to browsing
            return RedirectToAction(actionName: "Index", controllerName: "Browse");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var cart = db.Carts.Where(
                c => c.Id == ShoppingCartId);

            string bookName = db.Carts.Single(
                item => item.Audiobook.Id == id).Audiobook.Title;

            Remove(id);

            return RedirectToAction(actionName: "Index");
        }

        public void Add(Audiobook audiobook)
        {
            // Get items in the cart
            var cartItem = db.Carts.SingleOrDefault(
                c => c.Id == ShoppingCartId
                && c.Item_Id == audiobook.Id);


            if (cartItem == null)
            {
                // Create a new item if no item exist in the cart.
                cartItem = new Cart
                {
                    Id = ShoppingCartId,
                    Item_Id = audiobook.Id,
                    Date = DateTime.Now,
                    Total = GetTotal()
                };
            }
            db.SaveChanges();
        }

        public void Remove(int id)
        {
            var cartItem = db.Carts.Single(
                cart => cart.Id == ShoppingCartId
                && cart.Item_Id == id);

            if (cartItem != null)
            {
                db.Carts.Remove(cartItem);
            }
            db.SaveChanges();
        }

        public void EmptyCart()
        {
            var cartItems = db.Carts.Where(
                cart => cart.Id == ShoppingCartId);

            foreach (var item in cartItems)
            {
                db.Carts.Remove(item);
            }

            db.SaveChanges();
        }

        public List<Cart> GetCartItems()
        {
            return db.Carts.Where(
                cart => cart.Id == ShoppingCartId).ToList();
        }

        public decimal GetTotal()
        {
            decimal? total = 0;
            foreach(var item in db.Carts.Where(c => c.Id == ShoppingCartId))
            {
                total += item.Audiobook.Price;
            }

            return total ?? decimal.Zero;
        }

        public void CreateOrder()
        {
            var cartItems = GetCartItems();

            foreach (var item in cartItems)
            {
                var order = new C_Order
                {
                    User_Id = User.Identity.GetUserId(),
                    Order_date = DateTime.Now,
                    Cart_Id = db.Carts.Where(
                        cart => cart.Id == ShoppingCartId).ToString()
                };

                db.C_Order.Add(order);
            }

            db.SaveChanges();

            EmptyCart();
        }

        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrEmpty(context.User.Identity.GetUserId()))
                {
                    // Set cart id to the user Id
                    context.Session[CartSessionKey] = context.User.Identity.GetUserId();
                }
                else
                {
                    //Generate random Guid using System.Guid
                    Guid tempCartId = Guid.NewGuid();
                    //Send tempCartId back to client as cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }

        public void MigrateCart()
        {
            var shoppingCart = db.Carts.Where(
                c => c.Id == ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.Id = User.Identity.GetUserId();
            }
            db.SaveChanges();
        }
    }

}
