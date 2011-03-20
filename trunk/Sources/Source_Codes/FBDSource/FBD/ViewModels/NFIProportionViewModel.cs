using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;

namespace FBD.ViewModels
{
    /// <summary>
    /// Contains the list of non financial index proportion
    /// </summary>
    public class NFIProportionViewModel
    {
        /// <summary>
        /// The list of non-financial index row to be displayed on the View
        /// </summary>
        public List<NFIProportionRowViewModel> ProportionRows = new List<NFIProportionRowViewModel>();

        /// <summary>
        /// The list of industries to be binded to the drop down list
        /// </summary>
        public List<BusinessIndustries> Industries = new List<BusinessIndustries>();

        /// <summary>
        /// The industry id of selected industry name
        /// </summary>
        public string IndustryID;

        /// <summary>
        /// The list of business types to be binded to the drop down list
        /// </summary>
        public List<BusinessTypes> Types = new List<BusinessTypes>();

        /// <summary>
        /// The type id of selected type name
        /// </summary>
        public string TypeID;
    }
}
