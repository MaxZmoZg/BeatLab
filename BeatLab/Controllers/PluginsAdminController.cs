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
    public class PluginsAdminController : Controller
    {
        private BeatLabDBEntities db = new BeatLabDBEntities();

        // GET: PluginsAdmin
        public ActionResult Index()
        {
            var plugins = db.Plugins.Include(p => p.Format_Plugin).Include(p => p.User);
            return View(plugins.ToList());
        }

        // GET: PluginsAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plugins plugins = db.Plugins.Find(id);
            if (plugins == null)
            {
                return HttpNotFound();
            }
            return View(plugins);
        }

        // GET: PluginsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.ID_Format_Plugin = new SelectList(db.Format_Plugin, "ID_Format_Plugin", "Name_Format_Plugin");
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User");
            return View();
        }

        // POST: PluginsAdmin/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Plugin,Date_of_issue_Plugin,ID_Format_Plugin,Name_Plugin,Plugin_file,Version_Plugins,IsDeleted,Description_plugin,Image_Plugin,ID_User")] Plugins plugins)
        {
            if (ModelState.IsValid)
            {
                db.Plugins.Add(plugins);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Format_Plugin = new SelectList(db.Format_Plugin, "ID_Format_Plugin", "Name_Format_Plugin", plugins.ID_Format_Plugin);
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User", plugins.ID_User);
            return View(plugins);
        }

        // GET: PluginsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plugins plugins = db.Plugins.Find(id);
            if (plugins == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Format_Plugin = new SelectList(db.Format_Plugin, "ID_Format_Plugin", "Name_Format_Plugin", plugins.ID_Format_Plugin);
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User", plugins.ID_User);
            return View(plugins);
        }

        // POST: PluginsAdmin/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Plugin,Date_of_issue_Plugin,ID_Format_Plugin,Name_Plugin,Plugin_file,Version_Plugins,IsDeleted,Description_plugin,Image_Plugin,ID_User")] Plugins plugins)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plugins).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Format_Plugin = new SelectList(db.Format_Plugin, "ID_Format_Plugin", "Name_Format_Plugin", plugins.ID_Format_Plugin);
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User", plugins.ID_User);
            return View(plugins);
        }

        // GET: PluginsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plugins plugins = db.Plugins.Find(id);
            if (plugins == null)
            {
                return HttpNotFound();
            }
            return View(plugins);
        }

        // POST: PluginsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Plugins plugins = db.Plugins.Find(id);
            db.Plugins.Remove(plugins);
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
