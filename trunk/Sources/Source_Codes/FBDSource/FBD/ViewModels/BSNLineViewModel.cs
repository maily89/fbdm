using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;

namespace FBD.ViewModels
{
    public class BSNLineViewModel
    {
        public BusinessLines BusinessLines { get; set; }
        public List<BusinessIndustries> BusinessIndustries { get; set; }
        public string IndustryID { get; set; }
    }
}
