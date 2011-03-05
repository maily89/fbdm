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
                TempData[Constants.ERR_MESSAGE] = Constants.ERR_DISPLAY_SYS_GROUP_RIGHT;
                return View(viewModel);
            }
            return View(viewModel);
        }


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
                TempData[Constants.ERR_MESSAGE] = Constants.ERR_DISPLAY_SYS_GROUP_RIGHT;
                return RedirectToAction("Index");
            }

            //try
            //{
                if (formCollection["Save"] != null)
                {
                    SYSUserGroupsRightsViewModel viewModelForSaving = new SYSUserGroupsRightsViewModel();
                    for (int i = 0; i < int.Parse(formCollection["NumberOfRightRows"].ToString()); i++)
                    {
                        SYSUserGroupsRightsRowViewModel rowModelForSaving = new SYSUserGroupsRightsRowViewModel();
                        if (formCollection["RightRows[" + i + "].Checked"] != null)
                        {
                            if (formCollection["RightRows[" + i + "].Checked"].ToString().Equals("true,false")
                                )
                             //|| formCollection["RightRows[" + i + "].Checked"].ToString().Equals("True,False")
                             //|| formCollection["RightRows[" + i + "].Checked"].ToString().Equals("TRUE,FALSE"))
                            {
                                rowModelForSaving.Checked = true;
                            }
                        }

                        rowModelForSaving.RightID = formCollection["RightRows[" + i + "].RightID"].ToString();
                        rowModelForSaving.RightName = formCollection["RightRows[" + i + "].Right"].ToString();

                        rowModelForSaving.GroupRightID = int.Parse(formCollection["RightRows" + i + "RightID"].ToString());
                        viewModelForSaving.LstGroupRightRows.Add(rowModelForSaving);
                    }
                    viewModelForSaving.GroupID = formCollection["GroupID"].ToString();

                    string errorIndex = SystemUserGroupsRights.EditMultiGroupRights(entities, viewModelForSaving);

                    SYSUserGroupsRightsViewModel viewModelAfterEditing = SystemUserGroupsRights.CreateViewModelbyGroup(entities, formCollection["GroupID"].ToString());

                    if (errorIndex != null)
                    {
                        TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_UPDATE_SYS_GROUP_RIGHT, errorIndex);
                        return View(viewModelAfterEditing);
                    }
                    else
                    {
                        TempData[Constants.ERR_MESSAGE] = Constants.SCC_UPDATE_SYS_GROUP_RIGHT;
                    }

                }
            //}
            //catch (Exception)
            //{
            //    TempData[Constants.ERR_MESSAGE] = Constants.ERR_POST_SYS_GROUP_RIGHT;
            //    return RedirectToAction("Index");
            //}

            return View(new SYSUserGroupsRightsViewModel());
        }
    }
}
