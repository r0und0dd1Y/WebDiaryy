using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDiary.Data.Models;

namespace WebDiary.Data.ViewModels
{
    public class SubjectViewModel
    {
        public Subject GetSubject { get; set; }
        public SchoolClass SchoolClass { get; set; }
        public SchoolWorker Teacher { get; set; }
    }
}
