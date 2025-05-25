using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using WebDiary.Data.EFContext;
using WebDiary.Data.Interfaces;
using WebDiary.Data.Models;

namespace WebDiary.Data.Repository
{
    public class StudentRepository : IStudent
    {
        private readonly EFDbContext _context;

        public StudentRepository(EFDbContext dbContext)
        {
            _context = dbContext;
        }
        public IEnumerable<Student> GetStudents => _context.Students
            .Include(x => x.Parent).ThenInclude(x => x.UserProfile).ThenInclude(x => x.User)
            .Include(x => x.Person).ThenInclude(x => x.UserProfile).ThenInclude(x => x.User)
            .Include(x => x.SchoolStudents).ThenInclude(x => x.School).ThenInclude(x => x.SchoolWorkers).ThenInclude(x => x.Person).ThenInclude(x => x.UserProfile).ThenInclude(x => x.User)
            .Include(x => x.SchoolClassStudents).ThenInclude(x => x.SchoolClass).ThenInclude(x => x.School)
            .Include(x => x.StudentSubjects).ThenInclude(x => x.Subject)
            .Include(x => x.Siblings).ThenInclude(x => x.Person).ThenInclude(x => x.UserProfile).ThenInclude(x => x.User)
            .Include(x => x.Marks).ThenInclude(x => x.Journal);
    }
}
