using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Extensions;

namespace BookStore.Controllers
{
    public class CatalogController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Group(int groupId = 1, int page = 1)
        {
            Group group = db.Groups.Find(groupId);

            int totalcount = 0;
            foreach (var category in group.Categories)
            {
                totalcount += category.Books.Count;
            }

            PageInfo info = new PageInfo() { PageNumber = page, PageSize = 12, TotalItems = totalcount };
            GroupViewModel model = new GroupViewModel()
            {
                PageInfo = info,
                Books = (from t in db.Books
                         where t.Category.GroupId == @group.Id
                         select t).ToList().Skip((page - 1) * info.PageSize).Take(info.PageSize).ToList(),
                Group = group
            };
            return View(model);
        }

        public ActionResult Book(int id = 1)
        {
            Book book = db.Books.Find(id);
            List<Book> sim_books = book.Category.Books.Where(t=>t.Id!=id).ToList();

            BookViewModel model = new BookViewModel()
            {
                Book = book,
                SimilarBooks = sim_books
            };
            return View(model);
        }

        public PartialViewResult BookSearch(int[] selCatgs, string name)
        {
            List<Book> books = new List<Book>();
            if (!String.IsNullOrEmpty(name))
            {
                foreach (var book in db.Books)
                {
                    if (book.Name.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                        book.Author.Contains(name,StringComparison.OrdinalIgnoreCase)||
                        book.Category.Name.Contains(name,StringComparison.OrdinalIgnoreCase))
                    {
                        books.Add(book);
                    }
                }
            }
            if (books.Count == 0 && selCatgs != null)
            {
                foreach (var categoryId in selCatgs)
                {
                    books.AddRange(from b in db.Books
                                   where b.CategoryId == categoryId
                                   select b);

                }
            }
            if (books.Count == 0) return PartialView("NotFound");
            return PartialView(books);  
        }

        [HttpPost]
        public ActionResult AddComment(string CreatedComment, string UserId, int BookId)
        {
            if(!String.IsNullOrEmpty(CreatedComment))
            {
                var comment = new Comment() { Text = CreatedComment, UserId = UserId, BookId = BookId, Date = DateTime.Now };
                db.Comments.Add(comment);
                db.SaveChanges();
            }
            return RedirectToAction("Book", new { id = BookId });
        }

        public ActionResult GetComments(int BookId, int page = 1)
        {
            var comments = db.Comments.ToList().FindAll(c => c.BookId == BookId);
            comments.Reverse();

            CommentsViewModel model = new CommentsViewModel() { Comments = comments.Skip((page - 1) * 8).Take(8).ToList(),
                PageInfo = new PageInfo() { PageNumber = page, PageSize = 8, TotalItems = comments.Count }  };
            return PartialView("GetComments", model );
        }

        public JsonResult IsCommentsAvailable()
        {
            bool result = Request.IsAuthenticated;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}