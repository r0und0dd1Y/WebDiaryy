using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebDiary.Data.Models
{
    public class School
    {
        [Key]
        [ScaffoldColumn(false)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public virtual ICollection<SchoolWorker> SchoolWorkers { get; set; }
        public ICollection<SchoolStudent> SchoolStudents { get; set; }
        public virtual ICollection<SchoolClass> Classes { get; set; }

    }
}
