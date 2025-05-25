using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebDiary.Data.EFContext;

namespace WebDiary.Data.Models
{
    public class UserProfile
    {
        [ForeignKey("User")]
        public string Id { get; set; }
        [Required, StringLength(75)]
        public string FirstName { get; set; }
        [Required, StringLength(75)]
        public string MiddleName { get; set; }
        [Required, StringLength(75)]
        public string LastName { get; set; }
        [StringLength(150)]
        public string Image { get; set; }
        public DateTime RegistrationDate { get; set; }
        [Required, StringLength(75)]
        public string Gender { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public virtual DbUser User { get; set; }
    }
}
