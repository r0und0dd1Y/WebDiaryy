using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDiary.Data.Models;

namespace WebDiary.Data.EFContext
{
    public class EFDbContext : IdentityDbContext<DbUser, DbRole, string, IdentityUserClaim<string>,
   DbUserRole, IdentityUserLogin<string>,
   IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public EFDbContext(DbContextOptions<EFDbContext> options) : base(options)
        { }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Parent> Parents { get; set; }
        public virtual DbSet<SchoolWorker> SchoolWorkers { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<SchoolClass> SchoolClasses { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }
        public virtual DbSet<Mark> Marks { get; set; }
        public virtual DbSet<LessonInfo>  LessonInfos { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Journal> Journals { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(builder);

            builder.Entity<SchoolClassStudent>()
                .HasKey(x => new { x.SchoolClassId, x.StudentId });
            builder.Entity<SchoolClassStudent>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.SchoolClassStudents)
                .HasForeignKey(sc => sc.StudentId);
            builder.Entity<SchoolClassStudent>()
                .HasOne(sc => sc.SchoolClass)
                .WithMany(c => c.SchoolClassStudents)
                .HasForeignKey(sc => sc.SchoolClassId);

            builder.Entity<SchoolStudent>()
                .HasKey(x => new { x.SchoolId, x.StudentId });
            builder.Entity<SchoolStudent>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.SchoolStudents)
                .HasForeignKey(sc => sc.StudentId);
            builder.Entity<SchoolStudent>()
                .HasOne(sc => sc.School)
                .WithMany(c => c.SchoolStudents)
                .HasForeignKey(sc => sc.SchoolId);

            builder.Entity<StudentSubject>()
                .HasKey(x => new { x.SubjectId, x.StudentId });
            builder.Entity<StudentSubject>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(sc => sc.StudentId);
            builder.Entity<StudentSubject>()
                .HasOne(sc => sc.Subject)
                .WithMany(c => c.StudentSubjects)
                .HasForeignKey(sc => sc.SubjectId);

           

            builder.Entity<DbUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });
                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

            });
        }
    }
}
