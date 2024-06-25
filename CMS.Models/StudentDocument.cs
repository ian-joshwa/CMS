using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class StudentDocument
    {
        [Key]
        public int Id { get; set; } 

        public StudentRegistration? Student { get; set; }

        public int StudentId { get; set; }

        public int TotalMarks { get; set; }
        public int ObtainedMarks { get; set; }

        public string DocumentType { get; set; }

        public string Combination { get; set; }

        public string? Document {  get; set; }


    }
}
