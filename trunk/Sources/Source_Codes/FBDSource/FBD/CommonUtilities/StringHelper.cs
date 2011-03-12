using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBD.CommonUtilities
{
    public class StringHelper
    {
        public static string FindParentIndex(string childIndex)
        {
            if (childIndex == null) return null;

            if (childIndex.Length < 3) return childIndex;

            string parentIndex = childIndex.Substring(0, 3);
            
            return parentIndex;
        }
    }
}
