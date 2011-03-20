using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBD.ViewModels
{
    /// <summary>
    /// Represent each row of non financial index proportion
    /// </summary>
    public class NFIProportionRowViewModel
    {
        /// <summary>
        /// The property to be displayed to the checkbox
        /// True value indicates the checkbox is checked
        /// As default, all the checkbox is unchecked
        /// </summary>
        public bool Checked = false;

        /// <summary>
        /// The field is got from table BusinessNFIProportionByType or BusinessNFIProportionByIndustry
        /// to specify the primary key of the proportion
        /// </summary>
        public int ProportionID = -1;

        /// <summary>
        /// Index ID of the non-financial index
        /// </summary>
        public string IndexID;

        /// <summary>
        /// Index Name of the non-financial index
        /// </summary>
        public string IndexName;

        /// <summary>
        /// The leaf index indicates whether or not the non-financial index is used to calculate
        /// </summary>
        public bool LeafIndex;

        /// <summary>
        /// The proportion of the non-financial index with specified industry or business type
        /// </summary>
        public decimal Proportion;
    }
}
