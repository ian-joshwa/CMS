using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.CommonHelper
{
    public static class Gender
    {

        public const string Male = "Male";
        public const string Female = "Female";
        public const string Others = "Others";

        public static List<SelectListItem> GetGenders()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = Male, Value = Male });
            list.Add(new SelectListItem { Text = Female, Value = Female });
            list.Add(new SelectListItem { Text = Others, Value = Others });

            return list;
        }

    }
}
