using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeatLab;

namespace BeatLab.Controllers
{
    public class UsersController : Controller
    {
        private BeatLabDBEntities db = new BeatLabDBEntities();

        // GET: Users
        [Authorize]
        public ActionResult Index()
        {
            var user = db.User.Include(u => u.User_Type);
            return View(user.ToList());
        }

        // GET: Users/Details/5
        [Authorize]
        public ActionResult Details()
        {
            return View(db.User.First(u => u.Login == HttpContext.User.Identity.Name));
        }

        // GET: Users/Create 
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.ID_User_Type = new SelectList(db.User_Type, "ID_User_Type", "Name_User_Type");
            return View();
        }

        // POST: Users/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID_User,Last_Name_User,First_Name_User,Middle_Name_User,Description_User,Age_User,Image_User,Nickname_User,Login,Password_Hash,Salt,ID_User_Type,Email_User")] User user)
        {
            if (ModelState.IsValid)
            {
                db.User.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_User_Type = new SelectList(db.User_Type, "ID_User_Type", "Name_User_Type", user.ID_User_Type);
            return View(user);
        }

        // GET: Users/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_User_Type = new SelectList(db.User_Type, "ID_User_Type", "Name_User_Type", user.ID_User_Type);
            return View(user);
        }

        // POST: Users/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(User user, HttpPostedFileBase uploadImage)
        {
           
           

            if (ModelState.IsValid)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                User userFromDatabase = db.User.First(u => u.Login == HttpContext.User.Identity.Name);
                user.Image_User = imageData;
                userFromDatabase.Login = user.Login;
                userFromDatabase.Last_Name_User = user.Last_Name_User;
                userFromDatabase.First_Name_User = user.First_Name_User;
                userFromDatabase.Last_Name_User = user.Middle_Name_User;
                userFromDatabase.Nickname_User = user.Nickname_User;
                userFromDatabase.Image_User = user.Image_User;
                userFromDatabase.Description_User = user.Description_User;
                userFromDatabase.Age_User = user.Age_User;
                userFromDatabase.Email_User = user.Email_User;
              
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            ViewBag.ID_User_Type = new SelectList(db.User_Type, "ID_User_Type", "Name_User_Type", user.ID_User_Type);
            return View(user);
        }




        // GET: Users/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.User.Find(id);
            db.User.Remove(user);
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
