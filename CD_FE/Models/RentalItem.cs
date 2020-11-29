using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CD_FE.Models
{
    public class RentalItem
    {
        public int RentalItemId { get; set; }
        public int RentalId { get; set; }
        public int CDId { get; set; }

        //Add this property so we can use it in a drop-down box
        public IEnumerable<SelectListItem> CDs { get; set; }
    }
}