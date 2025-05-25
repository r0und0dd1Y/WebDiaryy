using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebDiary.Data.Models
{
    public class Subject
    {
        [Key]
        [ScaffoldColumn(false)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [ForeignKey("SchoolClass")]
        public string SchoolClassId { get; set; }
        public virtual SchoolClass SchoolClass { get; set; }
        [ForeignKey("Teacher")]
        public string TeacherId { get; set; }
        public virtual SchoolWorker Teacher { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<Journal> Journals { get; set; }
        public ICollection<StudentSubject> StudentSubjects { get; set; }
    }
}
