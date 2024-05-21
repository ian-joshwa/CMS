using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DataAccessLayer.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        ISessionAppService SessionAppService { get; }
        ICourseAppService CourseAppService { get; }

        IDepartmentAppService DepartmentAppService { get; }

        IInstructorAppService InstructorAppService { get; }

        IEnrollementAppService EnrollementAppService { get; }

        IFeesAppService FeesAppService { get; }

        IExaminationAppService ExaminationAppService { get; }

        IStudentRegistrationAppService StudentRegistrationAppService { get; }

        //IResultAppService ResultAppService { get; }

        Task<bool> Save();
    }
}
