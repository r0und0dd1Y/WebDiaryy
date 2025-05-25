using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDiary.Data.EFContext;
using WebDiary.Data.Interfaces;
using WebDiary.Data.Models;

namespace WebDiary.Data.Repository
{
    public class ParentRepository : IParent
    {
        private readonly EFDbContext _context;

        public ParentRepository(EFDbContext dbContext)
        {
            _context = dbContext;
        }
        public IEnumerable<Parent> GetParents => _context.Parents
            .Include(x => x.UserProfile).ThenInclude(x => x.User)
            .Include(x => x.Kids).ThenInclude(x => x.Person).ThenInclude(x => x.UserProfile).ThenInclude(x => x.User);
    }
}
