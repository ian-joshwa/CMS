using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DataAccessLayer.Infrastructure.Interfaces
{
    public interface ICourseYearAppService : IAppService<CourseYear>
    {
        void Update(CourseYear courseYear);

    }
}
