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
    public class InstructorAppService : AppService<Instructor>, IInstructorAppService
    {
        private readonly ApplicationDbContext _context;

        public InstructorAppService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Instructor instructor)
        {
            var ii = _context.Instructors.Where(x => x.Id == instructor.Id).FirstOrDefault();
            if (ii != null)
            {
                ii.Name = instructor.Name;
                ii.DepartmentId = instructor.DepartmentId;
                ii.DOB = instructor.DOB;
                ii.HiredDate = instructor.HiredDate;
                ii.Gender = instructor.Gender;

                _context.Instructors.Update(ii);
            }
        }
    }
}
