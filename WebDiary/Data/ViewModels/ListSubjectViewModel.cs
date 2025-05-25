using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDiary.Data.Models;

namespace WebDiary.Data.ViewModels
{
    public class ListSubjectViewModel
    {
        public IEnumerable<SubjectViewModel> GetSubjects { get; set; }
        public SchoolClass SchoolClass { get; set; }
        public SchoolWorker SchoolWorker { get; set; }
    }
}
