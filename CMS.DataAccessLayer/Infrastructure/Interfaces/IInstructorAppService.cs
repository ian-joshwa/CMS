using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;   

namespace CMS.DataAccessLayer.Infrastructure.Interfaces
{
    public interface IInstructorAppService : IAppService<Instructor>
    {
        void Update(Instructor instructor);

    }
}
