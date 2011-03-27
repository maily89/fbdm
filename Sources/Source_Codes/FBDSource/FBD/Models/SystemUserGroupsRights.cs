using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.ViewModels;

namespace FBD.Models
{
    
    public partial class SystemUserGroupsRights 
    {

        /// <summary>
        /// Select all records in SystemUserGroupsRights which has GroupID = groupID
        /// </summary>
        /// <param name="entities">The Model of Entities Framework</param>
        /// <param name="groupID">groupID</param>
        /// <returns>List of all GroupsRights</returns>
        public static List<SystemUserGroupsRights> SelectSysGroupsRightsByGroup(FBDEntities entities, string groupID)
        {
            
            List<SystemUserGroupsRights> lstRightsByGroup = entities.SystemUserGroupsRights
                                                                    .Include("SystemRights")
                                                                    .Include("SystemUserGroups")
                                                                    .Where(i => i.SystemUserGroups
                                                                                 .GroupID.Equals(groupID))
                                                                    .ToList();
            return lstRightsByGroup;
        }

        /// <summary>
        /// Select single GroupsRights with specified id
        /// </summary>
        /// <param name="entities">The Model of Entities Framework</param>
        /// <param name="id">ID</param>
        /// <returns>A GroupsRights with id = ID</returns>
        public static SystemUserGroupsRights SelectSysGroupRightByID(FBDEntities entities, int id)
        {
            SystemUserGroupsRights rightbyID = entities.SystemUserGroupsRights
                                                       .Include("SystemRights")
                                                       .Include("SystemUserGroups")
                                                       .First(i => i.ID == id);
            return rightbyID;
        }

        /// <summary>
        /// Add new record to SystemUserGroupsRights
        /// </summary>
        /// <param name="entities">The Model of Entities Framework</param>
        /// <param name="viewModel">The View model contains a GroupID and list of right for this Group</param>
        /// <param name="row">The row View Model contains data of 1 Right</param>
        /// <returns>
        /// 1: if OK
        /// 0: if ERROR
        /// </returns>
        private static int AddGroupRight(FBDEntities entities, SYSUserGroupsRightsViewModel viewModel, SYSUserGroupsRightsRowViewModel row)
        {
            SystemUserGroupsRights groupRight = new SystemUserGroupsRights();

            SystemUserGroups group = SystemUserGroups.SelectUserGroupByID(viewModel.GroupID, entities);
            if (group == null) 
            { 
                throw new Exception(); 
            }

            SystemRights right = SystemRights.SelectRightsByID(row.RightID, entities);
            if (right == null)
            {
                throw new Exception();
            }

            groupRight.SystemUserGroups = group;
            groupRight.SystemRights = right;

            entities.AddToSystemUserGroupsRights(groupRight);
            int result = entities.SaveChanges();
            
            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// Delete a record from SystemUserGroupsRights
        /// </summary>
        /// <param name="entities">The Model of Entities Framework</param>
        /// <param name="id">ID</param>
        /// <returns>
        /// 1: if OK
        /// 0: if ERROR
        /// </returns>
        private static int DeleteGroupRight(FBDEntities entities, int id)
        {
            SystemUserGroupsRights groupRight = new SystemUserGroupsRights();
            groupRight = SelectSysGroupRightByID(entities,id);
            entities.DeleteObject(groupRight);
            int result = entities.SaveChanges();

            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// Edit (add/delete) multiple records to SystemUserGroupsRights
        /// </summary>
        /// <param name="entities">The Model of Entities Framework</param>
        /// <param name="viewModel">The View model contains a GroupID and list of right for this Group</param>
        /// <returns>
        /// 1: if OK
        /// 0: if ERROR
        /// </returns>
        public static string EditMultiGroupRights(FBDEntities entities, SYSUserGroupsRightsViewModel viewModel)
        {
            string errorIndex = "";
            try
            {
                foreach (var row in viewModel.LstGroupRightRows)
                {
                    errorIndex = row.RightID;

                    if (row.Checked == true)
                    {
                        if (row.GroupRightID < 0)
                        {
                            AddGroupRight(entities, viewModel, row);
                        }
                    }
                    else
                    {
                        if (row.GroupRightID >= 0)
                            DeleteGroupRight(entities, row.GroupRightID);
                    }
                }

            }
            catch (Exception)
            {
                return errorIndex;
            }
            return null;
        }

        /// <summary>
        /// Create new View Model contain GroupID and list of right for this Group
        /// </summary>
        /// <param name="entities">The Model of Entities Framework</param>
        /// <param name="groupID">ID of the selected group</param>
        /// <returns>
        /// 1: if OK
        /// 0: if ERROR
        /// </returns>
        public static SYSUserGroupsRightsViewModel CreateViewModelbyGroup(FBDEntities entities, string groupID)
        {
            List<SystemUserGroupsRights> lstGroupRightsByGroup = new List<SystemUserGroupsRights>();

            List<SystemRights> lstRights = new List<SystemRights>();

            SYSUserGroupsRightsViewModel viewModelResult = new SYSUserGroupsRightsViewModel();

            lstGroupRightsByGroup = SelectSysGroupsRightsByGroup(entities, groupID);
            lstRights = entities.SystemRights.OrderBy(i => i.RightID).ToList();

            foreach(var index in lstRights)
            {
                SYSUserGroupsRightsRowViewModel viewModelRow = new SYSUserGroupsRightsRowViewModel();

                viewModelRow.RightID = index.RightID;
                viewModelRow.RightName = index.RightName;

                viewModelResult.LstGroupRightRows.Add(viewModelRow);
            }

            foreach(var item in lstGroupRightsByGroup)
            {
                foreach(var row in viewModelResult.LstGroupRightRows)
                {
                    if(item.SystemRights.RightID.Equals(row.RightID))
                    {
                        row.Checked = true;
                        row.GroupRightID = item.ID;
                        break;
                    }
                }
            }

            viewModelResult.LstUserGroups = entities.SystemUserGroups.ToList();
            viewModelResult.GroupID = groupID;

            return viewModelResult;
        }
    }
}
