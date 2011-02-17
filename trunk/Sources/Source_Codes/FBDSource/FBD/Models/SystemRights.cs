using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FBD.Models
{
    public partial class SystemRights
    {
        public static List<SystemRights> SelectRights()
        {
            FBDEntities entities = new FBDEntities();
            return entities.SystemRights.ToList();
        }



    }
}