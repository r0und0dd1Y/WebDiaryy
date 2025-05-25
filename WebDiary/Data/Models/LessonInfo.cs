using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebDiary.Data.Models
{
    public class LessonInfo
    {
        [Key]
        [ScaffoldColumn(false)]
        public string Id { get; set; }

        public string Theme { get; set; }
        public string Homework { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("Journal")]
        public string JournalId { get; set; }
        public Journal Journal { get; set; }
    }
}
