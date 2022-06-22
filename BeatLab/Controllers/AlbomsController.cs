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
    public class AlbomsController : Controller
    {
        private BeatLabDBEntities db = new BeatLabDBEntities();

        // GET: Alboms
        [Authorize]
        public ActionResult Index()
        {
            LoadDropDownLists();
            var alboms = db.Alboms
             
                .Include(m => m.Type_Alboms)
                .Include(m => m.User);
            return View(alboms.ToList());
            //var alboms = db.Alboms.Include(a => a.Type_Alboms).Include(a => a.User);
            //return View(alboms.ToList());
        }
         private void LoadDropDownLists()
        {
            List<Type_Alboms> formats = db.Type_Alboms.ToList();
            formats.Insert(0, new Type_Alboms{ Name_Type_Alboms = "-----" });
            ViewBag.ID_Type_Alboms = new SelectList(formats, "ID_Type_Alboms", "Name_Type_Alboms");


        }
        // GET: Alboms/Details/5
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

        // GET: Alboms/Create
        public ActionResult Create()
        {
            ViewBag.ID_Type_Alboms = new SelectList(db.Type_Alboms, "ID_Type_Alboms", "Name_Type_Alboms");
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User");
            return View();
        }

        // POST: Alboms/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Album,ID_User,Name_Album,Image_Album,ID_Type_Alboms")] Alboms alboms, HttpPostedFileBase uploadImage)
        {
            if (alboms.Name_Album == null || alboms.Name_Album== "")
            {
                ModelState.AddModelError(nameof(alboms.Name_Album), "Введите название");
            }
        

            if (ModelState.IsValid)
            {
                if (uploadImage != null)
                {
                    alboms.Image_Album = uploadImage.ToByteArray();
                }
                alboms.ID_User = Me.GetId();
                db.Alboms.Add(alboms);
                db.SaveChanges();
                return RedirectToAction("Details","Users");
            }

            ViewBag.ID_Type_Alboms = new SelectList(db.Type_Alboms, "ID_Type_Alboms", "Name_Type_Alboms", alboms.ID_Type_Alboms);
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User", alboms.ID_User);
            return View(alboms);
        }

        // GET: Alboms/Edit/5
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

        // POST: Alboms/Edit/5
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
                return RedirectToAction("Details","Users");
            }
            ViewBag.ID_Type_Alboms = new SelectList(db.Type_Alboms, "ID_Type_Alboms", "Name_Type_Alboms", alboms.ID_Type_Alboms);
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User", alboms.ID_User);
            return View(alboms);
        }

        // GET: Alboms/Delete/5
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

        // POST: Alboms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Alboms alboms = db.Alboms.Find(id);
            db.Alboms.Remove(alboms);
            db.SaveChanges();
            return RedirectToAction("Details","Users");
        }

        [HttpPost]
        public ActionResult Filter()
        {
            List<Alboms> filteredAlboms = db.Alboms
 
                .Include(m => m.Type_Alboms)
                .Include(m => m.User)
                .ToList();
            if (!string.IsNullOrWhiteSpace(Request["Name"]))
            {
                filteredAlboms = filteredAlboms.Where(m =>
                {
                    return m.Name_Album.IndexOf(Request["Name"],
                                                StringComparison.OrdinalIgnoreCase) != -1;
                })
                    .ToList();
            }

            if (Request.Form["ID_Type_Alboms"] is string albomsFormatId && albomsFormatId != "0")
            {
                filteredAlboms = filteredAlboms.Where(m => m.ID_Type_Alboms== int.Parse(albomsFormatId))

                    .ToList();
            }
            

            if (Request.Form.Keys.OfType<string>().Contains("Алфавиту"))
            {
                filteredAlboms = filteredAlboms
                    .OrderBy(m => m.Name_Album)
                    .ToList();
            }

          
            LoadDropDownLists();
            return View("Index", filteredAlboms);
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
