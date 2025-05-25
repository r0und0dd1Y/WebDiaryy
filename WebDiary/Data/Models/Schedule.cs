using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebDiary.Data.Models
{
    public class Schedule
    {
        [Key]
        [ScaffoldColumn(false)]
        public string Id { get; set; }

        [ForeignKey("SchoolClass")]
        public string SchoolClassId { get; set; }
        public virtual SchoolClass SchoolClass { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
