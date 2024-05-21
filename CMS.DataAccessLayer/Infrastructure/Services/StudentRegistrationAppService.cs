using CMS.DataAccessLayer.Data;
using CMS.DataAccessLayer.Infrastructure.Interfaces;
using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DataAccessLayer.Infrastructure.Services
{
    public class StudentRegistrationAppService : AppService<StudentRegistration>, IStudentRegistrationAppService
    {
        private readonly ApplicationDbContext _context;
        public StudentRegistrationAppService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(StudentRegistration studentRegistration)
        {
            var ss = _context.StudentRegistrations.Where(x => x.Id == studentRegistration.Id).FirstOrDefault();
            if (ss != null)
            {
                ss.DOB = studentRegistration.DOB;
                ss.CNIC = studentRegistration.CNIC;
                ss.Gender = studentRegistration.Gender;
                ss.Address = studentRegistration.Address;
                ss.PhoneNumber = studentRegistration.PhoneNumber;
                ss.Age = studentRegistration.Age;

                _context.StudentRegistrations.Update(ss);
            }
        }
    }
}
