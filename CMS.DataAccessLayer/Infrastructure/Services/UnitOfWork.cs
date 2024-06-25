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

        public IEnrollementAppService EnrollementAppService { get; private set; }

        public IFeesAppService FeesAppService { get; private set; }


        public IStudentRegistrationAppService StudentRegistrationAppService { get; private set; }

        public IResultAppService ResultAppService { get; private set; }
        public ICourseYearAppService CourseYearAppService { get; private set; }

        public IStudentDocumentAppService StudentDocumentAppService { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            SessionAppService = new SessionAppService(context);
            CourseAppService = new CourseAppService(context);
            EnrollementAppService = new EnrollementAppService(context);
            StudentRegistrationAppService = new StudentRegistrationAppService(context);
            ResultAppService = new ResultAppService(context);
            FeesAppService = new  FeesAppService(context);
            CourseYearAppService = new CourseYearAppService(context);
            StudentDocumentAppService = new StudentDocumentAppService(context);
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
