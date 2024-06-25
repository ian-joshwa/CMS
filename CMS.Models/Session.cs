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
        public int Id { get; set; }

        public string SessionName { get; set; }

        public string Description { get; set; }
        public bool IsInterDocumentRequired { get; set; }

        //Department

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int AdmissionFee { get; set; }

        public int MonthlyFee { get; set; }

        public float Merit {  get; set; }

        public int? Capacity { get; set; }

        public string Status { get; set; }


    }
}
