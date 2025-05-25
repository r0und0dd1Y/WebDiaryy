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
    public class ScheduleController : Controller
    {
        private readonly ISchedule _schedules;

        public ScheduleController(ISchedule schedules)
        {
            _schedules = schedules;
        }

        [Authorize]
        [Route("Schedule/GetSchedule")]
        public ViewResult GetSchedule(string scheduleId)
        {
            Schedule schedule = null;
            if (string.IsNullOrEmpty(scheduleId))
            {

            }
            else
            {
                schedule = _schedules.GetSchedules.FirstOrDefault(x => x.Id.ToLower() == scheduleId.ToLower());

            }
            var scheduleObj = new ScheduleViewModel
            {
                GetSchedule = schedule,
                //Lessons = schedule.Lessons,
                SchoolClass = schedule.SchoolClass

            };
            return View(scheduleObj);
        }

        [Authorize]
        [Route("Schedule/GetScheduleSchoolClass")]
        public ViewResult GetScheduleSchoolClass(string schoolClassId)
        {
            SchoolClass schoolClass = null;
            Schedule schedule = null;
            if (!string.IsNullOrEmpty(schoolClassId))
            {
                foreach (var s in _schedules.GetSchedules)
                {
                    if (s.SchoolClass.Id.ToLower() == schoolClassId.ToLower())
                    {
                        schedule = s;
                        schoolClass = s.SchoolClass;
                        break;
                    }
                }
                schedule = new Schedule { Id = schedule.Id, Lessons = schedule.Lessons.OrderBy(x => x.Time).ToList().OrderBy(x => x.WeekDay).ToList(), SchoolClass = schedule.SchoolClass, SchoolClassId = schedule.SchoolClass.Id };
            }

            var scheduleViewModel = new ScheduleViewModel { GetSchedule = schedule, SchoolClass = schoolClass };
            return View(scheduleViewModel);
        }

        [Authorize]
        [Route("Schedule/GetScheduleSchoolWorker")]
        public ViewResult GetScheduleSchoolWorker(string schoolWorkerId)
        {
            SchoolWorker schoolWorker = null;
            List<Lesson> lessons = new List<Lesson>();
            if (!string.IsNullOrEmpty(schoolWorkerId))
            {
                foreach (var s in _schedules.GetSchedules)
                {
                    foreach (var l in s.Lessons)
                    {
                        if (l.Subject.Teacher.Id == schoolWorkerId)
                        {
                            lessons.Add(l);
                            schoolWorker = l.Subject.Teacher;
                        }
                    }
                }
                lessons = lessons.OrderBy(x => x.Time).ToList().OrderBy(x => x.WeekDay).ToList();
            }
            else
            {

            }

            var scheduleViewModel = new ScheduleTeacherViewModel { Lessons = lessons, SchoolWorker = schoolWorker };
            return View(scheduleViewModel);
        }

        [Authorize]
        [Route("Schedule/SchedulesListSchool")]
        public ViewResult SchedulesListSchool(string schoolId)
        {
            School school = null;
            List<Schedule> schedules = new List<Schedule>();
            if (!string.IsNullOrEmpty(schoolId))
            {
                foreach (var s in _schedules.GetSchedules)
                {
                    if (s.SchoolClass.School.Id.ToLower() == schoolId.ToLower())
                    {
                        schedules.Add(new Schedule { Id = s.Id, Lessons = s.Lessons.OrderBy(x => x.Time).ToList().OrderBy(x => x.WeekDay).ToList(), SchoolClass = s.SchoolClass, SchoolClassId = s.SchoolClassId });
                        school = s.SchoolClass.School;
                    }

                }
            }
            else
            {

            }

            var schedulesObj = new ListScheduleViewModel { Schedules = schedules, School = school };
            return View(schedulesObj);
        }

        [Authorize]
        [Route("Schedule/SchedulesListStudent")]
        public ViewResult SchedulesListStudent(string studentId)
        {
            Student student = null;
            List<ScheduleStudentViewModel> scheduleStudentViewModels = new List<ScheduleStudentViewModel>();
            if (!string.IsNullOrEmpty(studentId))
            {
                foreach (var s in _schedules.GetSchedules)
                {
                    ScheduleStudentViewModel scheduleStudentViewModel = null;
                    List<Lesson> lessons = new List<Lesson>();
                    foreach (var l in s.Lessons)
                    {
                       foreach(var st in l.Subject.StudentSubjects)
                        {
                            if (st.Student.Id.ToLower() == studentId.ToLower())
                            {
                                lessons.Add(l);
                                student = st.Student;
                                
                            }
                        }
                    }
                    if (lessons.Count != 0)
                    {
                        scheduleStudentViewModel = new ScheduleStudentViewModel { Lessons = lessons.OrderBy(x => x.Time).OrderBy(x => x.WeekDay).ToList(), SchoolClass = s.SchoolClass };
                        scheduleStudentViewModels.Add(scheduleStudentViewModel);
                    }

                }
                scheduleStudentViewModels.OrderBy(x => x.SchoolClass.Name);
            }
            else
            {

            }

            var schedulesObj = new ListScheduleStudentViewModel { GetSchedules = scheduleStudentViewModels, Student = student };
            return View(schedulesObj);
        }

        public ActionResult RedirectSchoolWorkerSchedule()
        {
            var info = HttpContext.Session.GetString("UserInfo");
            string _schoolworkerid = null;
            if (info != null)
            {
                var result = JsonConvert.DeserializeObject<UserInfo>(info);
                var id = result.UserId;
                _schoolworkerid = id;
            }
            else
            {
                return RedirectToAction("Logout", "Account");
            }
            return RedirectToAction("GetScheduleSchoolWorker", new { schoolWorkerId = _schoolworkerid });
        }

        public ActionResult RedirectStudentSchedule()
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
            return RedirectToAction("SchedulesListStudent", new { studentId = _studentid });
        }

    }
}