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
    public class ExaminationAppService : AppService<Examination>, IExaminationAppService
    {
        private readonly ApplicationDbContext _context;

        public ExaminationAppService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Examination examination)
        {
            var ss = _context.Examinations.Where(x => x.Id == examination.Id).FirstOrDefault();
            if (ss != null)
            {
                ss.ExamName = examination.ExamName;
                ss.ExamDate = examination.ExamDate;
                //ss.DepartmentId= session.DepartmentId;
                ss.SessionId = examination.SessionId;
                ss.CourseId = examination.CourseId;
                ss.TotalMarks = examination.TotalMarks;
                ss.Duration = examination.Duration;
                _context.Examinations.Update(ss);
            }
        }
    }
}
