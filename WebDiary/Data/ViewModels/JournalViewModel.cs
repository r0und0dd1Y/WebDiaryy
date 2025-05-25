using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDiary.Data.Models;

namespace WebDiary.Data.ViewModels
{
    public class JournalViewModel
    {
        public Journal GetJournal { get; set; }
        public List<Mark> MarksFilter { get; set; }
    }
}
