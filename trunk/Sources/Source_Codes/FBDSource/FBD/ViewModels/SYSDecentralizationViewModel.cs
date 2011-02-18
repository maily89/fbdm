using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;

namespace FBD.ViewModels
{
    public class SYSDecentralizationViewModel
    {
        public List<SystemRights> ListRights { get; set; }
        public List<SystemUserGroups> ListUserGroups { get; set; }
        public string GroupID { get; set; }
        public string GroupName { get; set; }
        public string RightID { get; set; }
        public string Right { get; set; }
    }
}
