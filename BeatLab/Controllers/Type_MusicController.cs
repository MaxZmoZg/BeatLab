using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using BeatLab;

namespace BeatLab.Controllers
{
    public class Type_MusicController : Controller
    {
        private BeatLabDBEntities db = new BeatLabDBEntities();

        // GET: Type_Music
        public ActionResult Index()
        {
            Price_Music price = new Price_Music();
            string xx = price.ID_Music.ToString();

            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();

           
            var results = db.Price_Music.Where(rs => rs.Price.Equals(xx));

            results.ToList().ForEach(rs => xValue.Add(rs.Date));
            results.ToList().ForEach(rs => yValue.Add(rs.Price));

            var chart = new Chart(600, 300, ChartTheme.Blue);
            chart.AddSeries(chartType: "Pie", xValue: xValue, yValues: yValue);
            chart.AddTitle("Payor");
            chart.Write("png");

            return null;
        }

        // GET: Type_Music/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Type_Music type_Music = db.Type_Music.Find(id);
            if (type_Music == null)
            {
                return HttpNotFound();
            }
            return View(type_Music);
        }

        // GET: Type_Music/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Type_Music/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Type_music,Name_Type_Music")] Type_Music type_Music)
        {
            if (ModelState.IsValid)
            {
                db.Type_Music.Add(type_Music);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(type_Music);
        }

        // GET: Type_Music/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Type_Music type_Music = db.Type_Music.Find(id);
            if (type_Music == null)
            {
                return HttpNotFound();
            }
            return View(type_Music);
        }

        // POST: Type_Music/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Type_music,Name_Type_Music")] Type_Music type_Music)
        {
            if (ModelState.IsValid)
            {
                db.Entry(type_Music).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(type_Music);
        }

        // GET: Type_Music/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Type_Music type_Music = db.Type_Music.Find(id);
            if (type_Music == null)
            {
                return HttpNotFound();
            }
            return View(type_Music);
        }

        // POST: Type_Music/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Type_Music type_Music = db.Type_Music.Find(id);
            db.Type_Music.Remove(type_Music);
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
