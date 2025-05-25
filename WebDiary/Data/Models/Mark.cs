using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebDiary.Data.Models
{
    public class Mark
    {
        [Key]
        [ScaffoldColumn(false)]
        public string Id { get; set; }
        public double Value { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime TimeSet { get; set; }

        [ForeignKey("Student")]
        public string StudentId { get; set; }
        public virtual Student Student { get; set; }

        public MarkType MarkType { get; set; }
        [ForeignKey("Journal")]
        public string JournalId { get; set; }
        public virtual Journal Journal { get; set; }

    }
    public enum MarkType
    {
        Current =0,
        Semester1=1,
        Semester2=2,
        Final=3
    }
}
