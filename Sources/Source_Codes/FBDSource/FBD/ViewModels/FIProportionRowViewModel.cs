using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FBD.ViewModels
{
    /// <summary>
    /// The class represents for a row to be displayed on the list proportion of the FIProportion View
    /// </summary>
    [MetadataType(typeof(FIProportionRowViewModelMetaData))]
    public class FIProportionRowViewModel
    {
        /// <summary>
        /// The property to be displayed to the checkbox
        /// True value indicates the checkbox is checked
        /// As default, all the checkbox is unchecked
        /// </summary>
        public bool Checked = false;

        /// <summary>
        /// The field is got from table BusinessFinancialIndexProportion
        /// to specify the primary key of the proportion
        /// </summary>
        public int ProportionID = -1;

        /// <summary>
        /// Index ID of the financial index
        /// </summary>
        public string IndexID;

        /// <summary>
        /// Index Name of the financial index
        /// </summary>
        public string IndexName;

        /// <summary>
        /// The leaf index indicates whether or not the financial index is used to calculate
        /// </summary>
        public bool LeafIndex;
        
        /// <summary>
        /// The proportion of the financial index with specified industry
        /// </summary>
        public decimal Proportion;

        /// <summary>
        /// Metadata class to validate the proportion
        /// </summary>
        public class FIProportionRowViewModelMetaData
        {
            public decimal Proportion;
        }
    }
}
