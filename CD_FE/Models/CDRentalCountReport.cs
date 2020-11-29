using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CD_FE.Models
{
    public class CDRentalCountReport
    {
        public int CDId { get; set; }
        public string CDTitle { get; set; }
        public Nullable<int> RentalCount { get; set; }
    }
}