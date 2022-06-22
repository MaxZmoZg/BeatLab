using BeatLab.Models;
using BeatLab.Models.Entities;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace BeatLab.Controllers
{
    public class AccountController : Controller
    {
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = SearchUserInDatabase(model);

                bool isUserFound = user != null;
                if (isUserFound)
                {
                    if (user.IsDeleted)
                    {
                        return RedirectToAction("Deactivated", new { id = user.ID_User });
                    }
                    FormsAuthentication.SetAuthCookie(model.Login, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(
                        nameof(model.Login), "Неверный логин или пароль");
                }
            }

            return View(model);
        }

        private static User SearchUserInDatabase(LoginModel model)
        {
            User user = null;
            using (BeatLabDBEntities db = new BeatLabDBEntities())
            {
                user = db.User.FirstOrDefault(u => u.Login == model.Login);
                if (user == null)
                {
                    return null;
                }
                SaltedPasswordGenerator.GenerateHashAndSalt(model.Password,
                                         out byte[] saltBytes,
                                         out byte[] encryptedPasswordAndSalt,
                                         salt: user.Salt);
                if (Enumerable.SequenceEqual(user.Password_Hash,
                                             encryptedPasswordAndSalt))
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {

            if (model.Login == null)
            {
                ModelState.AddModelError(nameof(model.Login), "Введите логин");
            }
            else
            {
                using (BeatLabDBEntities db = new BeatLabDBEntities())
                {
                    if (db.User.Any(u => u.Login == model.Login))
                    {
                        ModelState.AddModelError(nameof(model.Login), "Такой логин уже существует");
                    }
                }
            }

            if (model.Password == null || model.Password.Length < 5)
            {
                ModelState.AddModelError(nameof(model.Password), "Введите пароль от 5 символов");
            }

            if (ModelState.IsValid)
            {
                CreateNewUser(model);
                FormsAuthentication.SetAuthCookie(model.Login, true);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        private static void CreateNewUser(RegisterModel model)
        {
            SaltedPasswordGenerator.GenerateHashAndSalt(model.Password,
                                         out byte[] saltBytes,
                                         out byte[] encryptedPasswordAndSalt);
            using (BeatLabDBEntities db = new BeatLabDBEntities())
            {


                User user = new User
                {
                    Email_User = model.Email,
                    Password_Hash = encryptedPasswordAndSalt,
                    Salt = saltBytes,
                    Login = model.Login,
                    Nickname_User = model.Login,
                    ID_User_Type = UserTypes.User
                };

                db.User.Add(user);
                db.SaveChanges();
            }
        }

        public ActionResult Deactivated(int id)
        {
            return View(id);
        }

        public ActionResult Recover(int id)
        {
            using (BeatLabDBEntities entities = new BeatLabDBEntities())
            {
                entities.User.Find(id).IsDeleted = false;
                entities.SaveChanges();
                FormsAuthentication.SetAuthCookie(entities.User.Find(id).Login, true);
                return RedirectToAction("Index", "Home");
            }
        }
    }
}