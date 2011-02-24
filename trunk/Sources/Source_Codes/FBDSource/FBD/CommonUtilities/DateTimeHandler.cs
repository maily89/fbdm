using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBD.CommonUtilities
{
    public class DateTimeHandler
    {
        public static bool IsToDateLaterThanFromDate(DateTime? toDate, DateTime? fromDate)
        {
            return toDate > fromDate ? true : false;
        }
    }
}
