using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDiary.Data.Models;

namespace WebDiary.Data.ViewModels
{
    public class ListScheduleViewModel
    {
        public List<Schedule> Schedules { get; set; }
        public School School { get; set; }
    }
}
