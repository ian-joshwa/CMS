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
    public class FeesAppService : AppService<Fees>, IFeesAppService
    {
        private readonly ApplicationDbContext _context;

        public FeesAppService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Fees> GetFeesList()
        {
            return _context.Fees.Include(x => x.StudentRegistration).Include("StudentRegistration.ApplicationUser").ToList();
        }

        public void Update(Fees fees)
        {
            var ss = _context.Fees.Where(x => x.Id == fees.Id).FirstOrDefault();
            if (ss != null)
            {
                ss.StudentRegistrationId = fees.StudentRegistrationId;
                ss.Amount = fees.Amount;
                ss.DueDate = fees.DueDate;
                ss.PaidDate = fees.PaidDate;
                ss.Status = fees.Status;
                ss.FeeType = fees.FeeType;
                ss.FeeVoucher = fees.FeeVoucher;
                _context.Fees.Update(ss);
            }
        }
    }
}
