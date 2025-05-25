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
    public class JournalController : Controller
    {
        private readonly IJournal _journals;
        public JournalController(IJournal journals)
        {
            _journals = journals;
        }

        [Authorize]
        [Route("Journal/GetJournalsStudent")]
        public ViewResult GetJournalsStudent(string studentId)
        {
            Student student = null;
            List<JournalViewModel> allJournalsFiltered = new List<JournalViewModel>();
            foreach (var j in _journals.GetJournals)
            {
                JournalViewModel journalViewModel = null;
                foreach (var st in j.Subject.StudentSubjects)
                {
                    if (st.Student.Id == studentId)
                    {
                        student = st.Student;
                        journalViewModel = new JournalViewModel();
                        journalViewModel.GetJournal = j;
                        journalViewModel.MarksFilter = new List<Mark>();
                        foreach (var mark in j.Marks)
                        {
                            if (mark.Student.Id == studentId)
                            {
                                journalViewModel.MarksFilter.Add(mark);

                            }
                        }
                    }
                }
                if (journalViewModel != null)
                {
                    journalViewModel.MarksFilter = journalViewModel.MarksFilter.OrderBy(x => x.TimeSet).ToList();
                    allJournalsFiltered.Add(journalViewModel);
                }
            }

            List<SchoolClass> schoolClasses = new List<SchoolClass>();
            foreach (var j in allJournalsFiltered)
            {
                if (!schoolClasses.Contains(j.GetJournal.Subject.SchoolClass))
                {
                    schoolClasses.Add(j.GetJournal.Subject.SchoolClass);
                }
            }
            List<ListJournalViewModel> filteredByClass = new List<ListJournalViewModel>();
            foreach (var s in schoolClasses)
            {
                ListJournalViewModel listJournalViewModel = new ListJournalViewModel();
                listJournalViewModel.SchoolClass = s;
                listJournalViewModel.JournalViewModels = new List<JournalViewModel>();
                foreach (var j in allJournalsFiltered)
                {
                    if (j.GetJournal.Subject.SchoolClass.Id == s.Id)
                    {
                        listJournalViewModel.JournalViewModels.Add(j);
                    }
                }
                filteredByClass.Add(listJournalViewModel);
            }

            var journalObj = new ListJournalsStudentViewModel { ListJournalsViewModels = filteredByClass, Student = student };
            return View(journalObj);
        }

        [Authorize]
        [Route("Journal/GetJournalTeacher")]
        public ViewResult GetJournalTeacher(string teacherId)
        {
            SchoolWorker teacher = null;
            List<JournalViewModel> allJournalsFiltered = new List<JournalViewModel>();
            foreach (var j in _journals.GetJournals)
            {
                JournalViewModel journalViewModel = null;
                if (j.Subject.Teacher.Id == teacherId)
                {
                    teacher = j.Subject.Teacher;
                    journalViewModel = new JournalViewModel();
                    journalViewModel.GetJournal = j;
                    journalViewModel.MarksFilter = j.Marks.OrderBy(x => x.TimeSet).ToList();
                }

                if (journalViewModel != null)
                {
                    allJournalsFiltered.Add(journalViewModel);
                }
            }
            var journalObj = new ListJournalViewModel { JournalViewModels = allJournalsFiltered, SchoolWorker = teacher };
            return View(journalObj);
        }

        [Authorize]
        [Route("Journal/RedirectStudentJournal")]
        public ActionResult RedirectStudentJournal()
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
            return RedirectToAction("GetJournalsStudent", new { studentId = _studentid });
        }

        [Authorize]
        [Route("Journal/RedirectTeacherJournal")]
        public ActionResult RedirectTeacherJournal()
        {
            var info = HttpContext.Session.GetString("UserInfo");
            string _teacherId = null;
            if (info != null)
            {
                var result = JsonConvert.DeserializeObject<UserInfo>(info);
                var id = result.UserId;
                _teacherId = id;
            }
            else
            {
                return RedirectToAction("Logout", "Account");
            }
            return RedirectToAction("GetJournalTeacher", new { teacherId = _teacherId });
        }

        //[Route("Journal/GetJournalsTeacher")]
        //public ViewResult GetJournalsTeacher(SchoolWorker teacherId)
        //{
        //    //var info = HttpContext.Session.GetString("UserInfo");
        //    //if (info != null)
        //    //{
        //    //    var result = JsonConvert.DeserializeObject<UserInfo>(info);
        //    //    var id = result.UserId;
        //    //}

        //    IEnumerable<Journal> journals = null;
        //    List<JournalViewModel> journalViewModels = null;
        //    if (string.IsNullOrEmpty(teacherId.Id))
        //    {

        //    }
        //    else
        //    {
        //        journals = _journals.GetJournals.Where(x => x.Subject.Teacher==teacherId);
        //        foreach (Journal j in journals)
        //        {
        //            journalViewModels.Add(new JournalViewModel { GetJournal = j, Marks=j.Marks, Subject = j.Subject });
        //        }
        //        //if (string.Equals("Benzin", category, StringComparison.OrdinalIgnoreCase))
        //        //{
        //        //    cars = _cars.GetCars.Where(x => x.Category.CategoryName.Equals("Benzin"))
        //        //        .OrderBy(i => i.Id);
        //        //}
        //        //else if (string.Equals("Diezel", category, StringComparison.OrdinalIgnoreCase))
        //        //{
        //        //    cars = _cars.GetCars.Where(x => x.Category.CategoryName.Equals("Diezel"))
        //        //        .OrderBy(i => i.Id);
        //        //}
        //        //else
        //        //{
        //        //    cars = _cars.GetCars.Where(x => x.Category.CategoryName.Equals("Electro"))
        //        //        .OrderBy(i => i.Id);
        //        //}
        //    }
        //    var journalsObj = new ListJournalsViewModel { GetJournals = journalViewModels };
        //    return View(journalsObj);




        //}

        //[Route("Journal/GetJournal")]
        //public ViewResult GetJournal(string journalId)
        //{
        //    //var info = HttpContext.Session.GetString("UserInfo");
        //    //if (info != null)
        //    //{
        //    //    var result = JsonConvert.DeserializeObject<UserInfo>(info);
        //    //    var id = result.UserId;
        //    //}

        //    Journal journal = null;
        //    if (string.IsNullOrEmpty(journalId))
        //    {

        //    }
        //    else
        //    {
        //        journal = _journals.GetJournals.FirstOrDefault(x => x.Id.ToLower() == journalId.ToLower());

        //    }
        //    var journalsObj = new JournalViewModel { GetJournal = journal, Marks=journal.Marks, Subject = journal.Subject };
        //    return View(journalsObj);
        //}


    }
}