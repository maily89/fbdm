using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;

namespace FBD.ViewModels
{
    public class SYSUserGroupsRightsViewModel
    {
        /// <summary>
        /// List of User Group, will be displayed in Drop Down List
        /// </summary>
        public List<SystemUserGroups> LstUserGroups = new List<SystemUserGroups>();
        
        /// <summary>
        /// List of User Right, will be filtered by User Group and displayed in the below table
        /// </summary>
        public List<SystemRights> LstRights = new List<SystemRights>();

        public List<SYSUserGroupsRightsRowViewModel> LstGroupRightRows = new List<SYSUserGroupsRightsRowViewModel>();
        
        /// <summary>
        /// ID of the selected Group from the Drop Down List
        /// </summary>
        /// 
        public string GroupID;
    }
}
