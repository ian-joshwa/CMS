using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.CommonHelper
{
    public static class WebsiteRoles
    {

        public const string Role_Admin = "ADMIN";
        public const string Role_Student = "STUDENT";

        public static List<string> GetRoles()
        {
            List<string> roles = new List<string>();
            roles.Add(Role_Admin);
            roles.Add(Role_Student);

            return roles;
        }

    }
}
