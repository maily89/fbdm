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
        public static string removeUtf8Character(string inputString)
        {
            char[] utf8 = {'à','á','ạ','ả','ã','â','ầ' ,'ấ','ậ','ẩ','ẫ','ă','ằ','ắ'
                            ,'ặ','ẳ','ẵ','è','é','ẹ','ẻ','ẽ','ê','ề','ế','ệ','ể','ễ','ì','í','ị','ỉ','ĩ',
                            'ò','ó','ọ','ỏ','õ','ô','ồ','ố','ộ', 'ổ','ỗ','ơ'
                            ,'ờ','ớ','ợ','ở','ỡ',
                            'ù','ú','ụ','ủ','ũ','ư','ừ','ứ','ự', 'ử','ữ',
                            'ỳ','ý','ỵ','ỷ','ỹ',
                            'đ',
                            'À','Á','Ạ','Ả','Ã','Â','Ầ','Ấ','Ậ', 'Ẩ','Ẫ','Ă','Ằ','Ắ','Ặ','Ẳ','Ẵ',
                            'È','É','Ẹ','Ẻ','Ẽ','Ê','Ề','Ế','Ệ' ,'Ể','Ễ','Ì','Í','Ị','Ỉ','Ĩ',
                            'Ò','Ó','Ọ','Ỏ','Õ','Ô','Ồ','Ố','Ộ', 'Ổ','Ỗ','Ơ','Ờ','Ớ','Ợ','Ở','Ỡ',
                            'Ù','Ú','Ụ','Ủ','Ũ','Ư','Ừ','Ứ','Ự', 'Ử','Ữ',
                            'Ỳ','Ý','Ỵ','Ỷ','Ỹ',
                            'Đ','ê','ù','à'};
            char[] nonutf8 = {'a','a','a','a','a','a','a','a','a','a','a'
                            ,'a','a','a','a','a','a',
                            'e','e','e','e','e','e','e','e','e','e','e',
                            'i','i','i','i','i',
                            'o','o','o','o','o','o','o','o','o','o','o','o'
                            ,'o','o','o','o','o',
                            'u','u','u','u','u','u','u','u','u','u','u',
                            'y','y','y','y','y',
                            'd',
                            'A','A','A','A','A','A','A','A','A','A','A','A'
                            ,'A','A','A','A','A',
                            'E','E','E','E','E','E','E','E','E','E','E',
                            'I','I','I','I','I',
                            'O','O','O','O','O','O','O','O','O','O','O','O'
                            ,'O','O','O','O','O',
                            'U','U','U','U','U','U','U','U','U','U','U',
                            'Y','Y','Y','Y','Y',
                            'D','e','u','a' };
            for (int i = 0; i < utf8.Length; i++)
            {
                inputString.Replace(utf8[i], nonutf8[i]);
            }
            return inputString;
            
        }
    }
}
