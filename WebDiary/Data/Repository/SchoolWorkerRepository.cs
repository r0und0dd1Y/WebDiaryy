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
    public class SchoolWorkerRepository :ISchoolWorker
    {
        private readonly EFDbContext _context;

        public SchoolWorkerRepository(EFDbContext dbContext)
        {
            _context = dbContext;
        }
        public IEnumerable<SchoolWorker> GetSchoolWorkers => _context.SchoolWorkers
            .Include(x=>x.Person).ThenInclude(x=>x.UserProfile).ThenInclude(x=>x.User)
            .Include(x=>x.School)
            .Include(x => x.Class).ThenInclude(x=>x.School)
            .Include(x => x.Subjects).ThenInclude(x=>x.SchoolClass);
    }
}
