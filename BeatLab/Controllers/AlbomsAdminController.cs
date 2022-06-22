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
    public class AlbomsAdminController : Controller
    {
        private BeatLabDBEntities db = new BeatLabDBEntities();

        // GET: AlbomsAdmin
        public ActionResult Index()
        {
            var alboms = db.Alboms.Include(a => a.Type_Alboms).Include(a => a.User);
            return View(alboms.ToList());
        }

        // GET: AlbomsAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alboms alboms = db.Alboms.Find(id);
            if (alboms == null)
            {
                return HttpNotFound();
            }
            return View(alboms);
        }

        // GET: AlbomsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.ID_Type_Alboms = new SelectList(db.Type_Alboms, "ID_Type_Alboms", "Name_Type_Alboms");
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User");
            return View();
        }

        // POST: AlbomsAdmin/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Album,ID_User,Name_Album,Image_Album,ID_Type_Alboms")] Alboms alboms, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                if (uploadImage != null)
                {
                    alboms.Image_Album = uploadImage.ToByteArray();
                }
                db.Alboms.Add(alboms);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Type_Alboms = new SelectList(db.Type_Alboms, "ID_Type_Alboms", "Name_Type_Alboms", alboms.ID_Type_Alboms);
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User", alboms.ID_User);
            return View(alboms);
        }

        // GET: AlbomsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alboms alboms = db.Alboms.Find(id);
            if (alboms == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Type_Alboms = new SelectList(db.Type_Alboms, "ID_Type_Alboms", "Name_Type_Alboms", alboms.ID_Type_Alboms);
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User", alboms.ID_User);
            return View(alboms);
        }

        // POST: AlbomsAdmin/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Album,ID_User,Name_Album,Image_Album,ID_Type_Alboms")] Alboms alboms, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                if (uploadImage != null)
                {
                    alboms.Image_Album = uploadImage.ToByteArray();
                }
                db.Entry(alboms).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Alboms");
            }
            ViewBag.ID_Type_Alboms = new SelectList(db.Type_Alboms, "ID_Type_Alboms", "Name_Type_Alboms", alboms.ID_Type_Alboms);
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User", alboms.ID_User);
            return View(alboms);
        }

        // GET: AlbomsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alboms alboms = db.Alboms.Find(id);
            if (alboms == null)
            {
                return HttpNotFound();
            }
            return View(alboms);
        }

        // POST: AlbomsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Alboms alboms = db.Alboms.Find(id);
            db.Alboms.Remove(alboms);
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
