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
    public class Price_PluginAdminController : Controller
    {
        private BeatLabDBEntities db = new BeatLabDBEntities();

        // GET: Price_PluginAdmin
        public ActionResult Index()
        {
            var price_Plugin = db.Price_Plugin.Include(p => p.Plugins);
            return View(price_Plugin.ToList());
        }

        // GET: Price_PluginAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Price_Plugin price_Plugin = db.Price_Plugin.Find(id);
            if (price_Plugin == null)
            {
                return HttpNotFound();
            }
            return View(price_Plugin);
        }

        // GET: Price_PluginAdmin/Create
        public ActionResult Create()
        {
            ViewBag.ID_Plugin = new SelectList(db.Plugins, "ID_Plugin", "Name_Plugin");
            return View();
        }

        // POST: Price_PluginAdmin/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Price_Plugin,Price,Date,ID_Plugin")] Price_Plugin price_Plugin)
        {
            if (ModelState.IsValid)
            {
                db.Price_Plugin.Add(price_Plugin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Plugin = new SelectList(db.Plugins, "ID_Plugin", "Name_Plugin", price_Plugin.ID_Plugin);
            return View(price_Plugin);
        }

        // GET: Price_PluginAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Price_Plugin price_Plugin = db.Price_Plugin.Find(id);
            if (price_Plugin == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Plugin = new SelectList(db.Plugins, "ID_Plugin", "Name_Plugin", price_Plugin.ID_Plugin);
            return View(price_Plugin);
        }

        // POST: Price_PluginAdmin/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Price_Plugin,Price,Date,ID_Plugin")] Price_Plugin price_Plugin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(price_Plugin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Plugin = new SelectList(db.Plugins, "ID_Plugin", "Name_Plugin", price_Plugin.ID_Plugin);
            return View(price_Plugin);
        }

        // GET: Price_PluginAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Price_Plugin price_Plugin = db.Price_Plugin.Find(id);
            if (price_Plugin == null)
            {
                return HttpNotFound();
            }
            return View(price_Plugin);
        }

        // POST: Price_PluginAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Price_Plugin price_Plugin = db.Price_Plugin.Find(id);
            db.Price_Plugin.Remove(price_Plugin);
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
