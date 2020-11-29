using CD_FE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CD_FE.ViewModels
{
    /// <summary>
    /// View Models are models for a View
    /// This Rental Details View Model will be used when we select the Details button of the Index page to display details of the Rental.
    /// </summary>
    public class RentalDetailsViewModel
    {
        public Rental Rental { get; set; }
        public string StaffFirstName { get; set; }
        public string StaffLastName { get; set; }
        public IEnumerable<CDsViewModel> RentedCDs { get; set; }
    }
}