using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class Session
    {
        [Key]
        public int SessionId { get; set; }

        public string SessionName { get; set; }

        public string Description { get; set; }

        //Department

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int? Capacity { get; set; }

        public int? EnrolledCount { get; set; }

        public string Status { get; set; }


    }
}
