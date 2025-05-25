using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDiary.Data.Models;

namespace WebDiary.Data.ViewModels
{
    public class ListParentViewModel
    {
       public IEnumerable<Parent> GetParents { get; set; }
       public School School { get; set; }
        public Student Kid { get; set; }
    }
}
