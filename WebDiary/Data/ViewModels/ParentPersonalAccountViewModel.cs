using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDiary.Data.Models;
using WebDiary.Models;

namespace WebDiary.Data.ViewModels
{
    public class ParentPersonalAccountViewModel
    {
        public UserInfo GetUserInfo { get; set; }
        public Parent Parent { get; set; }
        public List<Student> Kids { get; set; }
    }
}
