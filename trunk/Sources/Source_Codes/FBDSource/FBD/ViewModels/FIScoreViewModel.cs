using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;

namespace FBD.ViewModels
{
    /// <summary>
    /// Contains the list financial index score
    /// </summary>
    public class FIScoreViewModel
    {
        /// <summary>
        /// The list of business industries to be binded to drop down list
        /// </summary>
        public List<BusinessIndustries> Industries = new List<BusinessIndustries>();

        /// <summary>
        /// The list of business scales to be binded to drop down list
        /// </summary>
        public List<BusinessScales> Scales = new List<BusinessScales>();

        /// <summary>
        /// The list of financial indexes to be binded to list
        /// </summary>
        public List<BusinessFinancialIndex> FinancialIndexes = new List<BusinessFinancialIndex>();

        /// <summary>
        /// The list or rows responsible for displaying information of financial index score
        /// </summary>
        public List<FIScoreRowViewModel> ScoreRows = new List<FIScoreRowViewModel>();

        /// <summary>
        /// The selected industry when selecting drop down list
        /// </summary>
        public string IndustryID;

        /// <summary>
        /// The selected scale when selecting drop down list
        /// </summary>
        public string ScaleID;

        // The selected index when selecting drop down list
        public string IndexID;
    }
}
