using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDiary.Data.Models;

namespace WebDiary.Data.ViewModels
{
    public class ScheduleTeacherViewModel
    {
        public List<Lesson> Lessons { get; set; }
        public SchoolWorker SchoolWorker { get; set; }
    }
}
