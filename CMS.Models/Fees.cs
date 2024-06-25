using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class Fees
    {
        [Key]
        public int Id { get; set; }
        public StudentRegistration? StudentRegistration { get; set; }

        public int StudentRegistrationId { get; set; }

        public string FeeType { get; set; }

        public int Amount { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? PaidDate { get; set; }

        public string Status { get; set; }

        public string? FeeVoucher { get; set; }

    }
}