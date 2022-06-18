using System.Linq;
using System.Web.Mvc;
using BeatLab.Models.Entities;


namespace BeatLab.Controllers
{
    public class PluginPriceChartController : Controller
    {
        private readonly BeatLabDBEntities db = new BeatLabDBEntities();

        // GET: PluginPriceChart
        public ActionResult Index(int id)
        {
            var prices = db.Plugins.Find(id).Price_Plugin.ToList();
            return View(prices);
        }
    }
}