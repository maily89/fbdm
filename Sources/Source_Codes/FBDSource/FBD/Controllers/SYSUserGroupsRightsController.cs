using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.ViewModels;


namespace FBD.Controllers
{
    public class SYSUserGroupsRightsController : Controller
    {
        //
        // GET: /SYSDecentralization/

        public ActionResult Index( string GroupID)
        {
            var userGroup = SystemUserGroups.SelectUserGroups();
            var model = new SYSUserGroupsRightsViewModel();
            model.UserGroups = userGroup;

            if (GroupID != null)
            {
                var group = SystemUserGroups.SelectUserGroupByID(GroupID);
                model.GroupID = group.GroupID;
                model.GroupName = group.GroupName;
                group.SystemUserGroupsRights.Load();
                //model.Rights = group.SystemUserGroupsRights;
            }

            var temp = SystemRights.SelectRights();
            model.Rights = temp;
            
            return View(model);
        }        
    }
}
