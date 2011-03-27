using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBD.Models
{
    /// <summary>
    /// Class responsible for managing accessibility of application' actions
    /// </summary>
    public class AccessManager
    {
        /// <summary>
        /// The method is used to check whether or not a specified user can archieve an input action
        /// </summary>
        /// <param name="actionName">the action being handled</param>
        /// <param name="userGroup">the user doing action</param>
        /// <returns>decision of allowing archieve the input action</returns>
        public static bool AllowAccess(string actionName, object userID)
        {
            // If invalid input, not allow to access
            if (userID == null || string.IsNullOrEmpty(actionName))
            {
                return false;
            }

            FBDEntities FBDModel = new FBDEntities();

            var user = SystemUsers.SelectUserByID(userID.ToString());

            // Select all the System's Rights that available with input User Group
            List<SystemUserGroupsRights> lstRightsByGroup = SystemUserGroupsRights
                                                                .SelectSysGroupsRightsByGroup(FBDModel, user.SystemUserGroups.GroupID);

            // In each Right
            foreach (var right in lstRightsByGroup)
            {
                // If the action is belong to the authorization of the user, then allow to access
                if (actionName.Equals(right.SystemRights.RightID))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
