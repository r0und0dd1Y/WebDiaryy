using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDiary.Data.Models;
using WebDiary.Models;

namespace WebDiary.Data.ViewModels
{
    public class StudentPersonalAccountViewModel
    {
        public UserInfo GetUserInfo { get; set; }
        public Student Student { get; set; }
        public List<School> Schools { get; set; }
    }
}
