using System;
using System.Collections.Generic;
using System.Linq;
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
    public class SchoolWorkerController : Controller
    {
        private readonly ISchoolWorker _schoolWorkers;
        public SchoolWorkerController(ISchoolWorker schoolWorkers)
        {
            _schoolWorkers = schoolWorkers;
        }
        [Route("SchoolWorker/GetSchoolWorker")]
        public ViewResult GetSchoolWorker(string schoolWorkerId)
        {
            //var info = HttpContext.Session.GetString("UserInfo");
            //if (info != null)
            //{
            //    var result = JsonConvert.DeserializeObject<UserInfo>(info);
            //    var id = result.UserId;
            //}

            SchoolWorker schoolWorker = null;
            if (string.IsNullOrEmpty(schoolWorkerId))
            {

            }
            else
            {
                schoolWorker = _schoolWorkers.GetSchoolWorkers.FirstOrDefault(x => x.Id.ToLower() == schoolWorkerId.ToLower());

            }
            var schoolWorkerObj = new SchoolWorkerViewModel { GetSchoolWorker = schoolWorker, Class = schoolWorker.Class, Subjects = schoolWorker.Subjects };
            return View(schoolWorkerObj);
        }

        [Authorize]
        [Route("SchoolWorker/SchoolWorkersList")]
        public ActionResult SchoolWorkersList(string schoolId, string searchString)
        {
            List<SchoolWorker> allSchoolWorkers = _schoolWorkers.GetSchoolWorkers.ToList();
            var schoolWorkers = new List<SchoolWorker>();
            School school = null;
            var schoolWorkerObj = new ListSchoolWorkerViewModel();
            if (!string.IsNullOrEmpty(schoolId))
            {
                schoolWorkers = allSchoolWorkers.Where(x => x.School.Id.ToLower() == schoolId.ToLower()).ToList();
                school = schoolWorkers.FirstOrDefault().School;
            }
            else
            {
                if (!User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "Home");
                }
                schoolWorkers = allSchoolWorkers;
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                var _schoolWorkers = new List<SchoolWorker>();
                foreach (var s in schoolWorkers)
                {
                    string fullName = s.Person.UserProfile.FirstName + s.Person.UserProfile.LastName + s.Person.UserProfile.MiddleName;
                    if (fullName.ToLower().Contains(searchString.ToLower()))
                    {
                        _schoolWorkers.Add(s);
                    }
                }
                schoolWorkers = _schoolWorkers;
            }
            schoolWorkerObj = new ListSchoolWorkerViewModel { GetSchoolWorkers = schoolWorkers, School = school };
            return View(schoolWorkerObj);
        }

        [Authorize]
        [Route("SchoolWorker/SchoolWorkerPublicAccount")]
        public ActionResult SchoolWorkerPublicAccount(string schoolWorkerId)
        {
            SchoolWorker schoolWorker = null;
            SchoolWorkerViewModel schoolWorkerObj = new SchoolWorkerViewModel();
            if (string.IsNullOrEmpty(schoolWorkerId))
            {

            }
            else
            {
                schoolWorker = _schoolWorkers.GetSchoolWorkers.ToList().FirstOrDefault(x => x.Id.ToLower() == schoolWorkerId.ToLower());
                var info = HttpContext.Session.GetString("UserInfo");
                if (info != null)
                {
                    var result = JsonConvert.DeserializeObject<UserInfo>(info);
                    var id = result.UserId;
                    if (schoolWorker.Id == id)
                    {
                        return RedirectToAction("SchoolWorkerPersonalAccount", "Account");
                    }
                }
                else
                {
                    return RedirectToAction("Logout", "Account");
                }


                if (schoolWorker.IsClassTeacher)
                {
                    schoolWorkerObj = new SchoolWorkerViewModel { GetSchoolWorker = schoolWorker, Class = schoolWorker.Class, Subjects = schoolWorker.Subjects };
                }
                else
                {
                    schoolWorkerObj = new SchoolWorkerViewModel { GetSchoolWorker = schoolWorker, Class = null, Subjects = schoolWorker.Subjects };
                }
            }
            return View(schoolWorkerObj);
        }

        public ActionResult GetSchoolWorkersSchoolWorker()
        {
            var info = HttpContext.Session.GetString("UserInfo");
            string _schoolId = null;
            if (info != null)
            {
                var result = JsonConvert.DeserializeObject<UserInfo>(info);
                var id = result.UserId;
                foreach (var s in _schoolWorkers.GetSchoolWorkers.ToList())
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
            return RedirectToAction("SchoolWorkersList", new { schoolId = _schoolId });
        }
    }
}