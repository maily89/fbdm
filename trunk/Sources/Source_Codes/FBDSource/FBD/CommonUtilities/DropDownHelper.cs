﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FBD.CommonUtilities
{
    public static class DropDownHelper
    {
        public static List<SelectListItem> ValueType = new List<SelectListItem>();
        static DropDownHelper()
        {
            ValueType.Add(new SelectListItem
                    {
                        Text = "Number Type",
                        Value = "1",
                        Selected = true
                    });
            ValueType.Add(new SelectListItem
                    {
                        Text = "Character Type",
                        Value = "0",
                        Selected = false
                    });

        }
    }
}
                    
