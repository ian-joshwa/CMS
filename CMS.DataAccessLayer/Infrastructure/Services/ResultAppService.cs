using CMS.DataAccessLayer.Data;
using CMS.DataAccessLayer.Infrastructure.Interfaces;
using CMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DataAccessLayer.Infrastructure.Services
{
    internal class ResultAppService : AppService<Result> , IResultAppService
    {
        private readonly ApplicationDbContext _context;
        public ResultAppService(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }

        public IQueryable<Result> GetResultList()
        {
            return _context.Results.AsQueryable();
        }

        public void Update(Result result)
        {
            var ss = _context.Results.Where(x => x.Id == result.Id).FirstOrDefault();
            if (ss != null)
            {
                ss.StudentRegistrationId = result.StudentRegistrationId;
                ss.CourseId= result.CourseId;
                ss.TotalMarks= result.TotalMarks;
                //ss.DepartmentId= session.DepartmentId;
                ss.Marks = result.Marks;
                _context.Results.Update(ss);
            }
        }
    }
}
