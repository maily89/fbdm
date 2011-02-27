using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;

namespace FBD.ViewModels
{
    public class FIProportionViewModel
    {
        public List<FIProportionRowViewModel> ProportionRows = new List<FIProportionRowViewModel>();

        public List<BusinessIndustries> Industries = new List<BusinessIndustries>();

        public string IndustryID;
    }
}
