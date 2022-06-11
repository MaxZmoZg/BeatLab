using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BeatLab.Controllers
{
    public class UsersController : Controller
    {
        private readonly BeatLabDBEntities db = new BeatLabDBEntities();

        // GET: Users
        [Authorize]
        public ActionResult Index()
        {
            var user = db.User.Include(u => u.User_Type);
            return View(user.ToList());
        }

        // GET: Users/Details/{login}
        [Authorize]
        public ActionResult Details(string login)
        {
            if (login == null)
            {
                return View(
                    db.User.First(u => u.Login == HttpContext.User.Identity.Name));
            }
            return View(
                db.User.First(u => u.Login == login));
        }

        // GET: Users/Create 
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.ID_User_Type = new SelectList(db.User_Type, "ID_User_Type", "Name_User_Type");
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID_User,Last_Name_User,First_Name_User,Middle_Name_User,Description_User,Age_User,Image_User,Nickname_User,Login,Password_Hash,Salt,ID_User_Type,Email_User")] User user)
        {

            if (ModelState.IsValid)
            {

                CreateNewUser(user); 
               
                return RedirectToAction("About","Home");
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(User user, HttpPostedFileBase uploadImage)
        {

            if (user.Last_Name_User == null || user.Last_Name_User ==  "")
            {
                ModelState.AddModelError(nameof(user.Last_Name_User), "Введите фамилию");
            }
            if (user.Middle_Name_User == null || user.Middle_Name_User == "")
            {
                ModelState.AddModelError(nameof(user.Middle_Name_User), "Введите отчество");
            }
            if (user.First_Name_User == null || user.First_Name_User == "")
            {
                ModelState.AddModelError(nameof(user.First_Name_User), "Введите имя");
            }
      
            if (ModelState.IsValid)
            {
                User userFromDatabase = db.User.First(u => u.Login == HttpContext.User.Identity.Name);
                userFromDatabase.Login = user.Login;
                userFromDatabase.Last_Name_User = user.Last_Name_User;
                userFromDatabase.First_Name_User = user.First_Name_User;
                userFromDatabase.Middle_Name_User = user.Middle_Name_User;
                userFromDatabase.Nickname_User = user.Nickname_User;
                if (uploadImage != null)
                {
                    userFromDatabase.Image_User = uploadImage.ToByteArray();
                }
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

        private static void CreateNewUser(User model)
        {
            var a = 123456;
            SaltedPasswordGenerator.GenerateHashAndSalt(a.ToString(),
                                         out byte[] saltBytes,
                                         out byte[] encryptedPasswordAndSalt);
            using (BeatLabDBEntities db = new BeatLabDBEntities())
            {


                User user = new User
                {
                    Last_Name_User = model.Last_Name_User,
                    First_Name_User = model.First_Name_User,
                    Middle_Name_User = model.Middle_Name_User,
                    Email_User = model.Email_User,
                    Description_User = model.Description_User,
                    Age_User =model.Age_User,
                    Password_Hash = encryptedPasswordAndSalt,
                    Salt = saltBytes,
                    Login = model.Login,
                    Nickname_User = model.Login,
                    ID_User_Type = 2,
                    Image_User =null
                    
                };

                db.User.Add(user);
                db.SaveChanges();
            }
        }


    }

}
