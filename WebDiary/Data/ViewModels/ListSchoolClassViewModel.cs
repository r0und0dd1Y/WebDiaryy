using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDiary.Data.Models;

namespace WebDiary.Data.ViewModels
{
    public class ListSchoolClassViewModel
    {
        public IEnumerable<SchoolClass> SchoolClasses { get; set; }
        public School School { get; set; }
        public Student Student { get; set; }
    }
}
