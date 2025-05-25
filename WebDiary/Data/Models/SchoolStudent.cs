using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDiary.Data.Models
{
    public class SchoolStudent
    {
        public string SchoolId { get; set; }
        public School School { get; set; }

        public string StudentId { get; set; }
        public Student Student { get; set; }
    }
}
