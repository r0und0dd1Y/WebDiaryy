using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDiary.Data.Models;

namespace WebDiary.Data.ViewModels
{
    public class StudentViewModel
    {
        public Student GetStudent { get; set; }
        public ICollection<SchoolClass> SchoolClasses { get; set; }
    }
}
