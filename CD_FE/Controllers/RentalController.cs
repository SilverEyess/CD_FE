
using CD_FE.Models;
using CD_FE.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CD_FE.Controllers
{
    public class RentalController : Controller
    {
        #region Index

        // GET: Rental
        public ActionResult Index()
        {
            
            // To get all the Rental records
            IEnumerable<Rental> rentals = WebClient.ApiRequest<Rental>.GetEnumerable("Rentals");
            // To get all the Staff records so we can shape our View Model
            IList<Staff> staffs = WebClient.ApiRequest<Staff>.GetList("Staffs");

            // Shape our RentalViewModel
            var rentalsViewModel = rentals.Select(
                r => new RentalsViewModel
            {
                RentalId = r.RentalId,
                DateRented = r.DateRented,
                DateReturned = r.DateReturned,
                StaffFirstName = staffs.Where(c => c.StaffId == r.StaffId).Select(n => n.StaffFirstName).FirstOrDefault(),
                StaffLastName = staffs.Where(c => c.StaffId == r.StaffId).Select(n => n.StaffLastName).FirstOrDefault()
                }).OrderByDescending(o => o.DateRented).ToList();

            return View(rentalsViewModel);

        }

        #endregion

        #region Details

        // GET: Rental/Details/5
        public ActionResult Details(int id)
        {
            var rental = WebClient.ApiRequest<Rental>.GetSingleRecord($"Rentals/{id}");
            IList<Staff> staffs = WebClient.ApiRequest<Staff>.GetList("Staffs");
            IList<CD> cds = WebClient.ApiRequest<CD>.GetList("CDs");

            // Shape our RentalDetailsViewModel
            var rentalDetails = new RentalDetailsViewModel
            {
                Rental = rental,
                StaffFirstName = staffs.Where(s => s.StaffId == rental.StaffId).Select(staf => staf.StaffFirstName).FirstOrDefault(),
                StaffLastName = staffs.Where(s => s.StaffId == rental.StaffId).Select(staf => staf.StaffLastName).FirstOrDefault(),
                RentedCDs = rental.RentalItems.Select(
                    ri => new CDsViewModel
                    {
                        RentalId = ri.RentalId,
                        CDTitle = cds.Where(c => c.CDId == ri.CDId).Select(cn => cn.CDTitle).FirstOrDefault(),
                        CDAuthor = cds.Where(c => c.CDId == ri.CDId).Select(cn => cn.CDAuthor).FirstOrDefault()
                    }).ToList()
            };

            return View(rentalDetails);
        }

        #endregion

        #region Create

        // GET: Rental/Create
        public ActionResult Create()
        {
            var rental = new Rental
            {
                DateRented = DateTime.Now,
                Staffs = GetStaffs(),
                RentedCDs = new List<CDsViewModel>()
            };

            return View(rental);
        }

        // POST: Rental/Create
        [HttpPost]
        public ActionResult Create(Rental rental)
        {
            try
            {
                rental = WebClient.ApiRequest<Rental>.Post("Rentals", rental);

                // We want to return to the Edit View of the rental so we can add rented movies or rental items
                return RedirectToAction("Edit", new { id = rental.RentalId });
            }
            catch
            {
                return View();
            }
        }

        #endregion

        #region Edit

        // GET: Rental/Edit/5
        public ActionResult Edit(int id)
        {
                        
            var rental = WebClient.ApiRequest<Rental>.GetSingleRecord($"Rentals/{id}");

            // To get the rental items based on the rental ID
            IList<RentalItem> rentalItems = WebClient.ApiRequest<RentalItem>.GetList($"RentalItemsById/{id}");

            // To get all CDs to populate our View Model
            IList<CD> cds = WebClient.ApiRequest<CD>.GetList("CDs");

            // to get all staff to populate our staff SelectIistItem
            rental.Staffs = GetStaffs();

            var rentedCDs = rentalItems.Select(
                ri => new CDsViewModel
                {
                    RentalItemId = ri.RentalItemId,
                    RentalId = ri.RentalId,
                    CDTitle = cds.Where(c => c.CDId == ri.CDId).Select(o => o.CDTitle).FirstOrDefault(),
                    CDAuthor = cds.Where(c => c.CDId == ri.CDId).Select(cn => cn.CDAuthor).FirstOrDefault()
                }).ToList();

            rental.RentedCDs = rentedCDs;

            return View(rental);
        }

        // POST: Rental/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Rental rental)
        {
            try
            {
                if (WebClient.ApiRequest<Rental>.Put($"Rentals/{id}", rental))
                    return RedirectToAction("Index");

                return View(rental);
            }
            catch
            {
                return View();
            }
        }

        #endregion

        #region Delete

        // GET: Rental/Delete/5
        public ActionResult Delete(int id)
        {
            // Get the Rental record based on the paremeter id
            var rental = WebClient.ApiRequest<Rental>.GetSingleRecord($"Rentals/{id}");
            // Another request to the Database to get the staff records
            IList<Staff> Staffs = WebClient.ApiRequest<Staff>.GetList("Staffs");
            // Another request to the database to get the CD records
            IList<CD> cds = WebClient.ApiRequest<CD>.GetList("CDs");

            // Shape our RentalDetailsViewModel
            var rentalDetails = new RentalDetailsViewModel
            {
                Rental = rental,
                StaffFirstName = Staffs.Where(c => c.StaffId == rental.RentalId)
                .Select(staf => staf.StaffFirstName).FirstOrDefault(),
                StaffLastName = Staffs.Where(c => c.StaffId == rental.RentalId)
                .Select(staf => staf.StaffLastName).FirstOrDefault(),
                RentedCDs = rental.RentalItems.Select(
                    ri => new CDsViewModel
                    {
                        RentalId = ri.RentalId,
                        CDTitle = cds.Where(c => c.CDId == ri.CDId).Select(cn => cn.CDTitle).FirstOrDefault(),
                        CDAuthor = cds.Where(c => c.CDId == ri.CDId).Select(cn => cn.CDAuthor).FirstOrDefault()

                    }).ToList()
            };
            return View(rentalDetails);
        }

        // POST: Rental/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {               
                WebClient.ApiRequest<Rental>.Delete($"Rentals/{id}");

                return RedirectToAction("Index");
            }                            
            catch
            {
                return View();
            }
        }

        #endregion

        #region CDs rented

        #region Add CDs

        // AddMovies - GET
        public ActionResult AddCDs(int rentalId)
        {
            var rentalItem = new RentalItem
            {
                RentalId = rentalId,
                CDs = GetCDs()
            };
            return View(rentalItem);
        }

        // AddMovies - POST
        [HttpPost]
        public ActionResult AddCDs(RentalItem rentalItem)
        {
            try
            {
                WebClient.ApiRequest<RentalItem>.Post("RentalItems", rentalItem);
                return RedirectToAction("Edit", new { id = rentalItem.RentalId });

            }
            catch
            {
                return View();
            }
        }

        #endregion

        #region Edit Rented CD

        // EditRentedCD - GET
        public ActionResult EditRentedCD(int id)
        {
            var rentalItem = WebClient.ApiRequest<RentalItem>
                .GetSingleRecord($"RentalItems/{id}");
            rentalItem.CDs = GetCDs();
            return View(rentalItem);
        }

        // EditRentedCD - POST
        [HttpPost]
        public ActionResult EditRentedCD(int id, RentalItem rentalItem)
        {
            try
            {
                if (WebClient.ApiRequest<RentalItem>.Put($"RentalItems/{id}", rentalItem))
                    return RedirectToAction("Edit", new { id = rentalItem.RentalId });

                return View(rentalItem);
            }
            catch
            {
                return View();
            }
        }

        #endregion

        #region Delete Rented CD

        // Delete - GET
        public ActionResult DeleteRentedCD(int id)
        {
            var rentalItem = WebClient.ApiRequest<RentalItem>.GetSingleRecord($"RentalItems/{id}");
            rentalItem.CDs = GetCDs();
            return View(rentalItem);
        }

        // Delete - POST
        [HttpPost]
        public ActionResult DeleteRentedCD(int id, RentalItem rentalItem)
        {
            try
            {
                rentalItem = WebClient.ApiRequest<RentalItem>.Delete($"RentalItems/{id}");
                return RedirectToAction("Edit", new { id = rentalItem.RentalId });
            }
            catch
            {
                return View();
            }
        }

        #endregion

        #endregion

        #region Helper Methods

        public IEnumerable<SelectListItem> GetCDs()
        {
            IList<CD> dbCDs = WebClient.ApiRequest<CD>.GetList("CDs");

            List<SelectListItem> cds = dbCDs.OrderBy(m => m.CDTitle).Select(o => new SelectListItem
            {
                Value = o.CDId.ToString(),
                Text = $"{o.CDTitle} - {o.CDAuthor}"
            }).ToList();

            return new SelectList(cds, "Value", "Text");
        }

        private IEnumerable<SelectListItem> GetStaffs()
        {
            // Store the staff records to a list.
            IList<Staff> dbStaffs = WebClient.ApiRequest<Staff>.GetList("Staffs");
            // Create and populate a list of SelectListItem
            // The value property will store the primary key of the staff
            // The text1 property will store the Staff Name that will be displayed in our drop-down
            List<SelectListItem> staffs = dbStaffs
                .OrderBy(o => o.StaffFirstName)
                .Select(c => new SelectListItem
                {
                    Value = c.StaffId.ToString(),
                    Text = $"{c.StaffFirstName} {c.StaffLastName}"
                }).ToList();

            return new SelectList(staffs, "Value", "Text");
        }

        #endregion

    }
}
