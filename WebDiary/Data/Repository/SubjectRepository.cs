using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDiary.Data.EFContext;
using WebDiary.Data.Interfaces;
using WebDiary.Data.Models;

namespace WebDiary.Data.Repository
{
    public class SubjectRepository : ISubject
    {
        private readonly EFDbContext _context;

        public SubjectRepository(EFDbContext dbContext)
        {
            _context = dbContext;
        }
        public IEnumerable<Subject> GetSubjects => _context.Subjects
            .Include(x => x.SchoolClass)
            .Include(x => x.Teacher).ThenInclude(x=>x.Person).ThenInclude(x=>x.UserProfile);
    }
}
