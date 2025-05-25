using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDiary.Data.Models;
using WebDiary.Models;

namespace WebDiary.Data.ViewModels
{
    public class SchoolWorkerPersonalAccountViewModel
    {
        public UserInfo GetUserInfo { get; set; }
        public SchoolWorker SchoolWorker { get; set; }
        public School GetSchool { get; set; }
    }
}
