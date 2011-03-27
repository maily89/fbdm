using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.ViewModels;
using FBD.CommonUtilities;

namespace FBD.Controllers
{
    public class SYSUserGroupsRightsController : Controller
    {
        //
        // GET: /SYSDecentralization/

        /// <summary>
        /// Use Logic class to create ViewModel combine data from two data groups: 
        ///     - List of Groups (from SystemUserGroups table) and 
        ///     - List of Rights (from SystemUserGroupsRights table)
        /// Display list of User Groups in Drop down list
        /// Display list of Rights in the table
        /// </summary>
        /// <returns>Return View with the newly created View Model</returns>
        public ActionResult Index ()
        {
            FBDEntities entities = new FBDEntities();
            SYSUserGroupsRightsViewModel viewModel = new SYSUserGroupsRightsViewModel();
            try
            {
                List<SystemUserGroups> lstGroups = new List<SystemUserGroups>();
                lstGroups = SystemUserGroups.SelectUserGroups();
                if (lstGroups.Count() < 1)
                    return View(viewModel);
                viewModel = SystemUserGroupsRights.CreateViewModelbyGroup(entities, lstGroups[0].GroupID);
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.SYSTEM_LIST_RIGHTS);
                return View(viewModel);
            }
            return View(viewModel);
        }


        /// <summary>
        /// There are two kinds of action:
        ///     - Action from formCollection["UserGroup"] : Select User Group with GroupID from Drop down list
        ///     - Action from formCollection["Save"] : Click Save button and list of rights for the GroupID will be updated
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns>View("Index")</returns>
        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            FBDEntities entities = new FBDEntities();
            try
            {
                if (formCollection["UserGroup"] != null)
                {
                    SYSUserGroupsRightsViewModel viewModelForGroupDDL = SystemUserGroupsRights
                                                                        .CreateViewModelbyGroup(
                                                                            entities,
                                                                            formCollection["UserGroup"].ToString());
                    return View(viewModelForGroupDDL);
                }
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.SYSTEM_LIST_RIGHTS);
                return RedirectToAction("Index");
            }

            try
            {
                if (formCollection["Save"] != null)
                {
                    SYSUserGroupsRightsViewModel viewModelForSaving = new SYSUserGroupsRightsViewModel();
                    viewModelForSaving.GroupID = formCollection["GroupID"].ToString();
                    for (int i = 0; i < int.Parse(formCollection["NumberOfRightRows"].ToString()); i++)
                    {
                        SYSUserGroupsRightsRowViewModel rowModelForSaving = new SYSUserGroupsRightsRowViewModel();
                        if (formCollection["LstGroupRightRows[" + i + "].Checked"] != null)
                        {
                            if (formCollection["LstGroupRightRows[" + i + "].Checked"].ToString().Equals("true,false")

                             || formCollection["LstGroupRightRows[" + i + "].Checked"].ToString().Equals("True,False")

                             || formCollection["LstGroupRightRows[" + i + "].Checked"].ToString().Equals("TRUE,FALSE"))
                            {
                                rowModelForSaving.Checked = true;
                            }
                        }

                        rowModelForSaving.RightID = formCollection["LstGroupRightRows[" + i + "].RightID"].ToString();
                        rowModelForSaving.RightName = formCollection["LstGroupRightRows[" + i + "].RightName"].ToString();

                        rowModelForSaving.GroupRightID = int.Parse(formCollection["LstGroupRightRows[" + i + "].GroupRightID"].ToString());
                        viewModelForSaving.LstGroupRightRows.Add(rowModelForSaving);
                    }

                    string errorIndex = SystemUserGroupsRights.EditMultiGroupRights(entities, viewModelForSaving);

                    SYSUserGroupsRightsViewModel viewModelAfterEditing = SystemUserGroupsRights.CreateViewModelbyGroup(entities, formCollection["GroupID"].ToString());

                    if (errorIndex != null) //If there is some error 
                    {
                        TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.SYSTEM_LIST_RIGHTS);
                        return View(viewModelAfterEditing);
                    }
                    else //If OK
                    {
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST, Constants.SYSTEM_LIST_RIGHTS, viewModelForSaving.GroupID);
                        return View(viewModelAfterEditing);
                    }

                }
                }
                catch (Exception)
                {
                    TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.SYSTEM_LIST_RIGHTS);
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
           // return View(new SYSUserGroupsRightsViewModel());
        }
    }
}
