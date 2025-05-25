using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDiary.Data.Models;

namespace WebDiary.Data.ViewModels
{
    public class SchoolWorkerViewModel
    {
        public SchoolWorker GetSchoolWorker { get; set; }
        public SchoolClass Class { get; set; }
        public ICollection<Subject> Subjects { get; set; }
    }
}
