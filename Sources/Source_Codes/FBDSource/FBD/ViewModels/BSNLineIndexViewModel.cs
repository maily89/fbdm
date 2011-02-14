using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;
namespace FBD.ViewModels
{
    public class BSNLineIndexViewModel
    {
        public List<BusinessIndustries> Industries { get; set; }
        public List<BusinessLines> Lines { get; set; }
        public string IndustryName { get; set; }
        public string IndustryID { get; set; }
    }
}
