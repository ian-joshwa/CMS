using CMS.DataAccessLayer.Data;
using CMS.DataAccessLayer.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DataAccessLayer.Infrastructure.Services
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _context;
        public ISessionAppService SessionAppService { get; private set; }
        public ICourseAppService CourseAppService { get; private set; }

        public IDepartmentAppService DepartmentAppService { get; private set; }

        public IInstructorAppService InstructorAppService { get; private set; }

        public IEnrollementAppService EnrollementAppService { get; private set; }

        public IFeesAppService FeesAppService { get; private set; }

        public IExaminationAppService ExaminationAppService { get; private set; }

        //public IResultAppService ResultAppService { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            SessionAppService = new SessionAppService(context);
            CourseAppService = new CourseAppService(context);
            DepartmentAppService = new DepartmentAppService(context);
            InstructorAppService = new InstructorAppService(context);
            EnrollementAppService = new EnrollementAppService(context);
            //ResultAppService = new 
            FeesAppService = new  FeesAppService(context);
        }

        public async Task<bool> Save()
        {
            try
            {
                int result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch(Exception ex)
            {
                return false;
            }

        }
    }
}
