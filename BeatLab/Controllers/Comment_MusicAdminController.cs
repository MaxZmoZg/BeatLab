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
    public class Comment_MusicAdminController : Controller
    {
        private BeatLabDBEntities db = new BeatLabDBEntities();

        // GET: Comment_MusicAdmin
        public ActionResult Index()
        {
            var comment_Music = db.Comment_Music.Include(c => c.Music).Include(c => c.User);
            return View(comment_Music.ToList());
        }

        // GET: Comment_MusicAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment_Music comment_Music = db.Comment_Music.Find(id);
            if (comment_Music == null)
            {
                return HttpNotFound();
            }
            return View(comment_Music);
        }

        // GET: Comment_MusicAdmin/Create
        public ActionResult Create()
        {
            ViewBag.ID_Music = new SelectList(db.Music, "ID_Music", "Name_music");
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User");
            return View();
        }

        // POST: Comment_MusicAdmin/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Comments_Music,Content_Comments,Data_Comments,ID_Music,ID_User")] Comment_Music comment_Music)
        {
            if (ModelState.IsValid)
            {
                db.Comment_Music.Add(comment_Music);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Music = new SelectList(db.Music, "ID_Music", "Name_music", comment_Music.ID_Music);
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User", comment_Music.ID_User);
            return View(comment_Music);
        }

        // GET: Comment_MusicAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment_Music comment_Music = db.Comment_Music.Find(id);
            if (comment_Music == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Music = new SelectList(db.Music, "ID_Music", "Name_music", comment_Music.ID_Music);
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User", comment_Music.ID_User);
            return View(comment_Music);
        }

        // POST: Comment_MusicAdmin/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Comments_Music,Content_Comments,Data_Comments,ID_Music,ID_User")] Comment_Music comment_Music)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment_Music).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Music = new SelectList(db.Music, "ID_Music", "Name_music", comment_Music.ID_Music);
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User", comment_Music.ID_User);
            return View(comment_Music);
        }

        // GET: Comment_MusicAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment_Music comment_Music = db.Comment_Music.Find(id);
            if (comment_Music == null)
            {
                return HttpNotFound();
            }
            return View(comment_Music);
        }

        // POST: Comment_MusicAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment_Music comment_Music = db.Comment_Music.Find(id);
            db.Comment_Music.Remove(comment_Music);
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
