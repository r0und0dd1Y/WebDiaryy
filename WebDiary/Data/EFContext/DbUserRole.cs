using Microsoft.AspNetCore.Identity;

namespace WebDiary.Data.EFContext
{
    public class DbUserRole : IdentityUserRole<string>
    {
        public virtual DbUser User { get; set; }
        public virtual DbRole Role { get; set; }
    }
}