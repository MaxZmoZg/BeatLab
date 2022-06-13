using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc; using BeatLab.Models.Entities; 
using System.Windows.Forms.DataVisualization.Charting;
using BeatLab;

namespace BeatLab.Controllers
{
    public class Price_MusicController : Controller
    {
        private readonly BeatLabDBEntities db = new BeatLabDBEntities();

        // GET: Price_Music
        public ActionResult Index()
        {
            var price_Music = db.Price_Music.Include(p => p.Music);
            return View(price_Music.ToList());
        }

        // GET: Price_Music/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Price_Music price_Music = db.Price_Music.Find(id);
            if (price_Music == null)
            {
                return HttpNotFound();
            }
            return View(price_Music);
        }

        // GET: Price_Music/Create
        public ActionResult Create()
        {
            ViewBag.ID_Music = new SelectList(db.Music, "ID_Music", "Name_music");
            return View();
        }

        // POST: Price_Music/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Price_Music,Price,Date,ID_Music")] Price_Music price_Music)
        {
            if (ModelState.IsValid)
            {
                db.Price_Music.Add(price_Music);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Music = new SelectList(db.Music, "ID_Music", "Name_music", price_Music.ID_Music);
            return View(price_Music);
        }

        // GET: Price_Music/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Price_Music price_Music = db.Price_Music.Find(id);
            if (price_Music == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Music = new SelectList(db.Music, "ID_Music", "Name_music", price_Music.ID_Music);
            return View(price_Music);
        }

        // POST: Price_Music/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Price_Music,Price,Date,ID_Music")] Price_Music price_Music)
        {
            if (ModelState.IsValid)
            {
                db.Entry(price_Music).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Music = new SelectList(db.Music, "ID_Music", "Name_music", price_Music.ID_Music);
            return View(price_Music);
        }

        // GET: Price_Music/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Price_Music price_Music = db.Price_Music.Find(id);
            if (price_Music == null)
            {
                return HttpNotFound();
            }
            return View(price_Music);
        }

        // POST: Price_Music/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Price_Music price_Music = db.Price_Music.Find(id);
            db.Price_Music.Remove(price_Music);
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
