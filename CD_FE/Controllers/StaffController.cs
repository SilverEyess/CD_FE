using CD_FE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CD_FE.Controllers
{
    public class StaffController : Controller
    {
        #region Index
        // GET: Staff
        public ActionResult Index()
        {
            // We will request for a collection of CD data
            IEnumerable<Staff> staffs = WebClient.ApiRequest<Staff>.GetEnumerable("Staffs");
            // Pass the data to the View
            return View(staffs);
        }
        #endregion

        #region Details
        // GET: Staff/Details/5
        public ActionResult Details(int id)
        {
            // We will request for a single record of CD based on the primary key ID as specified in the parameter.
            var staff = WebClient.ApiRequest<Staff>.GetSingleRecord($"Staffs/{id}");
            // Pass the data to the view
            return View(staff);
        }
        #endregion

        #region Create
        // GET: Staff/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Staff/Create
        [HttpPost]
        public ActionResult Create(Staff staff)
        {
            try
            {
                WebClient.ApiRequest<Staff>.Post("Staffs", staff);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Edit
        // GET: Staff/Edit/5
        public ActionResult Edit(int id)
        {
            // We will request for a single record of CD based on the primary key ID as specified in the parameter.
            var staff = WebClient.ApiRequest<Staff>.GetSingleRecord($"Staffs/{id}");
            // Pass the data to the view
            return View(staff);
        }

        // POST: Staff/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Staff staff)
        {
            try
            {
                if (WebClient.ApiRequest<Staff>.Put($"Staffs/{id}", staff))
                    return RedirectToAction("Index");
                return View(staff);
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Delete
        // GET: Staff/Delete/5
        public ActionResult Delete(int id)
        {
            // We will request for a single record of CD based on the primary key ID as specified in the parameter.
            var staff = WebClient.ApiRequest<Staff>.GetSingleRecord($"Staffs/{id}");
            // Pass the data to the view
            return View(staff);
        }

        // POST: Staff/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                WebClient.ApiRequest<Staff>.Delete($"Staffs/{id}");

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion

    }
}
