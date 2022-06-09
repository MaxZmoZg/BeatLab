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
    public class СhatController : Controller
    {
        private BeatLabDBEntities db = new BeatLabDBEntities();

        // GET: Сhat
        public ActionResult Index()
        {
            var сhat = db.Сhat.Include(с => с.User).Include(с => с.User1);
            return View(сhat.ToList());
        }

        // GET: Сhat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Сhat сhat = db.Сhat.Find(id);
            if (сhat == null)
            {
                return HttpNotFound();
            }
            return View(сhat);
        }

        // GET: Сhat/Create
        public ActionResult Create()
        {
            ViewBag.ID_User1 = new SelectList(db.User, "ID_User", "Last_Name_User");
            ViewBag.ID_User2_ = new SelectList(db.User, "ID_User", "Last_Name_User");
            return View();
        }

        // POST: Сhat/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Chat,ID_User1,ID_User2_,IsFirstTyping_Chat,Message_Char")] Сhat сhat)
        {
            if (ModelState.IsValid)
            {
                db.Сhat.Add(сhat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_User1 = new SelectList(db.User, "ID_User", "Last_Name_User", сhat.ID_User1);
            ViewBag.ID_User2_ = new SelectList(db.User, "ID_User", "Last_Name_User", сhat.ID_User2_);
            return View(сhat);
        }

        // GET: Сhat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Сhat сhat = db.Сhat.Find(id);
            if (сhat == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_User1 = new SelectList(db.User, "ID_User", "Last_Name_User", сhat.ID_User1);
            ViewBag.ID_User2_ = new SelectList(db.User, "ID_User", "Last_Name_User", сhat.ID_User2_);
            return View(сhat);
        }

        // POST: Сhat/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Chat,ID_User1,ID_User2_,IsFirstTyping_Chat,Message_Char")] Сhat сhat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(сhat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_User1 = new SelectList(db.User, "ID_User", "Last_Name_User", сhat.ID_User1);
            ViewBag.ID_User2_ = new SelectList(db.User, "ID_User", "Last_Name_User", сhat.ID_User2_);
            return View(сhat);
        }

        // GET: Сhat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Сhat сhat = db.Сhat.Find(id);
            if (сhat == null)
            {
                return HttpNotFound();
            }
            return View(сhat);
        }

        // POST: Сhat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Сhat сhat = db.Сhat.Find(id);
            db.Сhat.Remove(сhat);
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
