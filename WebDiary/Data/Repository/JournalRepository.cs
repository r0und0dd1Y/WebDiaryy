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
    public class JournalRepository : IJournal
    {
        private readonly EFDbContext _context;

        public JournalRepository(EFDbContext dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<Journal> GetJournals => _context.Journals
            .Include(x => x.Marks).ThenInclude(x => x.Student).ThenInclude(x => x.Person).ThenInclude(x => x.UserProfile)
            .Include(x => x.Subject).ThenInclude(x => x.SchoolClass).ThenInclude(x=>x.SchoolClassStudents).ThenInclude(x=>x.Student).ThenInclude(x=>x.Person).ThenInclude(x=>x.UserProfile)
            .Include(x => x.Subject).ThenInclude(x => x.Teacher).ThenInclude(x => x.Person).ThenInclude(x => x.UserProfile)
            .Include(x => x.Subject).ThenInclude(x => x.StudentSubjects).ThenInclude(x => x.Student).ThenInclude(x => x.Person).ThenInclude(x => x.UserProfile)
            .Include(x => x.LessonInfos);
    }
}
