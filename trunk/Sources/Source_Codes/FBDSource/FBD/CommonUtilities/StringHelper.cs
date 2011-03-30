using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

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

        /// <summary>
        /// The method is used to encode a password as MD5 format
        /// </summary>
        /// <param name="password">the password to be encoded</param>
        /// <returns>an encoded string</returns>
        public static string Encode(string password)
        {
            Byte[] orginialPwBytes;
            Byte[] encodedPwBytes;
            MD5 md5;

            md5 = new MD5CryptoServiceProvider();
            orginialPwBytes = ASCIIEncoding.Default.GetBytes(password);
            encodedPwBytes = md5.ComputeHash(orginialPwBytes);

            return BitConverter.ToString(encodedPwBytes);
        }

        public static string FormatDate(DateTime date)
        {
            if (date == null ) return null;
            return string.Format(Constants.DATE_FORMAT , date);

        }
    }
}
