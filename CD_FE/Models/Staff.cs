using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CD_FE.Models
{
    public class Staff
    {
        public int StaffId { get; set; }
        public string StaffFirstName { get; set; }
        public string StaffLastName { get; set; }
        public string StaffPhone { get; set; }
        public string StaffAddress { get; set; }
        public string StaffEmail { get; set; }
        public string StaffActive { get; set; }
    }
}