using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.CommonHelper
{
    public static class DocumentType
    {

        public const string Matriculation = "Matriculation";
        public const string Intermediate = "Intermediate";

        public static List<SelectListItem> GetDocumentTypes()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = Matriculation, Value = Matriculation });
            list.Add(new SelectListItem { Text = Intermediate, Value = Intermediate });

            return list;
        }

    }
}
