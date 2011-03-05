using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBD.ViewModels
{
    public class SYSUserGroupsRightsRowViewModel
    {
        /// <summary>
        /// TRUE: the checkbox will be checked 
        ///       Represent: The selected User Group has this Right
        /// FALSE: the checkbox will be unchecked
        ///       Represent: The selected User Group does not have this Right
        /// </summary>
        public bool Checked = false;

        /// <summary>
        /// = -1: Virtual ID, does not exist in the SystemUserGroupsRights table
        ///       Represent: The selected User Group does not have this Right
        /// != -1: The ID of the row contain: GroupID, RightID
        ///       Represent: The selected User Group has this Right
        /// </summary>
        public int GroupRightID = -1;

        /// <summary>
        /// RightID get from SystemRights
        /// </summary>
        public string RightID;

        /// <summary>
        /// RightName get from SystemRights
        /// </summary>
        public string RightName;
    }
}