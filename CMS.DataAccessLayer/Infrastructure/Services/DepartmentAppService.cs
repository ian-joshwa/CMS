using CMS.DataAccessLayer.Data;
using CMS.DataAccessLayer.Infrastructure.Interfaces;
using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DataAccessLayer.Infrastructure.Services
{
    public class DepartmentAppService:AppService<Department>, IDepartmentAppService
    {

        private readonly ApplicationDbContext _context;

        public DepartmentAppService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        


        public void Update(Department department)
        {
            var ss = _context.Departments.Where(x => x.Id == department.Id ).FirstOrDefault();
            if (ss != null)
            {
                ss.Name = department.Name;
                ss.HOD=department.HOD;
                
                _context.Departments.Update(ss);
            }
        }
    }
}
