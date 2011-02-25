using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;

namespace FBD.ViewModels
{
    public class SYSUsersIndexViewModel
    {
        public List<SystemUserGroups> Groups { get; set; }
        public List<SystemBranches> Branches { get; set; }
        public List<SystemUsers> Users { get; set; }
        public string GroupID { get; set; }
        public string GroupName { get; set; }
        public string BranchID { get; set; }
        public string BranchName { get; set; }
    }
}
