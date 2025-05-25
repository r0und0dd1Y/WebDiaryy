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
    public class ScheduleRepository : ISchedule
    {
        private readonly EFDbContext _context;

        public ScheduleRepository(EFDbContext dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<Schedule> GetSchedules => _context.Schedules
            .Include(x => x.SchoolClass).ThenInclude(x => x.SchoolClassStudents).ThenInclude(x => x.Student).ThenInclude(x => x.Person).ThenInclude(x => x.UserProfile)
            .Include(x => x.SchoolClass).ThenInclude(x => x.School).ThenInclude(x=>x.SchoolWorkers)
            .Include(x => x.Lessons).ThenInclude(x => x.Subject).ThenInclude(x => x.StudentSubjects).ThenInclude(x => x.Student).ThenInclude(x => x.Person).ThenInclude(x => x.UserProfile)
            .Include(x => x.Lessons).ThenInclude(x => x.Subject).ThenInclude(x => x.Teacher).ThenInclude(x => x.Person).ThenInclude(x => x.UserProfile);

    }
}
