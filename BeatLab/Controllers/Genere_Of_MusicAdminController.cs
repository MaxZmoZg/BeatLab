using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeatLab.Models.Entities;

namespace BeatLab.Controllers
{
    public class Genere_Of_MusicAdminController : Controller
    {
        private BeatLabDBEntities db = new BeatLabDBEntities();

        // GET: Genere_Of_MusicAdmin
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(db.Genere_Of_Music.ToList());
        }

        // GET: Genere_Of_MusicAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genere_Of_Music genere_Of_Music = db.Genere_Of_Music.Find(id);
            if (genere_Of_Music == null)
            {
                return HttpNotFound();
            }
            return View(genere_Of_Music);
        }

        // GET: Genere_Of_MusicAdmin/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genere_Of_MusicAdmin/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Genere_Of_Music,Name_Gener_of_music")] Genere_Of_Music genere_Of_Music)
        {
            if (ModelState.IsValid)
            {
                db.Genere_Of_Music.Add(genere_Of_Music);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(genere_Of_Music);
        }

        // GET: Genere_Of_MusicAdmin/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genere_Of_Music genere_Of_Music = db.Genere_Of_Music.Find(id);
            if (genere_Of_Music == null)
            {
                return HttpNotFound();
            }
            return View(genere_Of_Music);
        }

        // POST: Genere_Of_MusicAdmin/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Genere_Of_Music,Name_Gener_of_music")] Genere_Of_Music genere_Of_Music)
        {
            if (ModelState.IsValid)
            {
                db.Entry(genere_Of_Music).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genere_Of_Music);
        }

        // GET: Genere_Of_MusicAdmin/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genere_Of_Music genere_Of_Music = db.Genere_Of_Music.Find(id);
            if (genere_Of_Music == null)
            {
                return HttpNotFound();
            }
            return View(genere_Of_Music);
        }

        // POST: Genere_Of_MusicAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Genere_Of_Music genere_Of_Music = db.Genere_Of_Music.Find(id);
            db.Genere_Of_Music.Remove(genere_Of_Music);
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
