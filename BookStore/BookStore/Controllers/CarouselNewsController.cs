using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class CarouselNewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CarouselNews
        public ActionResult Index()
        {
            return View(db.CarouselNews.ToList());
        }

        // GET: CarouselNews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarouselNews carouselNews = db.CarouselNews.Find(id);
            if (carouselNews == null)
            {
                return HttpNotFound();
            }
            return View(carouselNews);
        }

        // GET: CarouselNews/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarouselNews/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Header,Caption")] CarouselNews carouselNews, HttpPostedFileBase upload)
        {
            string fileName = System.IO.Path.GetFileName(upload.FileName);
            if (ModelState.IsValid)
            {
                carouselNews.ImagePath = "/Content/Images/SiteImages/HomeIndex/" + fileName;
                upload.SaveAs(Server.MapPath(carouselNews.ImagePath));
                db.CarouselNews.Add(carouselNews);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carouselNews);
        }

        // GET: CarouselNews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarouselNews carouselNews = db.CarouselNews.Find(id);
            if (carouselNews == null)
            {
                return HttpNotFound();
            }
            return View(carouselNews);
        }

        // POST: CarouselNews/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Header,Caption,ImagePath")] CarouselNews carouselNews)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carouselNews).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carouselNews);
        }

        // GET: CarouselNews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarouselNews carouselNews = db.CarouselNews.Find(id);
            if (carouselNews == null)
            {
                return HttpNotFound();
            }
            return View(carouselNews);
        }

        // POST: CarouselNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarouselNews carouselNews = db.CarouselNews.Find(id);
            db.CarouselNews.Remove(carouselNews);
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
