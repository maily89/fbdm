using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBD.ViewModels
{
    public class FIScoreRowViewModel
    {
        /// <summary>
        /// Indicates whether or not the row is checked by checkbox
        /// </summary>
        public bool Checked = false;

        /// <summary>
        /// The score id of the financial index score record got from data base
        /// </summary>
        public int ScoreID = -1;

        /// <summary>
        /// The level id corresponding with the financial index score
        /// </summary>
        public decimal LevelID;

        /// <summary>
        /// The lowest value in range
        /// </summary>
        public decimal FromValue;

        /// <summary>
        /// The highest value in range
        /// </summary>
        public decimal ToValue;

        /// <summary>
        /// The string value to type
        /// </summary>
        public string FixedValue;
    }
}
