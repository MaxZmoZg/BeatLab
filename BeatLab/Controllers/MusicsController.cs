using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace BeatLab.Controllers
{
    public class MusicsController : Controller
    {
        private readonly BeatLabDBEntities db = new BeatLabDBEntities();

        // GET: Musics
        public ActionResult Index()
        {
            LoadDropDownLists();
            var music = db.Music
                .Include(m => m.Alboms)
                .Include(m => m.Genere_Of_Music)
                .Include(m => m.Type_Music)
                .Include(m => m.User);
            return View(music.ToList());
        }

        private void LoadDropDownLists()
        {
            List<Genere_Of_Music> genres = db.Genere_Of_Music.ToList();
            genres.Insert(0, new Genere_Of_Music { Name_Gener_of_music = "-----" });
            ViewBag.ID_Genere_of_Music = new SelectList(genres, "ID_Genere_Of_Music", "Name_Gener_of_music");

            List<Type_Music> types = db.Type_Music.ToList();
            types.Insert(0, new Type_Music { Name_Type_Music = "-----" });
            ViewBag.ID_Type_mysic = new SelectList(types, "ID_Type_music", "Name_Type_Music");

            List<User> users = db.User.ToList();
            users.Insert(0, new User { Nickname_User = "-----" });
            ViewBag.ID_User = new SelectList(users, "ID_User", "Nickname_User");
        }

        // GET: Musics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Music music = db.Music.Find(id);
            if (music == null)
            {
                return HttpNotFound();
            }
            return View(music);
        }

        // GET: Musics/Create
        public ActionResult Create()
        {
            ViewBag.ID_Alboms = new SelectList(db.Alboms, "ID_Album", "Name_Album");
            ViewBag.ID_Genere_of_Music = new SelectList(db.Genere_Of_Music, "ID_Genere_Of_Music", "Name_Gener_of_music");
            ViewBag.ID_Type_mysic = new SelectList(db.Type_Music, "ID_Type_music", "Name_Type_Music");
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Nickname_User");
            return View();
        }

        // POST: Musics/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Music,ID_Genere_of_Music,ID_Type_mysic,Name_music,Description_Music,Price_Music,ID_Alboms,Image_music,ID_User,PriceString")] Music music, HttpPostedFileBase uploadImage)
        {
            if (music.Name_music == null || music.Name_music == "")
            {
                ModelState.AddModelError(nameof(music.Name_music), "Введите название");
            }
            if (music.Description_Music == null || music.Description_Music == "")
            {
                ModelState.AddModelError(nameof(music.Description_Music), "Введите описание");
            }
            

            if (ModelState.IsValid)
            {
                if (uploadImage != null)
                {
                    music.Image_music = uploadImage.ToByteArray();
                }
                if (Request.Files["uploadMusic"] != null)
                {
                    music.Music_file = Request.Files["uploadMusic"].ToByteArray();
                }

                music.Alboms = null;
                music.ID_User = db.User.First(u => u.Login == HttpContext.User.Identity.Name).ID_User;
                Price_Music priceMusic = new Price_Music
                {
                    Price = int.Parse(Request.Form["PriceString"]),
                    Date = DateTime.Now
                };
                music.Price_Music.Add(priceMusic);
                db.Music.Add(music);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.ID_Genere_of_Music = new SelectList(db.Genere_Of_Music, "ID_Genere_Of_Music", "Name_Gener_of_music", music.ID_Genere_of_Music);
            ViewBag.ID_Type_mysic = new SelectList(db.Type_Music, "ID_Type_music", "Name_Type_Music", music.ID_Type_mysic);

            return View(music);
        }

        // GET: Musics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Music music = db.Music.Find(id);
            if (music == null)
            {
                return HttpNotFound();
            }

            music.PriceString = db.Price_Music
                .ToList()
                .Last().Price
                .ToString();

            ViewBag.ID_Alboms = new SelectList(db.Alboms, "ID_Album", "Name_Album", music.ID_Alboms);
            ViewBag.ID_Genere_of_Music = new SelectList(db.Genere_Of_Music, "ID_Genere_Of_Music", "Name_Gener_of_music", music.ID_Genere_of_Music);
            ViewBag.ID_Type_mysic = new SelectList(db.Type_Music, "ID_Type_music", "Name_Type_Music", music.ID_Type_mysic);
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Nickname_User", music.ID_User);
            return View(music);
        }

        // POST: Musics/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Music,ID_Genere_of_Music,ID_Type_mysic,Name_music,Description_Music,Price_Music,ID_Alboms,Image_music,ID_User,PriceString,Music_file")] Music music)
        {
            if (ModelState.IsValid)
            {
                db.Entry(music).State = EntityState.Modified;
                db.SaveChanges();

                Price_Music priceMusic = new Price_Music
                {
                    ID_Music = music.ID_Music,
                    Price = int.Parse(music.PriceString),
                    Date = DateTime.Now
                };
                db.Price_Music.Add(priceMusic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Alboms = new SelectList(db.Alboms, "ID_Album", "Name_Album", music.ID_Alboms);
            ViewBag.ID_Genere_of_Music = new SelectList(db.Genere_Of_Music, "ID_Genere_Of_Music", "Name_Gener_of_music", music.ID_Genere_of_Music);
            ViewBag.ID_Type_mysic = new SelectList(db.Type_Music, "ID_Type_music", "Name_Type_Music", music.ID_Type_mysic);
            ViewBag.ID_User = new SelectList(db.User, "ID_User", "Nickname_User", music.ID_User);
            return View(music);
        }

        // GET: Musics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Music music = db.Music.Find(id);
            if (music == null)
            {
                return HttpNotFound();
            }
            return View(music);
        }

        // POST: Musics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Music music = db.Music.Find(id);


            db.Music.Remove(music);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult LoadAudio(int musicId)
        {
            byte[] songBytes;
            songBytes = db.Music.First(m => m.ID_Music == musicId).Music_file;
            if (songBytes == null) return null;
            FileStreamResult songStreamResult =
                new FileStreamResult(new MemoryStream(songBytes),
                                     "audio/mp3");
            return songStreamResult;
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
            List<Music> filteredMusics = db.Music
                .Include(m => m.Alboms)
                .Include(m => m.Genere_Of_Music)
                .Include(m => m.Type_Music)
                .Include(m => m.User)
                .ToList();
            if (!string.IsNullOrWhiteSpace(Request["Name"]))
            {
                filteredMusics = filteredMusics.Where(m =>
                {
                    return m.Name_music.IndexOf(Request["Name"],
                                                StringComparison.OrdinalIgnoreCase) != -1;
                })
                    .ToList();
            }

            if (Request.Form["ID_Genere_of_Music"] is string musicGenreId && musicGenreId != "0")
            {
                filteredMusics = filteredMusics.Where(m => m.ID_Genere_of_Music == int.Parse(musicGenreId))
                    .ToList();
            }
            if (Request.Form["ID_User"] is string userId && userId != "0")
            {
                filteredMusics = filteredMusics.Where(m => m.ID_User == int.Parse(userId))
                    .ToList();
            }
            if (Request.Form["ID_Type_mysic"] is string musicTypeId && musicTypeId != "0")
            {
                filteredMusics = filteredMusics.Where(m => m.ID_Type_mysic == int.Parse(musicTypeId))
                    .ToList();
            }

            if (Request.Form.Keys.OfType<string>().Contains("Алфавиту"))
            {
                filteredMusics = filteredMusics
                    .OrderByDescending(m => m.Name_music)
                    .ToList();
            }
            else if (Request.Form.Keys.OfType<string>().Contains("Рейтингу"))
            {
                filteredMusics = filteredMusics
                    .OrderByDescending(m => m.Order_Music.Count)
                    .ToList();
            }
            else if (Request.Form.Keys.OfType<string>().Contains("Цене"))
            {
                filteredMusics = filteredMusics
                    .OrderByDescending(m =>
                    {
                        if (m.Price_Music.LastOrDefault() is Price_Music priceMusic)
                        {
                            return priceMusic.Price;
                        }
                        else
                        {
                            return 0;
                        }
                    })
                    .ToList();
            }
            LoadDropDownLists();
            return View("Index", filteredMusics);
        }
        [Authorize]
        public ActionResult PostComment(int musicId, string comment)
        {
            Comment_Music newComment = new Comment_Music
            {
                ID_Music = musicId,
                Data_Comments = DateTime.Now,
                Content_Comments = comment,
                ID_User = db.User.First(u => u.Login == HttpContext.User.Identity.Name).ID_User
            };
            db.Comment_Music.Add(newComment);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = musicId });
        }
    }
}
