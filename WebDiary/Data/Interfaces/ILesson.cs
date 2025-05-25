using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDiary.Data.Models;

namespace WebDiary.Data.Interfaces
{
    public interface ILesson
    {
        IEnumerable<Lesson> GetLessons { get; }
    }
}
