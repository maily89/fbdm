using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;

namespace FBD.ViewModels
{
    /// <summary>
    /// The view model is used to exchange data between View and Controller of FIProportion business process
    /// </summary>
    public class FIProportionViewModel
    {
        /// <summary>
        /// The list of financial index row to be displayed on the View
        /// </summary>
        public List<FIProportionRowViewModel> ProportionRows = new List<FIProportionRowViewModel>();

        /// <summary>
        /// The list of industries to be binded to the drop down list
        /// </summary>
        public List<BusinessIndustries> Industries = new List<BusinessIndustries>();

        /// <summary>
        /// The industry id of selected industry name
        /// </summary>
        public string IndustryID;
    }
}
