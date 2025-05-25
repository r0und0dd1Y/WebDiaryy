using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDiary.Data.Models;

namespace WebDiary.Data.ViewModels
{
    public class ListScheduleStudentViewModel
    {
        public List<ScheduleStudentViewModel> GetSchedules { get; set; }
        public Student Student { get; set; }
    }
}
