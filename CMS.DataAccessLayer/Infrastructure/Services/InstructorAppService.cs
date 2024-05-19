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
            var ii = _context.instructors.Where(x => x.Id == instructor.Id).FirstOrDefault();
            if (ii != null)
            {
                ii.Name = instructor.Name;
                ii.Department = instructor.Department;
                _context.instructors.Update(ii);
            }
        }
    }
}
