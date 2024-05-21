using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class Result
    {
        [Key]
        public int Id { get; set; }
        public StudentRegistration StudentRegistration { get; set; }

        public int StudentRegistrationId { get; set; }

        public Examination? Examination { get; set; }

        public int ExaminationId { get; set; }

        public int Marks { get; set; }

    }
}
