using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using WebDiary.Data.Models;

namespace WebDiary.Data.EFContext
{
    public class DbUser : IdentityUser<string>
    {
        public ICollection<DbUserRole> UserRoles { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}