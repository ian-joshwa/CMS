using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.CommonHelper
{
    public static class CombinationInter
    {

        public const string PreMedical = "PreMedical";
        public const string PreEngineering = "PreEngineering";
        public const string ICS = "ICS";

        public static List<SelectListItem> GetCombinations()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "Pre Medical", Value = PreMedical });
            list.Add(new SelectListItem { Text = "Pre Engineering", Value = PreEngineering });
            list.Add(new SelectListItem { Text = "ICS", Value = ICS });

            return list;
        }

    }
}
