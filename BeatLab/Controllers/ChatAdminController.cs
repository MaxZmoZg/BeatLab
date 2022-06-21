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
    public class ChatAdminController : Controller
    {
        private BeatLabDBEntities db = new BeatLabDBEntities();

        // GET: ChatAdmin
        public ActionResult Index()
        {
            var chat = db.Chat.Include(c => c.User).Include(c => c.User1);
            return View(chat.ToList());
        }

        // GET: ChatAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chat chat = db.Chat.Find(id);
            if (chat == null)
            {
                return HttpNotFound();
            }
            return View(chat);
        }

        // GET: ChatAdmin/Create
        public ActionResult Create()
        {
            ViewBag.ID_Receiver = new SelectList(db.User, "ID_User", "Last_Name_User");
            ViewBag.ID_Sender = new SelectList(db.User, "ID_User", "Last_Name_User");
            return View();
        }

        // POST: ChatAdmin/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Chat,ID_Sender,ID_Receiver,Message")] Chat chat)
        {
            if (ModelState.IsValid)
            {
                db.Chat.Add(chat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Receiver = new SelectList(db.User, "ID_User", "Last_Name_User", chat.ID_Receiver);
            ViewBag.ID_Sender = new SelectList(db.User, "ID_User", "Last_Name_User", chat.ID_Sender);
            return View(chat);
        }

        // GET: ChatAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chat chat = db.Chat.Find(id);
            if (chat == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Receiver = new SelectList(db.User, "ID_User", "Last_Name_User", chat.ID_Receiver);
            ViewBag.ID_Sender = new SelectList(db.User, "ID_User", "Last_Name_User", chat.ID_Sender);
            return View(chat);
        }

        // POST: ChatAdmin/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Chat,ID_Sender,ID_Receiver,Message")] Chat chat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Receiver = new SelectList(db.User, "ID_User", "Last_Name_User", chat.ID_Receiver);
            ViewBag.ID_Sender = new SelectList(db.User, "ID_User", "Last_Name_User", chat.ID_Sender);
            return View(chat);
        }

        // GET: ChatAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chat chat = db.Chat.Find(id);
            if (chat == null)
            {
                return HttpNotFound();
            }
            return View(chat);
        }

        // POST: ChatAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Chat chat = db.Chat.Find(id);
            db.Chat.Remove(chat);
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
