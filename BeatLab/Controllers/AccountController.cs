using BeatLab.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

namespace BeatLab.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account


        [HttpPost]
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
            using (UserContext db = new UserContext())
            {
                user = db.Users.FirstOrDefault(u => u.Login == model.Login);
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
            if (ModelState.IsValid)
            {
                User user = null;
                using (UserContext db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email_User == model.Email);
                }
                if (user == null)
                {
                    user = CreateNewUser(model);
                    bool isUserSuccessfullyAddedToDatabase = user != null;
                    if (isUserSuccessfullyAddedToDatabase)
                    {
                        FormsAuthentication.SetAuthCookie(model.Login, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(
                        nameof(model.Email), "Пользователь с таким email уже существует");
                }
            }

            return View(model);
        }

        private static User CreateNewUser(RegisterModel model)
        {
            SaltedPasswordGenerator.GenerateHashAndSalt(model.Password,
                                         out byte[] saltBytes,
                                         out byte[] encryptedPasswordAndSalt);
            using (UserContext db = new UserContext())
            {
                User user = new User
                {
                    Email_User = model.Email,
                    Password_Hash = encryptedPasswordAndSalt,
                    Salt = saltBytes,
                    Login = model.Login,
                    ID_User_Type = 2
                };
                db.Users.Add(user);
                db.SaveChanges();
                return user;
            }
        }
    }
}