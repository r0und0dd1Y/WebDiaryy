using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace WebDiary.Data.Models
{
    public class SchoolWorker
    {
        [Key, ForeignKey(nameof(Person))]
        public string Id { get; set; }
        public virtual Person Person { get; set; }

        public string RoleDescription { get; set; }
        public bool IsDirector { get; set; }
        public bool IsClassTeacher { get; set; }
        public virtual SchoolClass Class { get; set; } 
        public virtual ICollection<Subject> Subjects { get; set; }

        [ForeignKey("School")]
        public string SchoolId { get; set; }
        public virtual School School { get; set; }

    }
}
