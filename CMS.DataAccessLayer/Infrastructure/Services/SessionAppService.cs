using CMS.DataAccessLayer.Data;
using CMS.DataAccessLayer.Infrastructure.Interfaces;
using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DataAccessLayer.Infrastructure.Services
{
    public class SessionAppService : AppService<Session>, ISessionAppService
    {
        private readonly ApplicationDbContext _context;

        public SessionAppService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Session session)
        {
            var ss = _context.Sessions.Where(x => x.Id == session.Id).FirstOrDefault();
            if (ss != null)
            {
                ss.SessionName = session.SessionName;
                ss.Description = session.Description;
                //ss.DepartmentId= session.DepartmentId;
                ss.StartTime = session.StartTime;
                ss.AdmissionFee = session.AdmissionFee;
                ss.Merit = session.Merit;
                ss.MonthlyFee = session.MonthlyFee;
                ss.EndTime = session.EndTime;
                ss.Capacity = session.Capacity;
                ss.Status = session.Status;
                ss.IsInterDocumentRequired = session.IsInterDocumentRequired;
                _context.Sessions.Update(ss);
            }
        }
    }
}
