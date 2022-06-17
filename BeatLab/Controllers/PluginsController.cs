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
    public class PluginsController : Controller
    {
        private BeatLabDBEntities db = new BeatLabDBEntities();

        // GET: Plugins
        public ActionResult Index()
        {
            LoadDropDownLists();
            var plugins = db.Plugins
                .Where(m => !m.IsDeleted)
                .Include(m => m.Format_Plugin)
                .Include(m => m.User);
            return View(plugins.ToList());
        }
        private void LoadDropDownLists()
        {
            List<Format_Plugin> formats = db.Format_Plugin.ToList();
            formats.Insert(0, new Format_Plugin { Name_Format_Plugin = "-----" });
            ViewBag.ID_Format_Plugin = new SelectList(formats, "ID_Format_Plugin", "Name_Format_Plugin");

            
            List<User> users = db.User.ToList().Where(u => u.User_Type.Name_User_Type == "admin" && u.ID_User != Me.GetId()).ToList();
            users.Insert(0, new User { Nickname_User = "-----" });
            ViewBag.ID_User = new SelectList(users, "ID_User", "Nickname_User");
        }

        // GET: Plugins/Details/5
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

        // GET: Plugins/Create
        public ActionResult Create()
        {
            ViewBag.ID_Format_Plugin = new SelectList(db.Format_Plugin, "ID_Format_Plugin", "Name_Format_Plugin");
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User");
            return View();
        }

        // POST: Plugins/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Plugin,Date_of_issue_Plugin,ID_Format_Plugin,Name_Plugin,Plugin_file,Version_Plugins,Description_plugin,Image_Plugin,ID_User,PriceString")] Plugins plugins, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                if (uploadImage != null)
                {
                    plugins.Image_Plugin = uploadImage.ToByteArray();
                }
                if (Request.Files["uploadPlugin"] != null)
                {
                    plugins.Plugin_file = Request.Files["uploadPlugin"].ToByteArray();
                }
                plugins.ID_User = db.User.First(u => u.Login == HttpContext.User.Identity.Name).ID_User;
                Price_Plugin pricePlugin = new Price_Plugin
                {
                    Price = int.Parse(Request.Form["PriceString"]),
                    Date = DateTime.Now
                };
                plugins.Price_Plugin.Add(pricePlugin);
                db.Plugins.Add(plugins);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Format_Plugin = new SelectList(db.Format_Plugin, "ID_Format_Plugin","Name_Format_Plugin", plugins.ID_Format_Plugin);
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User", plugins.ID_User);
            return View(plugins);
        }

        // GET: Plugins/Edit/5
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
            plugins.PriceString = db.Price_Plugin
                .ToList()
                .Last().Price
                .ToString();

            ViewBag.ID_Format_Plugin = new SelectList(db.Format_Plugin, "ID_Format_Plugin", "Name_Format_Plugin", plugins.ID_Format_Plugin);
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User", plugins.ID_User);
            return View(plugins);
        }

        // POST: Plugins/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Plugin,Date_of_issue_Plugin,ID_Format_Plugin,Name_Plugin,Plugin_file,Version_Plugins,Description_plugin,Image_Plugin,ID_User,PriceString")] Plugins plugin, HttpPostedFileBase uploadImage, HttpPostedFileBase uploadPlugin)
        {
            if (ModelState.IsValid)
            {
                if (uploadImage != null)
                {
                    plugin.Image_Plugin = uploadImage.ToByteArray();
                }
                if (uploadPlugin != null)
                {
                    plugin.Plugin_file = uploadPlugin.ToByteArray();
                }
                db.Entry(plugin).State = EntityState.Modified;
                db.SaveChanges();
                Price_Plugin price = new Price_Plugin
                {
                    ID_Plugin = plugin.ID_Plugin,
                    Price = int.Parse(plugin.PriceString),
                    Date = DateTime.Now
                };
                db.Price_Plugin.Add(price);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Format_Plugin = new SelectList(db.Format_Plugin, "ID_Format_Plugin", "Name_Format_Plugin", plugin.ID_Format_Plugin);
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Last_Name_User", plugin.ID_User);
            return View(plugin);
        }

        // GET: Plugins/Delete/5
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

        // POST: Plugins/Delete/5
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

        [HttpPost]
        public ActionResult Filter()
        {
            List<Plugins> filteredPlugins = db.Plugins
                .Where(m => !m.IsDeleted)
                .Include(m => m.Format_Plugin)
                .Include(m => m.User)
                .ToList();
            if (!string.IsNullOrWhiteSpace(Request["Name"]))
            {
                filteredPlugins = filteredPlugins.Where(m =>
                {
                    return m.Name_Plugin.IndexOf(Request["Name"],
                                                StringComparison.OrdinalIgnoreCase) != -1;
                })
                    .ToList();
            }

            if (Request.Form["ID_Format_Plugin"] is string pluginFormatId && pluginFormatId != "0")
            {
                filteredPlugins = filteredPlugins.Where(m => m.ID_Format_Plugin == int.Parse(pluginFormatId))

                    .ToList();
            }
            if (Request.Form["ID_User"] is string userId && userId != "0")
            {
                filteredPlugins = filteredPlugins.Where(m => m.ID_User == int.Parse(userId))
                    .ToList();
            }

            if (Request.Form.Keys.OfType<string>().Contains("Алфавиту"))
            {
                filteredPlugins = filteredPlugins
                    .OrderByDescending(m => m.Name_Plugin)
                    .ToList();
            }
          
            else if (Request.Form.Keys.OfType<string>().Contains("Цене"))
            {
                filteredPlugins = filteredPlugins
                    .OrderByDescending(m =>
                    {
                        if (m.Price_Plugin.LastOrDefault() is Price_Plugin pricePlugin)
                        {
                            return pricePlugin.Price;
                        }
                        else
                        {
                            return 0;
                        }
                    })
                    .ToList();
            }
            LoadDropDownLists();
            return View("Index", filteredPlugins);
        }
    }
}
