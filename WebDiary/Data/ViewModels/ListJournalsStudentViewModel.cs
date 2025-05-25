using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDiary.Data.Models;

namespace WebDiary.Data.ViewModels
{
    public class ListJournalsStudentViewModel
    {
        public List<ListJournalViewModel> ListJournalsViewModels { get; set; }
        public Student Student { get; set; }
    }
}
