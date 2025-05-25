using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDiary.Data.Models;

namespace WebDiary.Data.ViewModels
{
    public class SchoolViewModel
    {
        public School GetSchool { get; set; }
        public SchoolWorker Director { get; set; }
    }
}
