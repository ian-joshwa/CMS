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
    public class StudentDocumentAppService : AppService<StudentDocument>, IStudentDocumentAppService
    {
        private readonly ApplicationDbContext _context;
        public StudentDocumentAppService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(StudentDocument studentDocument)
        {
            var ss = _context.StudentDocuments.Where(x => x.Id == studentDocument.Id).FirstOrDefault();
            if (ss != null)
            {
                ss.TotalMarks = studentDocument.TotalMarks;
                ss.ObtainedMarks = studentDocument.ObtainedMarks;
                ss.DocumentType = studentDocument.DocumentType;
                ss.Combination = studentDocument.Combination;
                ss.Document = studentDocument.Document;

                _context.StudentDocuments.Update(ss);
            }
        }
    }
}
