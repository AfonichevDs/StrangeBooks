using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class CartController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            string ret;
            try
            {
                if(Request.UrlReferrer.ToString()!= "http://localhost:2555/Cart")
                ret = Request.UrlReferrer.ToString();
                else ret = "/Home/Index";
            }
            catch (NullReferenceException)
            {
                ret = "/Home/Index";
            }
            return View(new CartViewModel() { ReturnUrl = ret });
        }

        [HttpGet]
        public ActionResult AddToCart(int BookId)
        {
            GetCart().AddItem(db.Books.Find(BookId), 1);
            return new EmptyResult();
        }

        [HttpGet]
        public ActionResult Remove(int BookId)
        {
            GetCart().RemoveItem(db.Books.Find(BookId));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateOrder()
        {
            ViewBag.Cart = GetCart();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrder([Bind(Include ="Id,FullName,Address,Phone")]Order order)
        {
            if(ModelState.IsValid)
            {
                order.TotalPrice = GetCart().TotalPrice;
                db.Orders.Add(order);
                foreach (var item in GetCart().Items)
                {
                    item.OrderId = order.Id;
                    item.Book = null;
                }
                db.OrderItems.AddRange(GetCart().Items);
                db.SaveChanges();
                GetCart().ClearCart();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Cart = GetCart();
            return View();
        }

        public ActionResult OrderItems()
        {
            return PartialView(GetCart());
        }

        public int GetBooksQuantity()
        {
            return GetCart().BookQuantity();
        }

        public ShopCart GetCart()
        {
            ShopCart Cart = (ShopCart)Session["Cart"]; 

            if(Cart == null)
            {
                Cart = new ShopCart();
                Session["Cart"] = Cart;
            }
            return Cart;
        }
    }
}