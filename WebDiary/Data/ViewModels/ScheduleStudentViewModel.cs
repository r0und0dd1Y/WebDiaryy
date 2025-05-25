using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDiary.Data.Models;

namespace WebDiary.Data.ViewModels
{
    public class ScheduleStudentViewModel
    {
        public List<Lesson> Lessons { get; set; }
        public SchoolClass SchoolClass { get; set; }
    }
}
