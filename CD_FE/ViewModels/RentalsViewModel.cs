using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CD_FE.ViewModels
{
    /// <summary>
    /// View Models are models for View
    /// This rentals View Model will be used to show the list of Rentals with Staff Name instead of Staff ID
    /// </summary>
    public class RentalsViewModel
    {
        public int RentalId { get; set; }
        public DateTime DateRented { get; set; }
        public DateTime? DateReturned { get; set; }
        public string StaffFirstName { get; set; }
        public string StaffLastName { get; set; }
    }
}