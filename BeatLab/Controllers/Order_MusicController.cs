using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeatLab;

namespace BeatLab.Controllers
{
    public class Order_MusicController : Controller
    {
        private BeatLabDBEntities db = new BeatLabDBEntities();

        // GET: Order_Music
        public ActionResult Index()
        {
            var order_Music = db.Order_Music.Include(o => o.Music).Include(o => o.User);
            return View(order_Music.ToList());
        }

        // GET: Order_Music/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Music order_Music = db.Order_Music.Find(id);
            if (order_Music == null)
            {
                return HttpNotFound();
            }
            return View(order_Music);
        }

        // GET: Order_Music/Create
        public ActionResult Create()
        {
            ViewBag.ID_Music = new SelectList(db.Music, "ID_Music", "Name_music");
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User");
            return View();
        }

        // POST: Order_Music/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Order_Music,ID_Music,ID_User")] Order_Music order_Music)
        {
            if (ModelState.IsValid)
            {
                db.Order_Music.Add(order_Music);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Music = new SelectList(db.Music, "ID_Music", "Name_music", order_Music.ID_Music);
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User", order_Music.ID_User);
            return View(order_Music);
        }

        // GET: Order_Music/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Music order_Music = db.Order_Music.Find(id);
            if (order_Music == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Music = new SelectList(db.Music, "ID_Music", "Name_music", order_Music.ID_Music);
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User", order_Music.ID_User);
            return View(order_Music);
        }

        // POST: Order_Music/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Order_Music,ID_Music,ID_User")] Order_Music order_Music)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_Music).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Music = new SelectList(db.Music, "ID_Music", "Name_music", order_Music.ID_Music);
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User", order_Music.ID_User);
            return View(order_Music);
        }

        // GET: Order_Music/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Music order_Music = db.Order_Music.Find(id);
            if (order_Music == null)
            {
                return HttpNotFound();
            }
            return View(order_Music);
        }

        // POST: Order_Music/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order_Music order_Music = db.Order_Music.Find(id);
            db.Order_Music.Remove(order_Music);
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
