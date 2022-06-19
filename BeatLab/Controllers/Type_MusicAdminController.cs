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
    public class Type_MusicAdminController : Controller
    {
        private BeatLabDBEntities db = new BeatLabDBEntities();

        // GET: Type_MusicAdmin
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(db.Type_Music.ToList());
        }

        // GET: Type_MusicAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Type_Music type_Music = db.Type_Music.Find(id);
            if (type_Music == null)
            {
                return HttpNotFound();
            }
            return View(type_Music);
        }

        // GET: Type_MusicAdmin/Create
        [Authorize (Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Type_MusicAdmin/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create([Bind(Include = "ID_Type_music,Name_Type_Music")] Type_Music type_Music)
        {
            if (ModelState.IsValid)
            {
                db.Type_Music.Add(type_Music);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(type_Music);
        }

        // GET: Type_MusicAdmin/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Type_Music type_Music = db.Type_Music.Find(id);
            if (type_Music == null)
            {
                return HttpNotFound();
            }
            return View(type_Music);
        }

        // POST: Type_MusicAdmin/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Type_music,Name_Type_Music")] Type_Music type_Music)
        {
            if (ModelState.IsValid)
            {
                db.Entry(type_Music).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(type_Music);
        }

        // GET: Type_MusicAdmin/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Type_Music type_Music = db.Type_Music.Find(id);
            if (type_Music == null)
            {
                return HttpNotFound();
            }
            return View(type_Music);
        }

        // POST: Type_MusicAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Type_Music type_Music = db.Type_Music.Find(id);
            db.Type_Music.Remove(type_Music);
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
