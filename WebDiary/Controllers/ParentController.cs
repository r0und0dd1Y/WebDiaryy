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
    public class ParentController : Controller
    {
        private readonly IParent _parents;
        public ParentController(IParent parents)
        {
            _parents = parents;
        }

        [Authorize]
        [Route("Parent/ParentPublicAccount")]
        public ActionResult ParentPublicAccount(string parentId)
        {
            Parent parent = null;
            var kids = new List<Student>();
            if (string.IsNullOrEmpty(parentId))
            {

            }
            else
            {
                var info = HttpContext.Session.GetString("UserInfo");
                if (info != null)
                {
                    var result = JsonConvert.DeserializeObject<UserInfo>(info);
                    var id = result.UserId;
                    if (id.ToLower() == parentId.ToLower())
                    {
                        return RedirectToAction("ParentPersonalAccount", "Account");
                    }
                }
                else
                {
                    return RedirectToAction("Logout", "Account");
                }
                parent = _parents.GetParents.FirstOrDefault(x => x.Id.ToLower() == parentId.ToLower());
                


            }
            var parentObj = new ParentViewModel {  GetParent=parent, Kids=parent.Kids.ToList() };
            return View(parentObj);
        }

        [Route("Parent/GetParent")]
        public ViewResult GetParent(string parentId)
        {
            //var info = HttpContext.Session.GetString("UserInfo");
            //if (info != null)
            //{
            //    var result = JsonConvert.DeserializeObject<UserInfo>(info);
            //    var id = result.UserId;
            //}

            Parent parent = null;
            if (string.IsNullOrEmpty(parentId))
            {

            }
            else
            {
                parent = _parents.GetParents.FirstOrDefault(x => x.Id.ToLower() == parentId.ToLower());

            }
            var parentObj = new ParentViewModel { GetParent = parent, Kids = parent.Kids.ToList() };
            return View(parentObj);
        }

        [Route("Parent/GetParentsKid")]
        public ViewResult GetParentsKid(Student studentId)
        {
            //var info = HttpContext.Session.GetString("UserInfo");
            //if (info != null)
            //{
            //    var result = JsonConvert.DeserializeObject<UserInfo>(info);
            //    var id = result.UserId;
            //}
            
            IEnumerable<Parent> parents = null;
            if (string.IsNullOrEmpty(studentId.Id))
            {

            }
            else
            {
                parents = _parents.GetParents.Where(x => x.Kids.Contains(studentId));

            }
            var parentObj = new ListParentViewModel { GetParents = parents, School = null, Kid=studentId };
            return View(parentObj);
        }

        [Route("Parent/GetParentsSchool")]
        public ViewResult GetParentsSchool(School schoolId)
        {
            //var info = HttpContext.Session.GetString("UserInfo");
            //if (info != null)
            //{
            //    var result = JsonConvert.DeserializeObject<UserInfo>(info);
            //    var id = result.UserId;
            //}

            List<Parent> parents = null;
            if (schoolId==null)
            {

            }
            else
            {
                foreach(Parent p in _parents.GetParents)
                {
                    foreach(Student s in p.Kids)
                    {
                        foreach(SchoolClassStudent s1 in s.SchoolClassStudents)
                        {
                            if (s1.SchoolClass.School.Id.ToLower() == schoolId.Id.ToLower())
                            {
                                parents.Add(p);
                            }
                        }
                    }
                }

            }
            var parentObj = new ListParentViewModel { GetParents = parents, School = schoolId, Kid = null };
            return View(parentObj);
        }
    }
}