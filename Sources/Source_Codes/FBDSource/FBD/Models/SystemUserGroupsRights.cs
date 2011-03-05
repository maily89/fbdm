using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.ViewModels;

namespace FBD.Models
{
    
    public partial class SystemUserGroupsRights 
    {
        
        public static List<SystemUserGroupsRights> SelectSysGroupsRightsByGroup(FBDEntities entities, string groupID)
        {
            
            List<SystemUserGroupsRights> lstRightsByGroup = entities.SystemUserGroupsRights
                                                                    .Include("SystemRights")
                                                                    .Where(i => i.SystemUserGroups
                                                                                 .GroupID.Equals(groupID))
                                                                    .ToList();
            return lstRightsByGroup;
        }

        /// <summary>
        /// Select single record in SystemUserGroupsRights with specified id
        /// </summary>
        /// <param name="entities">The Model of Entities Framework</param>
        /// <param name="id">The id as primary key in SystemUserGroupsRights</param>
        /// <returns>a record in SystemUserGroupsRights, has ID = id parameter</returns>
        public static SystemUserGroupsRights SelectSysGroupRightByID(FBDEntities entities, int id)
        {
            SystemUserGroupsRights rightbyID = entities.SystemUserGroupsRights
                                                       .First(i => i.ID == id);
            return rightbyID;
        }

        public static int AddGroupRight(FBDEntities entities, SYSUserGroupsRightsViewModel viewModel, SYSUserGroupsRightsRowViewModel row)
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

        public static int DeleteGroupRight(FBDEntities entities, int id)
        {
            SystemUserGroupsRights groupRight = new SystemUserGroupsRights();
            groupRight = entities.SystemUserGroupsRights
                                 .First(i => i.ID == id);
            entities.DeleteObject(groupRight);
            int result = entities.SaveChanges();

            return result <= 0 ? 0 : 1;
        }

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
