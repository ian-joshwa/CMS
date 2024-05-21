using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class Examination
    {
        [Key]
        public int Id { get; set; }

        public string ExamName { get; set; }

        public DateTime ExamDate { get; set; }

        public Session? Session { get; set; }

        public int SessionId { get; set; }

        public Course? Course { get; set; }

        public int CourseId { get; set; }

        public int TotalMarks { get; set; }

        public string Duration{ get; set; }

    }
}
