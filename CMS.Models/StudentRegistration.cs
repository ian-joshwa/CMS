using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class StudentRegistration
    {
        public int Id { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
        public DateTime DOB { get; set; }
        public string CNIC { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }

    }
}
