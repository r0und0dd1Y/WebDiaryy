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
    public class SchoolClassRepository :ISchoolClass
    {
        private readonly EFDbContext _context;

        public SchoolClassRepository(EFDbContext dbContext)
        {
            _context = dbContext;
        }
        public IEnumerable<SchoolClass> GetSchoolClasses => _context.SchoolClasses
            .Include(x => x.SchoolClassStudents).ThenInclude(x => x.Student).ThenInclude(x => x.Person).ThenInclude(x => x.UserProfile).ThenInclude(x => x.User)
            .Include(x => x.Teacher).ThenInclude(x => x.Person).ThenInclude(x => x.UserProfile).ThenInclude(x => x.User)
            .Include(x => x.School).ThenInclude(x => x.SchoolWorkers).ThenInclude(x => x.Person).ThenInclude(x => x.UserProfile).ThenInclude(x => x.User)
            .Include(x => x.Subjects).ThenInclude(x=>x.Teacher);

    }
}
