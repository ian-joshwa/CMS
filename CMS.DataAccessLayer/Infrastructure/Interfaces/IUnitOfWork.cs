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

        IEnrollementAppService EnrollementAppService { get; }

        IFeesAppService FeesAppService { get; }


        IStudentRegistrationAppService StudentRegistrationAppService { get; }
        IResultAppService ResultAppService { get; }
        ICourseYearAppService CourseYearAppService { get; }
        IStudentDocumentAppService StudentDocumentAppService { get; }

        Task<bool> Save();
    }
}
