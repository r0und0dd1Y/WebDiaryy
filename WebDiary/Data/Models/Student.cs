using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebDiary.Data.Models
{
    public class Student
    {
        [Key, ForeignKey(nameof(Person))]
        public string Id { get; set; }
        public virtual Person Person { get; set; }

        public ICollection<SchoolClassStudent> SchoolClassStudents { get; set; }
        public virtual ICollection<Student> Siblings { get; set; }
        public virtual ICollection<Mark> Marks { get; set; }
        public ICollection<StudentSubject> StudentSubjects { get; set; }
        public ICollection<SchoolStudent> SchoolStudents { get; set; }

        [ForeignKey("Parent")]
        public string ParentId { get; set; }
        public virtual Parent Parent { get; set; }
    }
}
