using CD_FE.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CD_FE.Controllers
{
    public class CDController : Controller
    {
        #region Index
        // GET: CD
        public ActionResult Index()
        {
            // We will request for a collection of CD data
            IEnumerable<CD> cds = WebClient.ApiRequest<CD>.GetEnumerable("CDs");
            // Pass the data to the View
            return View(cds);
        }
        #endregion

        #region Details
        // GET: CD/Details/5
        public ActionResult Details(int id)
        {
            // We will request for a single record of CD based on the primary key ID as specified in the parameter.
            var cd = WebClient.ApiRequest<CD>.GetSingleRecord($"CDs/{id}");
            // Pass the data to the view
            return View(cd);
        }
        #endregion

        #region Create
        // GET: CD/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CD/Create
        [HttpPost]
        public ActionResult Create(CD cd)
        {
            try
            {
                WebClient.ApiRequest<CD>.Post("CDs", cd);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Edit
        // GET: CD/Edit/5
        public ActionResult Edit(int id)
        {
            // We will request for a single record of CD based on the primary key ID as specified in the parameter.
            var cd = WebClient.ApiRequest<CD>.GetSingleRecord($"CDs/{id}");
            // Pass the data to the view
            return View(cd);
        }

        // POST: CD/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CD cd)
        {
            try
            {
                if (WebClient.ApiRequest<CD>.Put($"CDs/{id}", cd))
                    return RedirectToAction("Index");
                return View(cd);
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Delete
        // GET: CD/Delete/5
        public ActionResult Delete(int id)
        {
            // We will request for a single record of CD based on the primary key ID as specified in the parameter.
            var cd = WebClient.ApiRequest<CD>.GetSingleRecord($"CDs/{id}");
            // Pass the data to the view
            return View(cd);
        }

        // POST: CD/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                WebClient.ApiRequest<CD>.Delete($"CDs/{id}");

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region FileDrop
        [HttpPost]
        public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var file in files)
            {
                // Save file in filepath as the filename given from the original file
                file.SaveAs(Path.Combine(Server.MapPath("~/UploadedFiles"), file.FileName));
            }

            return Json("File uploaded successfully.");
        }
        #endregion

    }
}
