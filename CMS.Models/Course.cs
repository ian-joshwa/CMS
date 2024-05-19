using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreditHours { get; set; }
        public Session? Session { get; set; }
        public int SessionId { get; set; }

        //public Instructor? Instructor { get; set; }

        //public int InstructorId { get; set;}

    }
}
