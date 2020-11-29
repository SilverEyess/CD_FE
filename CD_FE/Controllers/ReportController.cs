using CD_FE.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CD_FE.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult GetRentalCountData()
        {
            IEnumerable<CDRentalCountReport> cdrentalCountReport = WebClient.ApiRequest<CDRentalCountReport>.GetEnumerable("Reports/GetCDRentalCountReport");

            return View(cdrentalCountReport.OrderBy(cd => cd.CDTitle));
        }

        public ActionResult DrawCDRentalCountChart()
        {
            IEnumerable<CDRentalCountReport> cdrentalCountReport = WebClient.ApiRequest<CDRentalCountReport>.GetEnumerable("Reports/GetCDRentalCountReport");
            return View(cdrentalCountReport);
        }

        [HttpGet]
        public ActionResult GetRentedCDReport(string criteria)
        {
            IEnumerable<RentedCDs> rentedCDS = WebClient.ApiRequest<RentedCDs>.GetEnumerable("Reports/GetRentedCDs");
            if (string.IsNullOrEmpty(criteria))
            {
                TempData["RentedCDs"] = rentedCDS;
                return View(rentedCDS);
            }
            else
            {
                rentedCDS = rentedCDS.Where(cd => cd.CDTitle.ToLower().Contains(criteria.ToLower()) ||
                cd.StaffFirstName.ToLower().Contains(criteria.ToLower()) || cd.StaffLastName.ToLower().Contains(criteria.ToLower())).ToList();
                TempData["RentedCDs"] = rentedCDS;
                return View(rentedCDS);
            }
        }

        public void ExportRentedCDData()
        {
            List<RentedCDs> rentedCDs = TempData["RentedCDs"] as List<RentedCDs>;
            StringWriter sw = new StringWriter();
            sw.WriteLine("\"RentalID\",\"Title\",\"StaffFirstName\",\"StaffLastName\",\"DateRented\"");
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=RentedCDs.csv");
            Response.ContentType = "application/octet-stream";

            foreach (var rentedCD in rentedCDs)
            {
                sw.WriteLine($"{rentedCD.RentalId}, {rentedCD.CDTitle}, {rentedCD.StaffFirstName}, {rentedCD.StaffLastName}, {rentedCD.DateRented}");
            }
            Response.Write(sw.ToString());
            Response.End();
        }
    }
}