using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DataAccessLayer.Infrastructure.Interfaces
{
    public interface ICourseAppService : IAppService<Course>
    {
        void Update(Course course);

    }
}
