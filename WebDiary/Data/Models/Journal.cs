using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebDiary.Data.Models
{
    public class Journal
    {
        [Key]
        [ScaffoldColumn(false)]
        public string Id { get; set; }

        [ForeignKey("Subject")]
        public string SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

        public virtual ICollection<LessonInfo> LessonInfos { get; set; }
        public virtual ICollection<Mark> Marks { get; set; }

    }
}
