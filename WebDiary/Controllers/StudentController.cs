using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using WebDiary.Data.Interfaces;
using WebDiary.Data.Models;
using WebDiary.Data.ViewModels;
using WebDiary.Models;

namespace WebDiary.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudent _students;
        public StudentController(IStudent students)
        {
            _students = students;
        }

        [Authorize]
        [Route("Student/StudentPublicAccount")]
        public ActionResult StudentPublicAccount(string studentId)
        {
            

            Student student = null;
            var schoolClasses = new List<SchoolClass>();
            if (string.IsNullOrEmpty(studentId))
            {

            }
            else
            {
                var info = HttpContext.Session.GetString("UserInfo");
                if (info != null)
                {
                    var result = JsonConvert.DeserializeObject<UserInfo>(info);
                    var id = result.UserId;
                    if (id.ToLower() == studentId)
                    {
                        return RedirectToAction("StudentPersonalAccount", "Account");
                    }
                }
                else
                {
                    return RedirectToAction("Logout", "Account");
                }
                student = _students.GetStudents.FirstOrDefault(x => x.Id.ToLower() == studentId.ToLower());
               foreach(SchoolClassStudent scs in student.SchoolClassStudents)
                {
                    schoolClasses.Add(scs.SchoolClass);
                }
                

            }
            var studentsObj = new StudentViewModel { GetStudent = student, SchoolClasses = schoolClasses };
            return View(studentsObj);
        }

        [Authorize]
        [Route("Student/GetStudents")]
        public ViewResult GetStudents(string schoolId, string classId, string subjectId, string searchString)
        {
            
            List<Student> allStudents = _students.GetStudents.ToList();
            var students = new List<Student>();
            School school = null;
            SchoolClass schoolClass = null;
            Subject subject = null;
            var studentsObj = new ListStudentViewModel();
            if (!string.IsNullOrEmpty(schoolId))
            {
                foreach (Student s in allStudents)
                {
                    foreach (var sc in s.SchoolStudents)
                    {
                        if (sc.School.Id.ToLower() == schoolId.ToLower())
                        {
                            students.Add(s);
                            school = sc.School;
                        }
                    }

                }

            }
            else if (!string.IsNullOrEmpty(classId))
            {
                foreach (Student s in allStudents)
                {
                    foreach (var sc in s.SchoolClassStudents)
                    {
                        if (sc.SchoolClass.Id.ToLower() == classId.ToLower())
                        {
                            students.Add(s);
                            school = sc.SchoolClass.School;
                            schoolClass = sc.SchoolClass;
                        }
                    }

                }
            }
            else if (!string.IsNullOrEmpty(subjectId))
            {
                foreach (Student s in allStudents)
                {
                    foreach (StudentSubject subj in s.StudentSubjects)
                    {
                        if (subj.Subject.Id.ToLower() == subjectId.ToLower())
                        {
                            students.Add(s);
                            subject = subj.Subject;
                            school = subj.Subject.SchoolClass.School;
                            schoolClass = subj.Subject.SchoolClass;
                        }

                    }

                }

            }
            else
            {
                students = allStudents;
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                var _students = new List<Student>();
                foreach (var s in students)
                {
                    string fullName = s.Person.UserProfile.FirstName.ToString() + s.Person.UserProfile.MiddleName.ToString() + s.Person.UserProfile.LastName.ToString();
                    if (fullName.ToLower().Contains(searchString.ToLower()))
                    {
                        _students.Add(s);
                    }
                }
                students = _students;
            }

            studentsObj = new ListStudentViewModel { GetStudents = students.OrderBy(x => x.Person.UserProfile.LastName), School = school, Subject = subject, SchoolClass = schoolClass };
            return View(studentsObj);
        }

        public ActionResult GetStudentsSchoolWorker()
        {
            var info = HttpContext.Session.GetString("UserInfo");
            string _schoolId = null;
            if (info != null)
            {
                var result = JsonConvert.DeserializeObject<UserInfo>(info);
                var id = result.UserId;
                foreach(var s in _students.GetStudents.ToList())
                {
                    foreach(var sc in s.SchoolStudents)
                    {
                        foreach (var scw in sc.School.SchoolWorkers)
                        {
                            if (scw.Person.UserProfile.User.Id.ToLower() == id.ToLower())
                            {
                                _schoolId = sc.School.Id;
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                return RedirectToAction("Logout", "Account");
            }
            return RedirectToAction("GetStudents", new { schoolId =_schoolId });
        }

        
    }
}
