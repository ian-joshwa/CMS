using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DataAccessLayer.Infrastructure.Interfaces
{
    public interface IFeesAppService : IAppService<Fees>
    {
        void Update(Fees fees);

        List<Fees> GetFeesList();

    }
}
