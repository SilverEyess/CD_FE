using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CD_FE.ViewModels
{
    /// <summary>
    /// View Models are models for View
    /// This CDs View model will be used to display the list of rented CDs of the Rental form to show the CD name instead of the CD ID.
    /// </summary>
    public class CDsViewModel
    {
        public int RentalItemId { get; set; }
        public int RentalId { get; set; }
        public string CDTitle { get; set; }
        public string CDAuthor { get; set; }
    }
}