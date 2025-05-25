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
    public class SchoolRepository : ISchool
    {
        private readonly EFDbContext _context;

        public SchoolRepository(EFDbContext dbContext)
        {
            _context = dbContext;
        }
        public IEnumerable<School> GetSchools => _context.Schools
            .Include(x => x.SchoolStudents).ThenInclude(x=>x.Student).ThenInclude(x=>x.Person).ThenInclude(x=>x.UserProfile).ThenInclude(x=>x.User)
            .Include(x => x.SchoolWorkers).ThenInclude(x => x.Person).ThenInclude(x => x.UserProfile).ThenInclude(x => x.User)
            .Include(x => x.Classes);

    }
}
