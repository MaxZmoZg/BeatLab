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
    public class MusicsController : Controller
    {
        private BeatLabDBEntities db = new BeatLabDBEntities();

        // GET: Musics
        public ActionResult Index()
        {
            var music = db.Music.Include(m => m.Alboms).Include(m => m.Genere_Of_Music).Include(m => m.Type_Music).Include(m => m.User);
            return View(music.ToList());
        }

        // GET: Musics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Music music = db.Music.Find(id);
            if (music == null)
            {
                return HttpNotFound();
            }
            return View(music);
        }

        // GET: Musics/Create
        public ActionResult Create()
        {
            ViewBag.ID_Alboms = new SelectList(db.Alboms, "ID_Album", "Name_Album");
            ViewBag.ID_Genere_of_Music = new SelectList(db.Genere_Of_Music, "ID_Genere_Of_Music", "Name_Gener_of_music");
            ViewBag.ID_Type_mysic = new SelectList(db.Type_Music, "ID_Type_music", "Name_Type_Music");
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Nickname_User");
            return View();
        }

        // POST: Musics/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Music,ID_Genere_of_Music,ID_Type_mysic,Name_music,Description_Music,Priсe_Music,ID_Alboms,Image_music,ID_User")] Music music)
        {
            if (ModelState.IsValid)
            {
                
                db.Music.Add(music);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Alboms = new SelectList(db.Alboms, "ID_Album", "Name_Album", music.ID_Alboms);
            ViewBag.ID_Genere_of_Music = new SelectList(db.Genere_Of_Music, "ID_Genere_Of_Music", "Name_Gener_of_music", music.ID_Genere_of_Music);
            ViewBag.ID_Type_mysic = new SelectList(db.Type_Music, "ID_Type_music", "Name_Type_Music", music.ID_Type_mysic);
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Nickname_User", music.ID_User);
            return View(music);
        }

        // GET: Musics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Music music = db.Music.Find(id);
            if (music == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Alboms = new SelectList(db.Alboms, "ID_Album", "Name_Album", music.ID_Alboms);
            ViewBag.ID_Genere_of_Music = new SelectList(db.Genere_Of_Music, "ID_Genere_Of_Music", "Name_Gener_of_music", music.ID_Genere_of_Music);
            ViewBag.ID_Type_mysic = new SelectList(db.Type_Music, "ID_Type_music", "Name_Type_Music", music.ID_Type_mysic);
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Nickname_User", music.ID_User);
            return View(music);
        }

        // POST: Musics/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Music,ID_Genere_of_Music,ID_Type_mysic,Name_music,Description_Music,Priсe_Music,ID_Alboms,Image_music,ID_User")] Music music)
        {
            if (ModelState.IsValid)
            {
                db.Entry(music).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Alboms = new SelectList(db.Alboms, "ID_Album", "Name_Album", music.ID_Alboms);
            ViewBag.ID_Genere_of_Music = new SelectList(db.Genere_Of_Music, "ID_Genere_Of_Music", "Name_Gener_of_music", music.ID_Genere_of_Music);
            ViewBag.ID_Type_mysic = new SelectList(db.Type_Music, "ID_Type_music", "Name_Type_Music", music.ID_Type_mysic);
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Nickname_User", music.ID_User);
            return View(music);
        }

        // GET: Musics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Music music = db.Music.Find(id);
            if (music == null)
            {
                return HttpNotFound();
            }
            return View(music);
        }

        // POST: Musics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Music music = db.Music.Find(id);
            db.Music.Remove(music);
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
