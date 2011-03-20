using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;

namespace FBD.ViewModels
{
    /// <summary>
    /// Contains the list of non financial index score
    /// </summary>
    public class NFIScoreViewModel
    {
        /// <summary>
        /// The list of business industries to be binded to drop down list
        /// </summary>
        public List<BusinessIndustries> Industries = new List<BusinessIndustries>();

        /// <summary>
        /// The list of non-financial indexes to be binded to list
        /// </summary>
        public List<BusinessNonFinancialIndex> NonFinancialIndexes = new List<BusinessNonFinancialIndex>();

        /// <summary>
        /// The list or rows responsible for displaying information of non-financial index score
        /// </summary>
        public List<NFIScoreRowViewModel> ScoreRows = new List<NFIScoreRowViewModel>();

        /// <summary>
        /// The selected industry when selecting drop down list
        /// </summary>
        public string IndustryID;

        // The selected index when selecting drop down list
        public string IndexID;
    }
}
