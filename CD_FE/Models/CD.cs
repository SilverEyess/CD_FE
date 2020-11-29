using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CD_FE.Models
{
    public class CD
    {
        public int CDId { get; set; }
        public string CDTitle { get; set; }
        public string CDAuthor { get; set; }
        public string Section { get; set; }
        public string X { get; set; }
        public string Y { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public string OnLoan { get; set; }
        public string PicFileName { get; set; }
    }
}