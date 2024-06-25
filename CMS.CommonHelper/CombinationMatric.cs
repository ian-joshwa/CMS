using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.CommonHelper
{
    public static class CombinationMatric
    {

        public const string Biology = "Biology";
        public const string Computer = "Computer";

        public static List<SelectListItem> GetCombinations()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = Biology, Value = Biology });
            list.Add(new SelectListItem { Text = Computer, Value = Computer });

            return list;
        }


    }
}
