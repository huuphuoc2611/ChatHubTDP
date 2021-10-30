using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace ChatHubTDP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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
        public ActionResult Team()
        {
            ViewBag.Message = "Your contact page.";

            return View("Team");
        }
        public ActionResult Prices()
        {
            ViewBag.Message = "Your contact page.";

            return View("Prices");
        }
        public ActionResult Bot()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Hdan()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ListSp()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}