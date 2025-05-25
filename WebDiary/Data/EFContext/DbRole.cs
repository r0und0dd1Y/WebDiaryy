using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WebDiary.Data.EFContext
{
    public class DbRole : IdentityRole<string>
    {
        public ICollection<DbUserRole> UserRoles { get; set; }
    }
}