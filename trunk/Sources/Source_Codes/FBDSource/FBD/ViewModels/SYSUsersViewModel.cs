using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;

namespace FBD.ViewModels
{
    public class SYSUsersViewModel
    {
        public SystemUsers SystemUsers { get; set; }
        public List<SystemUserGroups> SystemUserGroups { get; set; }
        public List<SystemBranches> SystemBranches { get; set; }
        public string GroupID { get; set; }
        public string GroupName { get; set; }
        public string BranchID { get; set; }
        public string BranchName { get; set; }
    }
}
