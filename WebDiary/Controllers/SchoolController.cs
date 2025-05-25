using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using WebDiary.Data.Interfaces;
using WebDiary.Data.Models;
using WebDiary.Data.ViewModels;
using WebDiary.Models;

namespace WebDiary.Controllers
{
    public class SchoolController : Controller
    {
        private readonly ISchool _schools;
        public SchoolController(ISchool schools)
        {
            _schools = schools;
        }

        [Authorize]
        [Route("School/GetSchoolById")]
        public ViewResult GetSchoolById(string schoolId)
        {
            //var info = HttpContext.Session.GetString("UserInfo");
            //if (info != null)
            //{
            //    var result = JsonConvert.DeserializeObject<UserInfo>(info);
            //    var id = result.UserId;
            //}

            School school = null;
            if (schoolId==null)
            {

            }
            else
            {
                school = _schools.GetSchools.FirstOrDefault(x => x.Id.ToLower() == schoolId.ToLower());

            }
            
            var schoolObj = new SchoolViewModel { GetSchool = school, Director = school.SchoolWorkers.FirstOrDefault(x=>x.IsDirector) };
            return View(schoolObj);
        }

        [Authorize]
        [Route("School/GetSchoolSchoolWorker")]
        public ActionResult GetSchoolSchoolWorker()
        {
            var info = HttpContext.Session.GetString("UserInfo");
            if (info != null)
            {
                var result = JsonConvert.DeserializeObject<UserInfo>(info);
                var id = result.UserId;
                var schools = _schools.GetSchools;
                var school = new School();
                var director = new SchoolWorker();
                foreach (School s in _schools.GetSchools)
                {
                    foreach(SchoolWorker sc in s.SchoolWorkers)
                    {
                        if (sc.Person.UserProfile.User.Id.ToLower() == id.ToLower())
                        {
                            school = s;
                        }
                        if (sc.IsDirector)
                        {
                            director = sc;
                        }
                    }
                }


                var schoolObj = new SchoolViewModel
                {
                    GetSchool = school,
                    Director=director
                };
                return View(schoolObj);
            }
            else
            {
                return RedirectToAction("Logout", "Account");
            }
        }

        [Authorize]
        [Route("School/GetSchoolsStudent")]
        public ActionResult GetSchoolsStudent()
        {
            var info = HttpContext.Session.GetString("UserInfo");
            if (info != null)
            {
                var result = JsonConvert.DeserializeObject<UserInfo>(info);
                var id = result.UserId;
                var schools = new List<School>();
                foreach (School s in _schools.GetSchools)
                {
                    foreach (var student in s.SchoolStudents)
                    {
                        if (student.Student.Person.UserProfile.User.Id.ToLower() == id.ToLower())
                        {
                            schools.Add(s);
                        }
                    }
                }


                var schoolsObj = new ListSchoolViewModel
                {
                     GetSchools=schools
                };
                return View(schoolsObj);
            }
            else
            {
                return RedirectToAction("Logout", "Account");
            }
        }

        [Authorize]
        [Route("School/GetSchoolList")]
        public ViewResult GetSchoolList()
        {
            //var info = HttpContext.Session.GetString("UserInfo");
            //if (info != null)
            //{
            //    var result = JsonConvert.DeserializeObject<UserInfo>(info);
            //    var id = result.UserId;
            //}

            IEnumerable<School> schools = null;
            schools = _schools.GetSchools.OrderBy(x => x.Name);

            var schoolObj = new ListSchoolViewModel
            {
                GetSchools = schools
            };
            return View(schoolObj);
        }
    }
}
