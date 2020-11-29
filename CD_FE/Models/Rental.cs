using CD_FE.Controllers;
using CD_FE.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CD_FE.Models
{
    public class Rental
    {
        public int RentalId { get; set; }
        public int StaffId { get; set; }
        public DateTime DateRented { get; set; }
        public DateTime? DateReturned { get; set; }
        public virtual ICollection<RentalItem> RentalItems { get; set; }
        // Add this property so we can use it in a drop-down box
        public IEnumerable<SelectListItem> Staffs { get; set; }
        // This property is to store the collection of CDs included in this rental
        public IEnumerable<CDsViewModel> RentedCDs { get; set; }

    }
}