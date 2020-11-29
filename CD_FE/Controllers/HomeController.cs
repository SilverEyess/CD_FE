using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CD_FE.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "CD Archive description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact CD Archive Developers.";

            return View();
        }
    }
}