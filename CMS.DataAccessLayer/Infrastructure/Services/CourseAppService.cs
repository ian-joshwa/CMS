using CMS.DataAccessLayer.Data;
using CMS.DataAccessLayer.Infrastructure.Interfaces;
using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace CMS.DataAccessLayer.Infrastructure.Services
{
    public class CourseAppService : AppService<Course>, ICourseAppService
    {
        private readonly ApplicationDbContext _context;
        public CourseAppService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Course course)
        {
            var cc = _context.Courses.Where(x => x.Id == course.Id).FirstOrDefault();
            if (cc != null)
            {
                cc.Name = course.Name;
                cc.CreditHours = course.CreditHours;
                cc.SessionId = course.SessionId;
                //cc.InstructorId = course.InstructorId;
                _context.Courses.Update(cc);
            }
        }
    }
}
