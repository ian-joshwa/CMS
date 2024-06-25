using CMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DataAccessLayer.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Course> Courses { get; set; }

        public DbSet<Enrollement> Enrollements { get; set; }
        public DbSet<Fees> Fees { get; set; }

        public DbSet<StudentRegistration> StudentRegistrations { get; set; }
        public DbSet<StudentDocument> StudentDocuments { get; set; }

        public DbSet<Result> Results { get; set; }
        public DbSet<CourseYear> CourseYears { get; set; }
    }
}
