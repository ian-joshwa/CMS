using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.CommonHelper
{
    public static class Navigation
    {
        public static string ActivePage { get; set; }

        public static string GetActivePage()
        {
            return ActivePage;
        }

    }
}
