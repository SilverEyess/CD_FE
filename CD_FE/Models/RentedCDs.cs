using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CD_FE.Models
{
    public class RentedCDs
    {
        public int RentalId { get; set; }
        public string CDTitle { get; set; }
        public string StaffFirstName { get; set; }
        public string StaffLastName { get; set; }
        public DateTime DateRented { get; set; }
    }
}