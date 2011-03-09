using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;

namespace FBD.ViewModels
{
    public class INVCollateralIndexScoreViewModel
    {
        /// <summary>
        /// The list of business CollateralIndex to be binded to drop down list
        /// </summary>
        public List<IndividualCollateralIndex> CollateralIndex = new List<IndividualCollateralIndex>();

        /// <summary>
        /// The list of business BorrowingPP to be binded to drop down list
        /// </summary>
        //public List<IndividualBorrowingPurposes> BorrowingPP = new List<IndividualBorrowingPurposes>();


        /// <summary>
        /// The list or rows responsible for displaying information of financial index score
        /// </summary>
        public List<INVCollateralScoreRowViewModel> ScoreRows = new List<INVCollateralScoreRowViewModel>();

        /// <summary>
        /// The selected industry when selecting drop down list
        /// </summary>
        public string CollateralIndexID;

        /// <summary>
        /// The selected scale when selecting drop down list
        /// </summary>
        //public string BorrowingPPID;


    }
}
