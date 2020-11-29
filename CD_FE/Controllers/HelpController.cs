using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CD_FE.Controllers
{
    public class HelpController : Controller
    {
        #region Index
        // GET: Help
        public ActionResult Index()
        {
            return View();
        }
        #endregion

    }
}