using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDiary.Data.Models
{
    public class SchoolClassStudent
    {
        public string SchoolClassId { get; set; }
        public SchoolClass SchoolClass { get; set; }

        public string StudentId { get; set; }
        public Student Student { get; set; }
    }
}
