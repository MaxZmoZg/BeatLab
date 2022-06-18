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
    public class Order_PluginController : Controller
    {
        private BeatLabDBEntities db = new BeatLabDBEntities();

        // GET: Order_Plugin
        public ActionResult Index()
        {
            var order_Plugin = db.Order_Plugin.Include(o => o.Plugins).Include(o => o.User);
            return View(order_Plugin.ToList());
        }

        // GET: Order_Plugin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Plugin order_Plugin = db.Order_Plugin.Find(id);
            if (order_Plugin == null)
            {
                return HttpNotFound();
            }
            return View(order_Plugin);
        }

        // GET: Order_Plugin/Create
        public ActionResult Create()
        {
            ViewBag.ID_Plugin = new SelectList(db.Plugins, "ID_Plugin", "Name_Plugin");
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User");
            return View();
        }

        // POST: Order_Plugin/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Order_Plugin,ID_Plugin,ID_User,Card_Number,Card_expiration_date,Card_secure_code,Card_owner,IsConsentContract")] Order_Plugin order_Plugin)
        {
            if (ModelState.IsValid)
            {
                order_Plugin.ID_Plugin = int.Parse(Request.QueryString["ID_Plugin"]);
                order_Plugin.ID_User = db.User.First(u => u.Login == HttpContext.User.Identity.Name).ID_User;
                db.Order_Plugin.Add(order_Plugin);
                db.SaveChanges();
                return RedirectToAction("Details","Users");
            }

            ViewBag.ID_Plugin = new SelectList(db.Plugins, "ID_Plugin", "Name_Plugin", order_Plugin.ID_Plugin);
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User", order_Plugin.ID_User);
            return View(order_Plugin);
        }

        // GET: Order_Plugin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Plugin order_Plugin = db.Order_Plugin.Find(id);
            if (order_Plugin == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Plugin = new SelectList(db.Plugins, "ID_Plugin", "Name_Plugin", order_Plugin.ID_Plugin);
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User", order_Plugin.ID_User);
            return View(order_Plugin);
        }

        // POST: Order_Plugin/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Order_Plugin,ID_Plugin,ID_User,Card_Number,Card_expiration_date,Card_secure_code,Card_owner")] Order_Plugin order_Plugin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_Plugin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Plugin = new SelectList(db.Plugins, "ID_Plugin", "Name_Plugin", order_Plugin.ID_Plugin);
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User", order_Plugin.ID_User);
            return View(order_Plugin);
        }

        // GET: Order_Plugin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Plugin order_Plugin = db.Order_Plugin.Find(id);
            if (order_Plugin == null)
            {
                return HttpNotFound();
            }
            return View(order_Plugin);
        }

        // POST: Order_Plugin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order_Plugin order_Plugin = db.Order_Plugin.Find(id);
            db.Order_Plugin.Remove(order_Plugin);
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
