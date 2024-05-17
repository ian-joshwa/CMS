using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.CommonHelper
{
    public static class SessionStatus
    {

        public const string Pending = "PENDING";
        public const string Completed = "COMPLETED";
        public const string Ongoing = "ONGOING";
        public const string Canceled = "CANCELED";

        public static List<SelectListItem> GetSessionStatus()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text=Pending, Value=Pending });
            list.Add(new SelectListItem { Text= Completed, Value= Completed });
            list.Add(new SelectListItem { Text= Ongoing, Value= Ongoing });
            list.Add(new SelectListItem { Text= Canceled, Value= Canceled });

            return list;
        }

    }
}
