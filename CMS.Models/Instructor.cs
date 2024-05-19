using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class Instructor
    {
        [Key]
        public int Id { get; set; }

        public string Name {  get; set; }

        public Department? Department { get; set; }
        public int DepartmentId { get; set; }



    }
}
