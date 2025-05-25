using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebDiary.Data.Models
{
    public class Lesson
    {
        [Key]
        [ScaffoldColumn(false)]
        public string Id { get; set; }
        public int WeekDay { get; set; }
        public string Cabinet { get; set; }
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        [ForeignKey("Subject")]
        public string SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

        [ForeignKey("Schedule")]
        public string ScheduleId { get; set; }
        public virtual Schedule Schedule { get; set; }
    }
}
