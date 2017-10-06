using BookStore.Extensions;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var bestbooks = db.Books.Where(b => b.Category.GroupId == 1).ToList();
            HomeIndexModel model = new HomeIndexModel() {LastBooks = db.Books.ToList().Skip(db.Books.Count() - 16).Take(16).Reverse().ToList() , BestBooks = bestbooks, Carousels = db.CarouselNews.ToList().GetRange(db.CarouselNews.Count() - 3, 3) };
            return View(model);
        }

        [HttpPost]
        public ActionResult GlobalSearch(string name)
        {
            List<Book> books = new List<Book>();
            foreach (var book in db.Books)
            {
                if (book.Name.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                        book.Author.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                        book.Category.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                {
                    books.Add(book);
                }
            }
            ViewBag.Name = name;
            return View(books);
        }

        public ActionResult GroupsList()
        {
            var model = db.Groups.ToList();
            return PartialView(model);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Delivery()
        {
            return View();
        }

        public ActionResult Contacts()
        {
            return View();
        }
    }
}