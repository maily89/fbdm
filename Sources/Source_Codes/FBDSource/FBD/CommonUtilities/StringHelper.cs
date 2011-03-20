using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBD.CommonUtilities
{
    public class StringHelper
    {
        /// <summary>
        /// Find the parent index of input index
        /// </summary>
        /// <param name="childIndex">The input index</param>
        /// <returns>The parent index</returns>
        public static string FindParentIndex(string childIndex)
        {
            if (childIndex == null) return null;

            if (childIndex.Length < 3) return childIndex;

            string parentIndex = childIndex.Substring(0, 3);
            
            return parentIndex;
        }

        /// <summary>
        /// Check whether or not the input index ID is digits number
        /// </summary>
        /// <param name="indexID">input index</param>
        /// <returns>true/false indicating checking result</returns>
        public static bool IsDigitsNumber(string indexID)
        {
            string digits = Constants.INDEX_FORMAT_STRING;

            for (int i = 0; i < indexID.Length; i++)
            {
                bool check = false;

                for (int j = 0; j < digits.Length; j++)
                { 
                    if (indexID[i].Equals(digits[j]))
                    {
                        check = true;
                        break;
                    }
                }
                if (check == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
