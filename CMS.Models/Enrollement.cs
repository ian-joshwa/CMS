using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class Enrollement
    {
        [Key]
        public int Id { get; set; }

        public StudentRegistration? StudentRegistration { get; set; }

        public int StudentRegistrationId { get; set; }

        public string Status { get; set; }

        public Session? Session { get; set; }
        public int SessionId { get; set; }
    }
}
