using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBD.Models
{
    public static class DatabaseAccess
    {
        private static FBDEntities entities;
        public static FBDEntities Entities
        {
            get 
            {
                if (entities == null)
                {
                    entities = new FBDEntities();
                }
                return entities;
            }
        }

    }
}
