using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDiary.Data.Models;

namespace WebDiary.Data.Interfaces
{
    public interface IStudent
    {
        IEnumerable<Student> GetStudents { get; }
    }
}
