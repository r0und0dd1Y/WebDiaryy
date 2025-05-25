using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDiary.Data.Interfaces;
using WebDiary.Data.Models;
using WebDiary.Data.ViewModels;
using WebDiary.Models;

namespace WebDiary.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ISubject _subjects;
        public SubjectController(ISubject subjects)
        {
            _subjects = subjects;
        }
        [Route("Subject/GetSubject")]
        public ViewResult GetSubject(string subjectId)
        {
            Subject subject = null;
            if (string.IsNullOrEmpty(subjectId))
            {

            }
            else
            {
                subject = _subjects.GetSubjects.FirstOrDefault(x => x.Id.ToLower() == subjectId.ToLower());

            }
            var subjectObj = new SubjectViewModel
            {
                GetSubject = subject,
                SchoolClass = subject.SchoolClass,
                Teacher = subject.Teacher
            };
            return View(subjectObj);
        }

        [Route("Subject/GetSubjectsStudent")]
        public ViewResult GetSubjectsStudent(string studentId)
        {
            //var info = HttpContext.Session.GetString("UserInfo");
            //if (info != null)
            //{
            //    var result = JsonConvert.DeserializeObject<UserInfo>(info);
            //    var id = result.UserId;
            //}

            List<Subject> subjects = null;
            List<SubjectViewModel> subjectsViewModels = null;
            if (string.IsNullOrEmpty(studentId))
            {

            }
            else
            {
                foreach (Subject s in _subjects.GetSubjects)
                {
                    foreach (SchoolClassStudent s1 in s.SchoolClass.SchoolClassStudents)
                    {
                        if (s1.Student.Id.ToLower() == studentId.ToLower())
                        {
                            subjects.Add(s);
                        }
                    }
                }
                foreach (Subject s in subjects)
                {
                    subjectsViewModels.Add(new SubjectViewModel { GetSubject = s, Teacher = s.Teacher, SchoolClass = s.SchoolClass });
                }
                //if (string.Equals("Benzin", category, StringComparison.OrdinalIgnoreCase)) 
                //{
                //    cars = _cars.GetCars.Where(x => x.Category.CategoryName.Equals("Benzin"))
                //        .OrderBy(i => i.Id);
                //}
                //else if (string.Equals("Diezel", category, StringComparison.OrdinalIgnoreCase))
                //{
                //    cars = _cars.GetCars.Where(x => x.Category.CategoryName.Equals("Diezel"))
                //        .OrderBy(i => i.Id);
                //}
                //else
                //{
                //    cars = _cars.GetCars.Where(x => x.Category.CategoryName.Equals("Electro"))
                //        .OrderBy(i => i.Id);
                //}
            }
            var subjectsObj = new ListSubjectViewModel { GetSubjects = subjectsViewModels };
            return View(subjectsObj);
        }

        [Authorize]
        [Route("Subject/GetSubjectsList")]
        public ViewResult GetSubjectsList(string schoolWorkerId, string schoolClassId)
        {
            //var info = HttpContext.Session.GetString("UserInfo");
            //if (info != null)
            //{
            //    var result = JsonConvert.DeserializeObject<UserInfo>(info);
            //    var id = result.UserId;
            //}

            IEnumerable<Subject> subjects;
            List<SubjectViewModel> subjectViewModels = new List<SubjectViewModel>();
            SchoolWorker schoolWorker = null;
            ListSubjectViewModel subjectsObj = null;

            SchoolClass schoolClass = null;
            if (!string.IsNullOrEmpty(schoolWorkerId))
            {
                subjects = _subjects.GetSubjects.Where(x => x.Teacher.Id.ToLower() == schoolWorkerId.ToLower());
                foreach (Subject s in subjects)
                {
                    subjectViewModels.Add(new SubjectViewModel { GetSubject = s, Teacher = s.Teacher, SchoolClass = s.SchoolClass });
                }
                schoolWorker = subjects.FirstOrDefault().Teacher;
                subjectsObj = new ListSubjectViewModel { GetSubjects = subjectViewModels, SchoolWorker = schoolWorker };
            }
            else if(!string.IsNullOrEmpty(schoolClassId))
            {
                subjects = _subjects.GetSubjects.Where(x => x.SchoolClass.Id.ToLower() == schoolClassId.ToLower());
                foreach (Subject s in subjects)
                {
                    subjectViewModels.Add(new SubjectViewModel { GetSubject = s, Teacher = s.Teacher, SchoolClass = s.SchoolClass });
                }
                schoolClass = subjects.FirstOrDefault().SchoolClass;
                subjectsObj = new ListSubjectViewModel { GetSubjects = subjectViewModels, SchoolClass = schoolClass };
            }
            return View(subjectsObj);
        }

        //[Route("Subject/GetSubjectsSchoolClass")]
        //public ViewResult GetSubjectsSchoolClass(string schoolClassId)
        //{
        //    //var info = HttpContext.Session.GetString("UserInfo");
        //    //if (info != null)
        //    //{
        //    //    var result = JsonConvert.DeserializeObject<UserInfo>(info);
        //    //    var id = result.UserId;
        //    //}

        //    IEnumerable<Subject> subjects = null;
        //    List<SubjectViewModel> subjectViewModels = null;
        //    SchoolClass schoolClass = null;
        //    if (string.IsNullOrEmpty(schoolClassId))
        //    {

        //    }
        //    else
        //    {
        //        subjects = _subjects.GetSubjects.Where(x => x.SchoolClass.Id.ToLower() == schoolClassId.ToLower());
        //        foreach (Subject s in subjects)
        //        {
        //            subjectViewModels.Add(new SubjectViewModel { GetSubject = s, Teacher = s.Teacher, SchoolClass = s.SchoolClass });
        //        }
        //        schoolClass = subjects.FirstOrDefault().SchoolClass;
        //    }
        //    var subjectsObj = new ListSubjectViewModel { GetSubjects = subjectViewModels, SchoolClass = schoolClass };
        //    return View(subjectsObj);

        //}

        [Authorize]
        [Route("Journal/RedirectStudentSubjects")]
        public ActionResult RedirectStudentSubjects()
        {
            var info = HttpContext.Session.GetString("UserInfo");
            string _studentid = null;
            if (info != null)
            {
                var result = JsonConvert.DeserializeObject<UserInfo>(info);
                var id = result.UserId;
                _studentid = id;
            }
            else
            {
                return RedirectToAction("Logout", "Account");
            }
            return RedirectToAction("GetSubjectsList", new { studentId = _studentid });
        }

        [Authorize]
        [Route("Journal/RedirectSchoolWorkerSubjects")]
        public ActionResult RedirectSchoolWorkerSubjects()
        {
            var info = HttpContext.Session.GetString("UserInfo");
            string _schoolWorkerId = null;
            if (info != null)
            {
                var result = JsonConvert.DeserializeObject<UserInfo>(info);
                var id = result.UserId;
                _schoolWorkerId = id;
            }
            else
            {
                return RedirectToAction("Logout", "Account");
            }
            return RedirectToAction("GetSubjectsList", new { schoolWorkerId = _schoolWorkerId });
        }
    }
}
