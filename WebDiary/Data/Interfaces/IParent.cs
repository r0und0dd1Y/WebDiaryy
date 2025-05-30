﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDiary.Data.Models;

namespace WebDiary.Data.Interfaces
{
    public interface IParent
    {
        IEnumerable<Parent> GetParents { get; }
    }
}
