using BeatLab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BeatLab.Controllers
{
    public class HomeController : Controller
    {
        private BeatLabDBEntities db = new BeatLabDBEntities();
        public ActionResult Index()
        {
            var music = db.Music.Include(m => m.Alboms).Include(m => m.Genere_Of_Music).Include(m => m.Type_Music).Include(m => m.User);
            return View(music.ToList());
        }
    
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Authorize]
        public ActionResult PersonalArea()
        {
         
            return View();
        }
        
       
    }
}