using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDiary.Data.Models;

namespace WebDiary.Data.ViewModels
{
    public class ListSchoolWorkerViewModel
    {
        public IEnumerable<SchoolWorker> GetSchoolWorkers { get; set; }
        public School School { get; set; }
    }
}
