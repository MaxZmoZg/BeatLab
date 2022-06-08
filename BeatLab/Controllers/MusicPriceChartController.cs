using System.Linq;
using System.Web.Mvc;

namespace BeatLab.Controllers
{
    public class MusicPriceChartController : Controller
    {
        private readonly BeatLabDBEntities db = new BeatLabDBEntities();

        // GET: MusicPriceChart
        public ActionResult Index(int id)
        {
            var prices = db.Music.Find(id).Price_Music.ToList();
            return View(prices);
        }
    }
}