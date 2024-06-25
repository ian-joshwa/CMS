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
    public class CourseYearAppService : AppService<CourseYear>, ICourseYearAppService
    {
        private readonly ApplicationDbContext _context;
        public CourseYearAppService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(CourseYear courseYear)
        {
            var cc = _context.CourseYears.Where(x => x.Id == courseYear.Id).FirstOrDefault();
            if (cc != null)
            {
                cc.Year = courseYear.Year;
                //cc.InstructorId = course.InstructorId;
                _context.CourseYears.Update(cc);
            }
        }
    }
}
