using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;

namespace FBD.ViewModels
{
    public class INVBasicIndexScoreViewModel
    {
        /// <summary>
        /// The list of business basicIndex to be binded to drop down list
        /// </summary>
        public List<IndividualBasicIndex> basicIndex = new List<IndividualBasicIndex>();

        /// <summary>
        /// The list of business BorrowingPP to be binded to drop down list
        /// </summary>
        public List<IndividualBorrowingPurposes> BorrowingPP = new List<IndividualBorrowingPurposes>();

       
        /// <summary>
        /// The list or rows responsible for displaying information of financial index score
        /// </summary>
        public List<INVBasicScoreRowViewModel> ScoreRows = new List<INVBasicScoreRowViewModel>();

        /// <summary>
        /// The selected industry when selecting drop down list
        /// </summary>
        public string basicIndexID;

        /// <summary>
        /// The selected scale when selecting drop down list
        /// </summary>
        public string BorrowingPPID;

        
    }
}
