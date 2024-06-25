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
    public class EnrollementAppService : AppService<Enrollement>, IEnrollementAppService
    {
        private readonly ApplicationDbContext _context;

        public EnrollementAppService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public IQueryable<Enrollement> GetEnrollementList()
        {
            return _context.Enrollements.AsQueryable();
        }

        public void Update(Enrollement enrollement)
        {
            var ss = _context.Enrollements.Where(x => x.Id == enrollement.Id).FirstOrDefault();
            if (ss != null)
            {
                ss.StudentRegistrationId = enrollement.StudentRegistrationId;
                ss.SessionId = enrollement.SessionId;
                ss.Status = enrollement.Status;
                _context.Enrollements.Update(ss);
            }
        }
    }
}
