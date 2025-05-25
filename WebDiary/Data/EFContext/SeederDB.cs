using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDiary.Data.Models;

namespace WebDiary.Data.EFContext
{
    public class SeederDB
    {
        public static void SeedUsers(UserManager<DbUser> userManager,
            EFDbContext _context)
        {

            var count = userManager.Users.Count();
            if (count <= 0)
            {
                string email = "fvllenleaves@gmail.com";
                var roleName = "Admin";
                var userprofile = new UserProfile
                {
                    FirstName = "admin",
                    LastName = "admin",
                    MiddleName = "admin",
                    RegistrationDate = DateTime.Now,
                    Gender = "Male",
                    BirthDate = DateTime.Now.AddYears(-45).AddDays(-38)
                };
                var user = new DbUser
                {
                    Email = email,
                    UserName = email,
                    PhoneNumber = "+38(095)890-10-45",
                    UserProfile = userprofile
                };
                _context.UserProfiles.Add(userprofile);
                var result = userManager.CreateAsync(user, "Qwerty1-").Result;
                result = userManager.AddToRoleAsync(user, roleName).Result;
                _context.SaveChanges();

                var school = new School
                {
                    Name = "King's College London Maths School",
                    Address = "80 Kennington Road, London, SE11 6NJ",
                    Description = "King's College London Mathematics School is a free school sixth form located in the Lambeth area of London, England. King's College London Mathematics School is run in partnership with King's College London University, to provide high quality mathematics education in London.",
                    PhoneNumber = "+44 20 7848 7346",
                    Email = "kingscollegelondonmathsschool@gmail.com",
                    Type = "Academy school Coeducational",
                    SchoolWorkers = new List<SchoolWorker>(),
                    SchoolStudents = new List<SchoolStudent>(),
                    Classes = new List<SchoolClass>()
                };
                _context.Schools.Add(school);
                _context.SaveChanges();

                var Directoremail = "bethaniemedina@gmail.com";
                var DirectorroleName = "Director";
                var Directoruserprofile = new UserProfile
                {
                    FirstName = "Bethanie",
                    LastName = "Medina",
                    MiddleName = "Saoirse",
                    RegistrationDate = DateTime.Now,
                    Gender = "Female",
                    BirthDate = DateTime.Now.AddYears(-48).AddDays(25), 
                    Image= "https://pbs.twimg.com/profile_images/1096227324884910080/3FN117n3.jpg"
                };
                var Directoruser = new DbUser
                {
                    Email = Directoremail,
                    UserName = Directoremail,
                    PhoneNumber = "+1 224-698-6579",
                    UserProfile = Directoruserprofile
                };
                _context.UserProfiles.Add(Directoruserprofile);
                result = userManager.CreateAsync(Directoruser, "Qwerty1-").Result;
                result = userManager.AddToRoleAsync(Directoruser, DirectorroleName).Result;
                result = userManager.AddToRoleAsync(Directoruser, "Teacher").Result;
                result = userManager.AddToRoleAsync(Directoruser, "SchoolWorker").Result;
                _context.SaveChanges();
                var Directorperson = new Person { UserProfile = Directoruserprofile, IsActive = true };
                _context.Persons.Add(Directorperson);
                var DirectorschoolWorker = new SchoolWorker { Person = Directorperson, RoleDescription = "Director and Geography teacher", Subjects = new List<Subject>(), IsDirector = true, IsClassTeacher = false };
                _context.SchoolWorkers.Add(DirectorschoolWorker);
                school.SchoolWorkers.Add(DirectorschoolWorker);


                var student1email = "afsanamcmanus@gmail.com";
                var student1roleName = "Student";
                var student1userprofile = new UserProfile
                {
                    FirstName = "Afsana",
                    LastName = "Mcmanus",
                    MiddleName = "Rae",
                    RegistrationDate = DateTime.Now,
                    Gender = "Non-binary",
                    BirthDate = DateTime.Now.AddYears(-16).AddDays(65),
                    Image= "https://miro.medium.com/max/1024/1*fNLMb7DHUQfn18w8YvyLQA.png"
                };
                var student1user = new DbUser
                {
                    Email = student1email,
                    UserName = student1email,
                    PhoneNumber = "+1 312-452-3307",
                    UserProfile = student1userprofile
                };
                _context.UserProfiles.Add(student1userprofile);
                result = userManager.CreateAsync(student1user, "Qwerty1-").Result;
                result = userManager.AddToRoleAsync(student1user, student1roleName).Result;
                _context.SaveChanges();
                var student1person = new Person { UserProfile = student1userprofile, IsActive = true };
                _context.Persons.Add(student1person);
                _context.SaveChanges();
                var student1 = new Student { Person = student1person, Marks = new List<Mark>(), Siblings = new List<Student>(), SchoolClassStudents = new List<SchoolClassStudent>(), StudentSubjects = new List<StudentSubject>(), SchoolStudents = new List<SchoolStudent>() };
                _context.Students.Add(student1);
                _context.SaveChanges();
                school.SchoolStudents.Add(new SchoolStudent { School = school, Student = student1 });

                var student3userprofile = new UserProfile
                {
                    FirstName = "Shah",
                    LastName = "Piper",
                    MiddleName = "Hansen",
                    RegistrationDate = DateTime.Now,
                    Gender = "Female",
                    BirthDate = DateTime.Now.AddYears(-10).AddDays(82),
                    Image = "https://d33wubrfki0l68.cloudfront.net/b6f06acb56add5ee5b6bd90048e0e11111fb312c/25c0a/img/thispersondoesnotexist.png"
                };
                var student3user = new DbUser
                {
                    Email = "shahpiperhansen@gmail.com",
                    UserName = "shahpiperhansen@gmail.com",
                    PhoneNumber = "+1 582-201-4640",
                    UserProfile = student3userprofile
                };
                _context.UserProfiles.Add(student3userprofile);
                result = userManager.CreateAsync(student3user, "Qwerty1-").Result;
                result = userManager.AddToRoleAsync(student3user, "Student").Result;
                _context.SaveChanges();
                var student3person = new Person { UserProfile = student3userprofile, IsActive = true };
                _context.Persons.Add(student3person);
                _context.SaveChanges();
                var student3 = new Student { Person = student3person, Marks = new List<Mark>(), Siblings = new List<Student>(), SchoolClassStudents = new List<SchoolClassStudent>(), StudentSubjects = new List<StudentSubject>(), SchoolStudents = new List<SchoolStudent>() };
                _context.Students.Add(student3);
                _context.SaveChanges();
                school.SchoolStudents.Add(new SchoolStudent { School = school, Student = student3 });

                ////////////////
                
                var student4userprofile = new UserProfile
                {
                    FirstName = "Alistair",
                    LastName = "Luci",
                    MiddleName = "Hines",
                    RegistrationDate = DateTime.Now,
                    Gender = "Male",
                    BirthDate = DateTime.Now.AddYears(-12).AddDays(82),
                    Image = "https://pbs.twimg.com/media/Ee-GRSuXkAA-3Er.jpg"
                };
                var student4user = new DbUser
                {
                    Email = ("AlistairLuciHines@gmail.com".ToLower()),
                    UserName = ("AlistairLuciHines@gmail.com".ToLower()),
                    PhoneNumber = "+1 219-839-3395",
                    UserProfile = student4userprofile
                };
                _context.UserProfiles.Add(student4userprofile);
                result = userManager.CreateAsync(student4user, "Qwerty1-").Result;
                result = userManager.AddToRoleAsync(student4user, "Student").Result;
                _context.SaveChanges();
                var student4person = new Person { UserProfile = student4userprofile, IsActive = true };
                _context.Persons.Add(student4person);
                _context.SaveChanges();
                var student4 = new Student { Person = student4person, Marks = new List<Mark>(), Siblings = new List<Student>(), SchoolClassStudents = new List<SchoolClassStudent>(), StudentSubjects = new List<StudentSubject>(), SchoolStudents = new List<SchoolStudent>() };
                _context.Students.Add(student4);
                _context.SaveChanges();
                school.SchoolStudents.Add(new SchoolStudent { School = school, Student = student4 });

                ////////////////5

                var student5userprofile = new UserProfile
                {
                    FirstName = "Maximilian",
                    LastName = "Trejo",
                    MiddleName = "Alford",
                    RegistrationDate = DateTime.Now,
                    Gender = "Male",
                    BirthDate = DateTime.Now.AddYears(-11).AddDays(88),
                    Image = "https://i.imgur.com/tcYfYeo.jpg"
                };
                var student5user = new DbUser
                {
                    Email = ("MaximilianTrejoAlford@gmail.com".ToLower()),
                    UserName = ("MaximilianTrejoAlford@gmail.com".ToLower()),
                    PhoneNumber = "+1 582-222-8754",
                    UserProfile = student5userprofile
                };
                _context.UserProfiles.Add(student5userprofile);
                result = userManager.CreateAsync(student5user, "Qwerty1-").Result;
                result = userManager.AddToRoleAsync(student5user, "Student").Result;
                _context.SaveChanges();
                var student5person = new Person { UserProfile = student5userprofile, IsActive = true };
                _context.Persons.Add(student5person);
                _context.SaveChanges();
                var student5 = new Student { Person = student5person, Marks = new List<Mark>(), Siblings = new List<Student>(), SchoolClassStudents = new List<SchoolClassStudent>(), StudentSubjects = new List<StudentSubject>(), SchoolStudents = new List<SchoolStudent>() };
                _context.Students.Add(student5);
                _context.SaveChanges();
                school.SchoolStudents.Add(new SchoolStudent { School = school, Student = student5 });

                ////////////////6

                var student6userprofile = new UserProfile
                {
                    FirstName = "Lana",
                    LastName = "Oneill",
                    MiddleName = "Amelia",
                    RegistrationDate = DateTime.Now,
                    Gender = "Male",
                    BirthDate = DateTime.Now.AddYears(-8).AddDays(120),
                    Image = "https://i.pinimg.com/originals/1a/5f/68/1a5f68900a3b36504a66035b82bd9090.png"
                };
                var student6user = new DbUser
                {
                    Email = ("LanaOneillAmelia@gmail.com".ToLower()),
                    UserName = ("LanaOneillAmelia@gmail.com".ToLower()),
                    PhoneNumber = "+1 223-355-2617",
                    UserProfile = student6userprofile
                };
                _context.UserProfiles.Add(student6userprofile);
                result = userManager.CreateAsync(student6user, "Qwerty1-").Result;
                result = userManager.AddToRoleAsync(student6user, "Student").Result;
                _context.SaveChanges();
                var student6person = new Person { UserProfile = student6userprofile, IsActive = true };
                _context.Persons.Add(student6person);
                _context.SaveChanges();
                var student6 = new Student { Person = student6person, Marks = new List<Mark>(), Siblings = new List<Student>(), SchoolClassStudents = new List<SchoolClassStudent>(), StudentSubjects = new List<StudentSubject>(), SchoolStudents = new List<SchoolStudent>() };
                _context.Students.Add(student6);
                _context.SaveChanges();
                school.SchoolStudents.Add(new SchoolStudent { School = school, Student = student6 });

                ////////////////7

                var student7userprofile = new UserProfile
                {
                    FirstName = "Alexandre",
                    LastName = "Lee",
                    MiddleName = "Carney",
                    RegistrationDate = DateTime.Now,
                    Gender = "Female",
                    BirthDate = DateTime.Now.AddYears(-13).AddDays(111),
                    Image = "https://pbs.twimg.com/media/EbLMssqWAAIL5Rn.jpg"
                };
                var student7user = new DbUser
                {
                    Email = ("AlexandreLeeCarney@gmail.com".ToLower()),
                    UserName = ("AlexandreLeeCarney@gmail.com".ToLower()),
                    PhoneNumber = "+1 231-631-3398",
                    UserProfile = student7userprofile
                };
                _context.UserProfiles.Add(student7userprofile);
                result = userManager.CreateAsync(student7user, "Qwerty1-").Result;
                result = userManager.AddToRoleAsync(student7user, "Student").Result;
                _context.SaveChanges();
                var student7person = new Person { UserProfile = student7userprofile, IsActive = true };
                _context.Persons.Add(student7person);
                _context.SaveChanges();
                var student7 = new Student { Person = student7person, Marks = new List<Mark>(), Siblings = new List<Student>(), SchoolClassStudents = new List<SchoolClassStudent>(), StudentSubjects = new List<StudentSubject>(), SchoolStudents = new List<SchoolStudent>() };
                _context.Students.Add(student7);
                _context.SaveChanges();
                school.SchoolStudents.Add(new SchoolStudent { School = school, Student = student7 });

                ////////////////8

                var student8userprofile = new UserProfile
                {
                    FirstName = "Humaira",
                    LastName = "Thompson",
                    MiddleName = "Denise",
                    RegistrationDate = DateTime.Now,
                    Gender = "Female",
                    BirthDate = DateTime.Now.AddYears(-14).AddDays(111),
                    Image = "https://i.imgur.com/Nf4Qwa7.jpg"
                };
                var student8user = new DbUser
                {
                    Email = ("HumairaThompsonDenise@gmail.com".ToLower()),
                    UserName = ("HumairaThompsonDenise@gmail.com".ToLower()),
                    PhoneNumber = "+1 582-333-3278",
                    UserProfile = student8userprofile
                };
                _context.UserProfiles.Add(student8userprofile);
                result = userManager.CreateAsync(student8user, "Qwerty1-").Result;
                result = userManager.AddToRoleAsync(student8user, "Student").Result;
                _context.SaveChanges();
                var student8person = new Person { UserProfile = student8userprofile, IsActive = true };
                _context.Persons.Add(student8person);
                _context.SaveChanges();
                var student8 = new Student { Person = student8person, Marks = new List<Mark>(), Siblings = new List<Student>(), SchoolClassStudents = new List<SchoolClassStudent>(), StudentSubjects = new List<StudentSubject>(), SchoolStudents = new List<SchoolStudent>() };
                _context.Students.Add(student8);
                _context.SaveChanges();
                school.SchoolStudents.Add(new SchoolStudent { School = school, Student = student8 });

                ////////////////9

                var student9userprofile = new UserProfile
                {
                    FirstName = "Petty",
                    LastName = "Loui",
                    MiddleName = "Irving",
                    RegistrationDate = DateTime.Now,
                    Gender = "Female",
                    BirthDate = DateTime.Now.AddYears(-12).AddDays(115),
                    Image = "https://i.imgur.com/0Hk1Uye.jpg"
                };
                var student9user = new DbUser
                {
                    Email = ("PettyLouiIrving@gmail.com".ToLower()),
                    UserName = ("PettyLouiIrving@gmail.com".ToLower()),
                    PhoneNumber = "+1 315-980-3097",
                    UserProfile = student9userprofile
                };
                _context.UserProfiles.Add(student9userprofile);
                result = userManager.CreateAsync(student9user, "Qwerty1-").Result;
                result = userManager.AddToRoleAsync(student9user, "Student").Result;
                _context.SaveChanges();
                var student9person = new Person { UserProfile = student9userprofile, IsActive = true };
                _context.Persons.Add(student9person);
                _context.SaveChanges();
                var student9 = new Student { Person = student9person, Marks = new List<Mark>(), Siblings = new List<Student>(), SchoolClassStudents = new List<SchoolClassStudent>(), StudentSubjects = new List<StudentSubject>(), SchoolStudents = new List<SchoolStudent>() };
                _context.Students.Add(student9);
                _context.SaveChanges();
                school.SchoolStudents.Add(new SchoolStudent { School = school, Student = student9 });

                //////////////
                var student2email = "edisonmcmanus@gmail.com";
                var student2roleName = "Student";
                var student2userprofile = new UserProfile
                {
                    FirstName = "Edison",
                    LastName = "Mcmanus",
                    MiddleName = "Catrin",
                    RegistrationDate = DateTime.Now,
                    Gender = "Male",
                    BirthDate = DateTime.Now.AddYears(-15).AddDays(82),
                    Image= "https://pbs.twimg.com/media/EXdS8nmXgAEN9oj.jpg"
                };
                var student2user = new DbUser
                {
                    Email = student2email,
                    UserName = student2email,
                    PhoneNumber = "+1 423-871-7315",
                    UserProfile = student2userprofile
                };
                _context.UserProfiles.Add(student2userprofile);
                result = userManager.CreateAsync(student2user, "Qwerty1-").Result;
                result = userManager.AddToRoleAsync(student2user, student2roleName).Result;
                _context.SaveChanges();
                var student2person = new Person { UserProfile = student2userprofile, IsActive = true };
                _context.Persons.Add(student2person);
                _context.SaveChanges();
                var student2 = new Student { Person = student2person, Marks = new List<Mark>(), Siblings = new List<Student>(), SchoolClassStudents = new List<SchoolClassStudent>(), StudentSubjects = new List<StudentSubject>(), SchoolStudents = new List<SchoolStudent>() };
                student2.Siblings.Add(student1);
                student1.Siblings.Add(student2);
                _context.Students.Add(student2);
                _context.SaveChanges();
                school.SchoolStudents.Add(new SchoolStudent { School = school, Student = student2 });


                ///////////////1
                var Teacheremail = "inigopitt@gmail.com";
                var TeacherroleName = "Teacher";
                var Teacheruserprofile = new UserProfile
                {
                    FirstName = "Inigo",
                    LastName = "Pitt",
                    MiddleName = "Mahek",
                    RegistrationDate = DateTime.Now,
                    Gender = "Male",
                    BirthDate = DateTime.Now.AddYears(-30).AddDays(55),
                    Image= "https://www.thepassivevoice.com/wp-content/uploads/2020/05/a1-2-640x640.jpg"
                };
                var Teacheruser = new DbUser
                {
                    Email = Teacheremail,
                    UserName = Teacheremail,
                    PhoneNumber = "+44 7364 649415",
                    UserProfile = Teacheruserprofile
                };
                _context.UserProfiles.Add(Teacheruserprofile);
                result = userManager.CreateAsync(Teacheruser, "Qwerty1-").Result;
                result = userManager.AddToRoleAsync(Teacheruser, TeacherroleName).Result;
                result = userManager.AddToRoleAsync(Teacheruser, "SchoolWorker").Result;
                _context.SaveChanges();
                var Teacherperson = new Person { UserProfile = Teacheruserprofile, IsActive = true };
                _context.Persons.Add(Teacherperson);
                var teacher = new SchoolWorker { Person = Teacherperson, RoleDescription = "Math Teacher", Subjects = new List<Subject>(), IsDirector = false, IsClassTeacher = true };
                _context.SchoolWorkers.Add(teacher);
                school.SchoolWorkers.Add(teacher);

                ///////////////2
                var Teacheremail2 = "MaureenMckeownFlowers@gmail.com";
                var Teacheruserprofile2 = new UserProfile
                {
                    FirstName = "Maureen",
                    LastName = "Mckeown",
                    MiddleName = "Flowers",
                    RegistrationDate = DateTime.Now,
                    Gender = "Male",
                    BirthDate = DateTime.Now.AddYears(-28).AddDays(55),
                    Image = "https://i.imgur.com/KSrujp9.jpg"
                };
                var Teacheruser2 = new DbUser
                {
                    Email = Teacheremail2.ToLower(),
                    UserName = Teacheremail2.ToLower(),
                    PhoneNumber = "+1 203-677-1403",
                    UserProfile = Teacheruserprofile2
                };
                _context.UserProfiles.Add(Teacheruserprofile2);
                result = userManager.CreateAsync(Teacheruser2, "Qwerty1-").Result;
                result = userManager.AddToRoleAsync(Teacheruser2, TeacherroleName).Result;
                result = userManager.AddToRoleAsync(Teacheruser2, "SchoolWorker").Result;
                _context.SaveChanges();
                var Teacherperson2 = new Person { UserProfile = Teacheruserprofile2, IsActive = true };
                _context.Persons.Add(Teacherperson2);
                var teacher2 = new SchoolWorker { Person = Teacherperson2, RoleDescription = "PE Teacher", Subjects = new List<Subject>(), IsDirector = false, IsClassTeacher = false };
                _context.SchoolWorkers.Add(teacher2);
                school.SchoolWorkers.Add(teacher2);

                ///////////////3
                var Teacheremail3 = "KadeLeggeNorris@gmail.com";
                var Teacheruserprofile3 = new UserProfile
                {
                    FirstName = "Kade",
                    LastName = "Legge",
                    MiddleName = "Norris",
                    RegistrationDate = DateTime.Now,
                    Gender = "Male",
                    BirthDate = DateTime.Now.AddYears(-37).AddDays(65),
                    Image = "https://www.thepassivevoice.com/wp-content/uploads/2020/05/a1-5-640x640.jpg"
                };
                var Teacheruser3 = new DbUser
                {
                    Email = Teacheremail3.ToLower(),
                    UserName = Teacheremail3.ToLower(),
                    PhoneNumber = "+1 217-345-5031",
                    UserProfile = Teacheruserprofile3
                };
                _context.UserProfiles.Add(Teacheruserprofile3);
                result = userManager.CreateAsync(Teacheruser3, "Qwerty1-").Result;
                result = userManager.AddToRoleAsync(Teacheruser3, TeacherroleName).Result;
                result = userManager.AddToRoleAsync(Teacheruser3, "SchoolWorker").Result;
                _context.SaveChanges();
                var Teacherperson3 = new Person { UserProfile = Teacheruserprofile3, IsActive = true };
                _context.Persons.Add(Teacherperson3);
                var teacher3= new SchoolWorker { Person = Teacherperson3, RoleDescription = "Arts Teacher", Subjects = new List<Subject>(), IsDirector = false, IsClassTeacher = false };
                _context.SchoolWorkers.Add(teacher3);
                school.SchoolWorkers.Add(teacher3);

                var Parentemail = "randallwhitney@gmail.com";
                var ParentroleName = "Parent";
                var Parentuserprofile = new UserProfile
                {
                    FirstName = "Randall",
                    LastName = "Whitney",
                    MiddleName = "Stacy",
                    RegistrationDate = DateTime.Now,
                    Gender = "Male",
                    BirthDate = DateTime.Now.AddYears(-51).AddDays(69),
                    Image= "https://i.redd.it/ni4l65qa5mg21.jpg"

                };
                var Parentuser = new DbUser
                {
                    Email = Parentemail,
                    UserName = Parentemail,
                    PhoneNumber = "+44 7911 023315",
                    UserProfile = Parentuserprofile
                };
                _context.UserProfiles.Add(Parentuserprofile);
                result = userManager.CreateAsync(Parentuser, "Qwerty1-").Result;
                result = userManager.AddToRoleAsync(Parentuser, ParentroleName).Result;
                _context.SaveChanges();
                var parent = new Parent { Kids = new List<Student>(), UserProfile = Parentuserprofile };
                parent.Kids.Add(student1);
                parent.Kids.Add(student2);
                _context.Parents.Add(parent);

                ///1
                var schoolClass = new SchoolClass { CreateDate = new System.DateTime(2021, 09, 01, 00, 00, 00), FinalDate = new System.DateTime(2021, 09, 01, 00, 00, 00).AddDays(365), IsActive = true, Name = "10B", SchoolClassStudents = new List<SchoolClassStudent>(), Subjects = new List<Subject>(), Teacher = teacher };
                _context.SchoolClasses.Add(schoolClass);
                _context.SaveChanges();
                school.Classes.Add(schoolClass);
                student1.SchoolClassStudents.Add(new SchoolClassStudent { SchoolClass = schoolClass, Student = student1 });
                student2.SchoolClassStudents.Add(new SchoolClassStudent { SchoolClass = schoolClass, Student = student2 });
                student3.SchoolClassStudents.Add(new SchoolClassStudent { SchoolClass = schoolClass, Student = student3 });
                student4.SchoolClassStudents.Add(new SchoolClassStudent { SchoolClass = schoolClass, Student = student4 });
                var subject = new Subject { SchoolClass = schoolClass, Description = "Second Half", Journals = new List<Journal>(), Lessons = new List<Lesson>(), Name = "Math", StudentSubjects = new List<StudentSubject>(), Teacher = teacher };
                _context.Subjects.Add(subject);
                _context.SaveChanges();
                subject.StudentSubjects.Add(new StudentSubject { Student = student1, Subject = subject });
                subject.StudentSubjects.Add(new StudentSubject { Student = student2, Subject = subject });
                subject.StudentSubjects.Add(new StudentSubject { Student = student3, Subject = subject });
                subject.StudentSubjects.Add(new StudentSubject { Student = student4, Subject = subject });



                var subject2 = new Subject { SchoolClass = schoolClass, Description = "Full Class", Journals = new List<Journal>(), Lessons = new List<Lesson>(), Name = "Geography", StudentSubjects = new List<StudentSubject>(), Teacher = DirectorschoolWorker };
                _context.Subjects.Add(subject2);
                _context.SaveChanges();
                subject2.StudentSubjects.Add(new StudentSubject { Student = student1, Subject = subject2 });
                subject2.StudentSubjects.Add(new StudentSubject { Student = student2, Subject = subject2 });


                ///2
                var schoolClass2 = new SchoolClass { CreateDate = new System.DateTime(2021, 09, 01, 00, 00, 00), FinalDate = new System.DateTime(2021, 09, 01, 00, 00, 00).AddDays(365), IsActive = true, Name = "8A", SchoolClassStudents = new List<SchoolClassStudent>(), Subjects = new List<Subject>(), Teacher = teacher2, School=school};
                _context.SchoolClasses.Add(schoolClass2);
                _context.SaveChanges();
                school.Classes.Add(schoolClass2);
                student5.SchoolClassStudents.Add(new SchoolClassStudent { SchoolClass = schoolClass2, Student = student5 });
                student6.SchoolClassStudents.Add(new SchoolClassStudent { SchoolClass = schoolClass2, Student = student6 });
                student7.SchoolClassStudents.Add(new SchoolClassStudent { SchoolClass = schoolClass2, Student = student7 });
                student8.SchoolClassStudents.Add(new SchoolClassStudent { SchoolClass = schoolClass2, Student = student8 });
                

                var subject3 = new Subject { SchoolClass = schoolClass2, Description = "Full Class", Journals = new List<Journal>(), Lessons = new List<Lesson>(), Name = "Arts", StudentSubjects = new List<StudentSubject>(), Teacher = teacher3 };
                _context.Subjects.Add(subject3);
                _context.SaveChanges();
                subject3.StudentSubjects.Add(new StudentSubject { Student = student5, Subject = subject3 });
                subject3.StudentSubjects.Add(new StudentSubject { Student = student6, Subject = subject3 });
                subject3.StudentSubjects.Add(new StudentSubject { Student = student7, Subject = subject3 });
                subject3.StudentSubjects.Add(new StudentSubject { Student = student8, Subject = subject3 });

                var subject4 = new Subject { SchoolClass = schoolClass2, Description = "Athletics", Journals = new List<Journal>(), Lessons = new List<Lesson>(), Name = "PE", StudentSubjects = new List<StudentSubject>(), Teacher = teacher2 };
                _context.Subjects.Add(subject4);
                _context.SaveChanges();
                subject4.StudentSubjects.Add(new StudentSubject { Student = student1, Subject = subject4 });
                subject4.StudentSubjects.Add(new StudentSubject { Student = student3, Subject = subject4 });
                subject4.StudentSubjects.Add(new StudentSubject { Student = student7, Subject = subject4 });
                subject4.StudentSubjects.Add(new StudentSubject { Student = student8, Subject = subject4 });

                var journal = new Journal { Subject = subject, Marks = new List<Mark>(), LessonInfos=new List<LessonInfo>() };
                _context.Journals.Add(journal);
                _context.SaveChanges();
                var mark1 = new Mark { Description = "Classwork", Journal = journal, MarkType = MarkType.Current, Student = student1, Value = 12, TimeSet = DateTime.Now };
                var mark5 = new Mark { Description = "Classwork", Journal = journal, MarkType = MarkType.Current, Student = student1, Value = 12, TimeSet = DateTime.Now.AddDays(-10) };
                var mark6 = new Mark { Description = "Homework", Journal = journal, MarkType = MarkType.Current, Student = student1, Value = 1, TimeSet = DateTime.Now.AddDays(-3) };
                var mark2 = new Mark { Description = "Stereometry", Journal = journal, MarkType = MarkType.Semester1, Student = student2, Value = 10, TimeSet = DateTime.Now.AddDays(-5) };
                var lessonInfo1 = new LessonInfo { Date = DateTime.Now.AddDays(-2), Homework = "p. 39 ex 1-4", Journal = journal, Theme = "Triangle" };
                var lessonInfo1_2 = new LessonInfo { Date = DateTime.Now.AddDays(-1), Homework = "Essay", Journal = journal, Theme = "Triangle" };
                _context.Marks.Add(mark1);
                _context.Marks.Add(mark2);
                _context.Marks.Add(mark5);
                _context.Marks.Add(mark6);
                _context.LessonInfos.Add(lessonInfo1);
                _context.LessonInfos.Add(lessonInfo1_2);
                _context.SaveChanges();
                journal.Marks.Add(mark1);
                journal.Marks.Add(mark2);
                journal.Marks.Add(mark5);
                journal.Marks.Add(mark6);
                journal.LessonInfos.Add(lessonInfo1);
                journal.LessonInfos.Add(lessonInfo1_2);
                subject.Journals.Add(journal);

                /////////////3
                var journal3 = new Journal { Subject = subject4, Marks = new List<Mark>(), LessonInfos = new List<LessonInfo>() };
                _context.Journals.Add(journal3);
                _context.SaveChanges();
                var journal3mark1 = new Mark { Description = "Classwork", Journal = journal3, MarkType = MarkType.Current, Student = student5, Value = 3, TimeSet = DateTime.Now };
                var journal3mark5 = new Mark { Description = "Classwork", Journal = journal3, MarkType = MarkType.Current, Student = student6, Value = 4, TimeSet = DateTime.Now.AddDays(-7) };
                var journal3mark6 = new Mark { Description = "Homework", Journal = journal3, MarkType = MarkType.Current, Student = student7, Value = 1, TimeSet = DateTime.Now.AddDays(-14) };
                var journal3mark7 = new Mark { Description = "Classwork", Journal = journal3, MarkType = MarkType.Semester1, Student = student8, Value = 10, TimeSet = DateTime.Now.AddDays(-8) };
                var journal3lessonInfo1 = new LessonInfo { Date = DateTime.Now.AddDays(-2), Homework = "p. 39 ex 1-4", Journal = journal, Theme = "Voleyball" };
                var journal3lessonInfo1_2 = new LessonInfo { Date = DateTime.Now.AddDays(-1), Homework = "Essay", Journal = journal, Theme = "Football" };
                _context.Marks.Add(journal3mark1);
                _context.Marks.Add(journal3mark7);
                _context.Marks.Add(journal3mark5);
                _context.Marks.Add(journal3mark6);
                _context.LessonInfos.Add(journal3lessonInfo1);
                _context.LessonInfos.Add(journal3lessonInfo1_2);
                _context.SaveChanges();
                journal.Marks.Add(journal3mark1);
                journal.Marks.Add(journal3mark7);
                journal.Marks.Add(journal3mark6);
                journal.Marks.Add(journal3mark5);
                journal.LessonInfos.Add(journal3lessonInfo1);
                journal.LessonInfos.Add(journal3lessonInfo1_2);
                subject4.Journals.Add(journal3);

                /////////////4
                var journal4 = new Journal { Subject = subject3, Marks = new List<Mark>(), LessonInfos = new List<LessonInfo>() };
                _context.Journals.Add(journal4);
                _context.SaveChanges();
                var journal4mark1 = new Mark { Description = "Classwork", Journal = journal4, MarkType = MarkType.Current, Student = student1, Value = 3, TimeSet = DateTime.Now };
                var journal4mark5 = new Mark { Description = "Classwork", Journal = journal4, MarkType = MarkType.Current, Student = student2, Value = 4, TimeSet = DateTime.Now.AddDays(-7) };
                var journal4mark6 = new Mark { Description = "Homework", Journal = journal4, MarkType = MarkType.Current, Student = student7, Value = 1, TimeSet = DateTime.Now.AddDays(-14) };
                var journal4mark7 = new Mark { Description = "Classwork", Journal = journal4, MarkType = MarkType.Semester1, Student = student8, Value = 10, TimeSet = DateTime.Now.AddDays(-8) };
                var journal4lessonInfo1 = new LessonInfo { Date = DateTime.Now.AddDays(-2), Homework = "p. 39 ex 1-4", Journal = journal, Theme = "Voleyball" };
                var journal4lessonInfo1_2 = new LessonInfo { Date = DateTime.Now.AddDays(-1), Homework = "Essay", Journal = journal, Theme = "Football" };
                _context.Marks.Add(journal4mark1);
                _context.Marks.Add(journal4mark7);
                _context.Marks.Add(journal4mark5);
                _context.Marks.Add(journal4mark6);
                _context.LessonInfos.Add(journal4lessonInfo1);
                _context.LessonInfos.Add(journal4lessonInfo1_2);
                _context.SaveChanges();
                journal.Marks.Add(journal4mark1);
                journal.Marks.Add(journal4mark7);
                journal.Marks.Add(journal4mark6);
                journal.Marks.Add(journal4mark5);
                journal.LessonInfos.Add(journal4lessonInfo1);
                journal.LessonInfos.Add(journal4lessonInfo1_2);
                subject.Journals.Add(journal4);

                var journal2 = new Journal { Subject = subject2, Marks = new List<Mark>() };
                _context.Journals.Add(journal2);
                _context.SaveChanges();
                var mark3 = new Mark { Description = "Homework", Journal = journal2, MarkType = MarkType.Current, Student = student1, Value = 5, TimeSet = DateTime.Now.AddDays(-2) };
                var mark4 = new Mark { Description = "Asia", Journal = journal2, MarkType = MarkType.Semester1, Student = student2, Value = 8, TimeSet = DateTime.Now.AddDays(-10) };
                var mark8 = new Mark { Description = "Asia", Journal = journal2, MarkType = MarkType.Current, Student = student2, Value = 12, TimeSet = DateTime.Now.AddDays(-15) };
                var mark9 = new Mark { Description = "Asia", Journal = journal2, MarkType = MarkType.Current, Student = student2, Value = 11, TimeSet = DateTime.Now.AddDays(-12) };
                var lessonInfo2 = new LessonInfo { Date = DateTime.Now.AddDays(2), Homework = "test", Journal = journal2, Theme = "Asia" };
                var lessonInfo2_2 = new LessonInfo { Date = DateTime.Now.AddDays(1), Homework = "Essay", Journal = journal2, Theme = "Asia" };
                _context.Marks.Add(mark3);
                _context.Marks.Add(mark4);
                _context.Marks.Add(mark8);
                _context.Marks.Add(mark9);
                _context.LessonInfos.Add(lessonInfo2);
                _context.LessonInfos.Add(lessonInfo2_2);
                _context.SaveChanges();
                journal2.Marks.Add(mark3);
                journal2.Marks.Add(mark3);
                journal2.Marks.Add(mark8);
                journal2.Marks.Add(mark9);
                journal.LessonInfos.Add(lessonInfo2);
                journal.LessonInfos.Add(lessonInfo2_2);
                subject2.Journals.Add(journal2);

                teacher.Class = schoolClass;


                var schedule = new Schedule { Lessons = new List<Lesson>(), SchoolClass = schoolClass };
                var lesson1 = new Lesson { Cabinet = "16",  Subject = subject,  Time=new System.DateTime ( 2000, 1, 01, 8, 30, 00 ), WeekDay = 5 };
                var lesson2 = new Lesson { Cabinet = "15",  Subject = subject, Time = new System.DateTime(2000, 1, 01, 9, 30, 00), WeekDay = 4 };

                var lesson3 = new Lesson { Cabinet = "8", Subject = subject2, Time = new System.DateTime(2000, 1, 01, 12, 25, 00), WeekDay = 2 };
                var lesson4 = new Lesson { Cabinet = "15",Subject = subject2, Time = new System.DateTime(2000, 1, 01, 13, 15, 00), WeekDay = 5 };
                var lesson5 = new Lesson { Cabinet = "2", Subject = subject2, Time = new System.DateTime(2000, 1, 01, 14, 40, 00), WeekDay = 5 };
                var lesson6 = new Lesson { Cabinet = "1", Subject = subject2, Time = new System.DateTime(2000, 1, 01, 13, 15, 00), WeekDay = 5 };

                var schedule2 = new Schedule { Lessons = new List<Lesson>(), SchoolClass = schoolClass2 };
                var schedule2lesson1 = new Lesson { Cabinet = "16", Subject = subject2, Time = new System.DateTime(2000, 1, 01, 8, 30, 00), WeekDay = 5 };
                var schedule2lesson2 = new Lesson { Cabinet = "15", Subject = subject3, Time = new System.DateTime(2000, 1, 01, 9, 30, 00), WeekDay = 4 };

                var schedule2lesson3 = new Lesson { Cabinet = "8", Subject = subject4, Time = new System.DateTime(2000, 1, 01, 12, 25, 00), WeekDay = 2 };
                var schedule2lesson4 = new Lesson { Cabinet = "15", Subject = subject2, Time = new System.DateTime(2000, 1, 01, 13, 15, 00), WeekDay = 5 };
                var schedule2lesson5 = new Lesson { Cabinet = "2", Subject = subject3, Time = new System.DateTime(2000, 1, 01, 14, 40, 00), WeekDay = 5 };
                var schedule2lesson6 = new Lesson { Cabinet = "1", Subject = subject4, Time = new System.DateTime(2000, 1, 01, 13, 15, 00), WeekDay = 5 };
                _context.Schedules.Add(schedule);
                _context.Schedules.Add(schedule2);
                _context.Lessons.Add(lesson1);
                _context.Lessons.Add(lesson2);
                _context.Lessons.Add(lesson3);
                _context.Lessons.Add(lesson3);
                _context.Lessons.Add(lesson5);
                _context.Lessons.Add(lesson6);
                _context.Lessons.Add(schedule2lesson1);
                _context.Lessons.Add(schedule2lesson2);
                _context.Lessons.Add(schedule2lesson3);
                _context.Lessons.Add(schedule2lesson3);
                _context.Lessons.Add(schedule2lesson5);
                _context.Lessons.Add(schedule2lesson6);
                _context.SaveChanges();
                schedule.Lessons.Add(lesson1);
                schedule.Lessons.Add(lesson2);
                schedule.Lessons.Add(lesson3);
                schedule.Lessons.Add(lesson3);
                schedule.Lessons.Add(lesson5);
                schedule.Lessons.Add(lesson6);
                schedule2.Lessons.Add(schedule2lesson1);
                schedule2.Lessons.Add(schedule2lesson2);
                schedule2.Lessons.Add(schedule2lesson3);
                schedule2.Lessons.Add(schedule2lesson3);
                schedule2.Lessons.Add(schedule2lesson5);
                schedule2.Lessons.Add(schedule2lesson6);
                schoolClass.Schedule=schedule;
                schoolClass2.Schedule = schedule2;
                _context.SaveChanges();


                _context.SaveChanges();


            }
        }
        public static void SeedTables(EFDbContext _context)
        {

        }
        public static void SeedRoles(RoleManager<DbRole> roleManager)
        {

            var count = roleManager.Roles.Count();
            if (count <= 0)
            {
                var roleName = "User";
                var result = roleManager.CreateAsync(new DbRole
                {
                    Name = roleName
                }).Result;
                roleName = "Admin";
                result = roleManager.CreateAsync(new DbRole
                {
                    Name = roleName
                }).Result;
                roleName = "Student";
                result = roleManager.CreateAsync(new DbRole
                {
                    Name = roleName
                }).Result;
                roleName = "Parent";
                result = roleManager.CreateAsync(new DbRole
                {
                    Name = roleName
                }).Result;
                roleName = "Director";
                result = roleManager.CreateAsync(new DbRole
                {
                    Name = roleName
                }).Result;
                roleName = "Manager";
                result = roleManager.CreateAsync(new DbRole
                {
                    Name = roleName
                }).Result;
                roleName = "Teacher";
                result = roleManager.CreateAsync(new DbRole
                {
                    Name = roleName
                }).Result;
                roleName = "SchoolWorker";
                result = roleManager.CreateAsync(new DbRole
                {
                    Name = roleName
                }).Result;
            }
        }

        public static void SeedData(IServiceProvider services, IHostingEnvironment env, IConfiguration config)
        {
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {

                var manager = scope.ServiceProvider.GetRequiredService<UserManager<DbUser>>();
                var managerRole = scope.ServiceProvider.GetRequiredService<RoleManager<DbRole>>();
                var context = scope.ServiceProvider.GetRequiredService<EFDbContext>();
                //var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();


                //SeederDB.SeedRegions(context);
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                SeederDB.SeedRoles(managerRole);
                SeederDB.SeedUsers(manager, context);
                //SeederDB.SeedTables(context);
            }
        }


    }
}
