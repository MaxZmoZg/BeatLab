using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeatLab.Controllers
{
    public class UsersController : Controller
    {
        private BeatLabDBEntities db = new BeatLabDBEntities();

        // GET: Users
        public async Task<ActionResult> Index()
        {
            var user = db.User.Include(u => u.User_Type);
            return View(await user.ToListAsync());
        }

        // GET: Users/Details
        public async Task<ActionResult> Details()
        {
            User user = await db.User.FirstAsync(u => u.Login == HttpContext.User.Identity.Name);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.ID_User_Type = new SelectList(db.User_Type, "ID_User_Type", "Name_User_Type");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_User,Last_Name_User,First_Name_User,Middle_Name_User,Description_User,Age_User,Image_User,Nickname_User,Login,Password_Hash,Salt,ID_User_Type,Email_User")] User user)
        {
            if (ModelState.IsValid)
            {
                db.User.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ID_User_Type = new SelectList(db.User_Type, "ID_User_Type", "Name_User_Type", user.ID_User_Type);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.User.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_User_Type = new SelectList(db.User_Type, "ID_User_Type", "Name_User_Type", user.ID_User_Type);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_User,Last_Name_User,First_Name_User,Middle_Name_User,Description_User,Age_User,Image_User,Nickname_User,Login,Password_Hash,Salt,ID_User_Type,Email_User")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ID_User_Type = new SelectList(db.User_Type, "ID_User_Type", "Name_User_Type", user.ID_User_Type);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.User.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            User user = await db.User.FindAsync(id);
            db.User.Remove(user);
            await db.SaveChangesAsync();
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
