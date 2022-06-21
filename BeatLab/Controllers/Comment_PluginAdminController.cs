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
    public class Comment_PluginAdminController : Controller
    {
        private BeatLabDBEntities db = new BeatLabDBEntities();

        // GET: Comment_PluginAdmin
        public ActionResult Index()
        {
            var comment_Plugin = db.Comment_Plugin.Include(c => c.Plugins).Include(c => c.User);
            return View(comment_Plugin.ToList());
        }

        // GET: Comment_PluginAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment_Plugin comment_Plugin = db.Comment_Plugin.Find(id);
            if (comment_Plugin == null)
            {
                return HttpNotFound();
            }
            return View(comment_Plugin);
        }

        // GET: Comment_PluginAdmin/Create
        public ActionResult Create()
        {
            ViewBag.ID_Plugin = new SelectList(db.Plugins, "ID_Plugin", "Name_Plugin");
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User");
            return View();
        }

        // POST: Comment_PluginAdmin/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Comment_Plugin,Content_Comment,Date_Comment,ID_Plugin,ID_User")] Comment_Plugin comment_Plugin)
        {
            if (ModelState.IsValid)
            {
                db.Comment_Plugin.Add(comment_Plugin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Plugin = new SelectList(db.Plugins, "ID_Plugin", "Name_Plugin", comment_Plugin.ID_Plugin);
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User", comment_Plugin.ID_User);
            return View(comment_Plugin);
        }

        // GET: Comment_PluginAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment_Plugin comment_Plugin = db.Comment_Plugin.Find(id);
            if (comment_Plugin == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Plugin = new SelectList(db.Plugins, "ID_Plugin", "Name_Plugin", comment_Plugin.ID_Plugin);
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User", comment_Plugin.ID_User);
            return View(comment_Plugin);
        }

        // POST: Comment_PluginAdmin/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Comment_Plugin,Content_Comment,Date_Comment,ID_Plugin,ID_User")] Comment_Plugin comment_Plugin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment_Plugin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Plugin = new SelectList(db.Plugins, "ID_Plugin", "Name_Plugin", comment_Plugin.ID_Plugin);
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User", comment_Plugin.ID_User);
            return View(comment_Plugin);
        }

        // GET: Comment_PluginAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment_Plugin comment_Plugin = db.Comment_Plugin.Find(id);
            if (comment_Plugin == null)
            {
                return HttpNotFound();
            }
            return View(comment_Plugin);
        }

        // POST: Comment_PluginAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment_Plugin comment_Plugin = db.Comment_Plugin.Find(id);
            db.Comment_Plugin.Remove(comment_Plugin);
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
