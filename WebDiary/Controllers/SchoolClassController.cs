using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebDiary.Data.Interfaces;
using WebDiary.Data.Models;
using WebDiary.Data.ViewModels;
using WebDiary.Models;

namespace WebDiary.Controllers
{
    public class SchoolClassController : Controller
    {
        ISchoolClass _schoolClasses;
        public SchoolClassController(ISchoolClass schoolClasses)
        {
            _schoolClasses = schoolClasses;
        }

        [Authorize]
        [Route("SchoolClass/SchoolClassesList")]
        public ActionResult SchoolClassesList(string schoolId, string studentId, string searchString)
        {
            var schoolClasses = new List<SchoolClass>();
            School school = null;
            Student student = null;

            if (!string.IsNullOrEmpty(schoolId))
            {
                foreach (var scs in _schoolClasses.GetSchoolClasses.ToList())
                {
                    if (scs.School.Id.ToLower() == schoolId.ToLower())
                    {
                        schoolClasses.Add(scs);
                        school = scs.School;
                    }
                }
            }
            else if (!string.IsNullOrEmpty(studentId))
            {
                if (!User.IsInRole("Student"))
                {
                    return RedirectToAction("Index", "Home");
                }
                foreach (var scs in _schoolClasses.GetSchoolClasses.ToList())
                {
                    foreach (var st in scs.SchoolClassStudents)
                    {
                        if (st.Student.Id.ToLower() == studentId.ToLower())
                        {
                            schoolClasses.Add(scs);
                            student = st.Student;
                        }
                    }
                }

            }
            else
            {
                if (!User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "Home");
                }
                schoolClasses = _schoolClasses.GetSchoolClasses.ToList();
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                schoolClasses = schoolClasses.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }

            var schoolClassObj = new ListSchoolClassViewModel { SchoolClasses = schoolClasses.OrderBy(x => x.Name), Student = student, School = school };
            return View(schoolClassObj);
        }

        [Authorize]
        [Route("SchoolClass/SchoolClass")]
        public ViewResult SchoolClass(string id)
        {
            var schoolClass = _schoolClasses.GetSchoolClasses.FirstOrDefault(x => x.Id.ToLower() == id.ToLower());
            var schoolClassObj = new SchoolClassViewModel { SchoolClass = schoolClass };
            return View(schoolClassObj);

        }



        public ActionResult SchoolClassesListSchoolWorker()
        {
            var info = HttpContext.Session.GetString("UserInfo");
            string _schoolId = null;
            if (info != null)
            {
                var result = JsonConvert.DeserializeObject<UserInfo>(info);
                var id = result.UserId;
                foreach (var s in _schoolClasses.GetSchoolClasses.ToList())
                {
                    foreach (var scw in s.School.SchoolWorkers)
                    {
                        if (scw.Person.UserProfile.User.Id.ToLower() == id.ToLower())
                        {
                            _schoolId = s.School.Id;
                            break;
                        }
                    }

                }
            }
            else
            {
                return RedirectToAction("Logout", "Account");
            }
            return RedirectToAction("SchoolClassesList", new { schoolId = _schoolId });
        }

        public ActionResult SchoolClassTeacherSchoolWorker()
        {
            var info = HttpContext.Session.GetString("UserInfo");
            string _schoolClassId = null;
            if (info != null)
            {
                var result = JsonConvert.DeserializeObject<UserInfo>(info);
                var id = result.UserId;
                foreach (var s in _schoolClasses.GetSchoolClasses.ToList())
                {
                    if (s.Teacher.Id.ToLower() == id.ToLower())
                    {
                        _schoolClassId = s.Id;
                        break;
                    }


                }
            }
            else
            {
                return RedirectToAction("Logout", "Account");
            }
            return RedirectToAction("SchoolClass", new { id = _schoolClassId });
        }

        public ActionResult SchoolClassesListStudent()
        {
            var info = HttpContext.Session.GetString("UserInfo");
            string _studentId = null;
            if (info != null)
            {
                var result = JsonConvert.DeserializeObject<UserInfo>(info);
                var id = result.UserId;
                _studentId = id;


            }
            else
            {
                return RedirectToAction("Logout", "Account");
            }
            return RedirectToAction("SchoolClassesList", new { studentId = _studentId });
        }
    }
}