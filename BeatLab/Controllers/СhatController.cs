using System.Linq;
using System.Web.Mvc;

namespace BeatLab.Controllers
{
    public class ChatController : Controller
    {
        private readonly BeatLabDBEntities db = new BeatLabDBEntities();

        // GET: Chat
        public ActionResult Index(int? inputReceiverId)
        {
            int receiverId;
            if (!inputReceiverId.HasValue)
            {
                receiverId = db.User
                    .Where(u => u.Login != HttpContext.User.Identity.Name)
                    .First().ID_User;
            }
            else
            {
                receiverId = db.User.Find(inputReceiverId).ID_User;
            }
            int senderId = db.User.First(u => u.Login == HttpContext.User.Identity.Name).ID_User;
            ViewBag.Chat = db.Chat.Where(c => c.ID_Sender == senderId
                                              && c.ID_Receiver == receiverId
                                              || c.ID_Sender == receiverId
                                              && c.ID_Receiver == senderId);
            ViewBag.ReceiverId = receiverId;
            return View(db);
        }

        [HttpPost]
        public ActionResult PostComment(string text, int receiverId, int senderId)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                Chat chat = new Chat
                {
                    ID_Receiver = receiverId,
                    ID_Sender = senderId,
                    Message = text
                };
                db.Chat.Add(chat);
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { inputReceiverId = receiverId });
        }
    }
}
