using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class CourseYear
    {

        [Key]
        public int Id { get; set; }

        public int Year { get; set; }

    }
}
