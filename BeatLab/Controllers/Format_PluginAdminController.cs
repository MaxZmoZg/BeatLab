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
    public class Format_PluginAdminController : Controller
    {
        private BeatLabDBEntities db = new BeatLabDBEntities();

        // GET: Format_PluginAdmin
        public ActionResult Index()
        {
            return View(db.Format_Plugin.ToList());
        }

        // GET: Format_PluginAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Format_Plugin format_Plugin = db.Format_Plugin.Find(id);
            if (format_Plugin == null)
            {
                return HttpNotFound();
            }
            return View(format_Plugin);
        }

        // GET: Format_PluginAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Format_PluginAdmin/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Format_Plugin,Name_Format_Plugin")] Format_Plugin format_Plugin)
        {
            if (ModelState.IsValid)
            {
                db.Format_Plugin.Add(format_Plugin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(format_Plugin);
        }

        // GET: Format_PluginAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Format_Plugin format_Plugin = db.Format_Plugin.Find(id);
            if (format_Plugin == null)
            {
                return HttpNotFound();
            }
            return View(format_Plugin);
        }

        // POST: Format_PluginAdmin/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Format_Plugin,Name_Format_Plugin")] Format_Plugin format_Plugin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(format_Plugin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(format_Plugin);
        }

        // GET: Format_PluginAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Format_Plugin format_Plugin = db.Format_Plugin.Find(id);
            if (format_Plugin == null)
            {
                return HttpNotFound();
            }
            return View(format_Plugin);
        }

        // POST: Format_PluginAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Format_Plugin format_Plugin = db.Format_Plugin.Find(id);
            db.Format_Plugin.Remove(format_Plugin);
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
