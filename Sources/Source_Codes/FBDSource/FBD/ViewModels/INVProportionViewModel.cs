using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;

namespace FBD.ViewModels
{
    /// <summary>
    /// The view model is used to exchange data between View and Controller of INVProportion business process
    /// </summary>
    public class INVProportionViewModel
    {
        /// <summary>
        /// The list of individual basic index row to be displayed on the View
        /// </summary>
        public List<INVProportionRowViewModel> ProportionRows = new List<INVProportionRowViewModel>();

        /// <summary>
        /// The list of borrowing purpose to be binded to the drop down list
        /// </summary>
        public List<IndividualBorrowingPurposes> BorrowingPPs = new List<IndividualBorrowingPurposes>();

        /// <summary>
        /// The BorrowingPP id of selected BorrowingPP name
        /// </summary>
        public string BorrowingPPID;
    }
}
