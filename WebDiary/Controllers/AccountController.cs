using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebDiary.Data.EFContext;
using WebDiary.Data.Models;
using WebDiary.Data.ViewModels;
using WebDiary.Models;

namespace WebDiary.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;
        private readonly RoleManager<DbRole> _roleManager;
        private readonly EFDbContext _context;
        //private readonly IEmailSender _myEmailSender;


        public AccountController(UserManager<DbUser> userManager, SignInManager<DbUser> signInManager,
            RoleManager<DbRole> roleManager, EFDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            //_myEmailSender = myEmailSender;
        }

        [Route("Account/PersonalAccount")]
        public ActionResult PersonalAccount()
        {
            //var info = HttpContext.Session.GetString("UserInfo");
            //if (info != null)
            //{
            //    var result = JsonConvert.DeserializeObject<UserInfo>(info);
            //    var id = result.UserId;
            //    var userObj = new PersonalAccountViewModel()
            //    {
            //        GetUserInfo = result
            //    };
            //    return View(userObj);
            //}
            //else
            //{
            //    return View();
            //}
            if (User.IsInRole("Student"))
            {
                return RedirectToAction("StudentPersonalAccount");
            }
            else if (User.IsInRole("SchoolWorker") || User.IsInRole("Director") || User.IsInRole("Manager") || User.IsInRole("Teacher"))
            {
                return RedirectToAction("SchoolWorkerPersonalAccount");
            }
            else if (User.IsInRole("Parent"))
            {
                return RedirectToAction("ParentPersonalAccount");
            }
            return RedirectToAction("Logout", "Account");
            

        }

        [Authorize(Roles = "Student")]
        [Route("Account/StudentPersonalAccount")]
        public ActionResult StudentPersonalAccount()
        {
            var info = HttpContext.Session.GetString("UserInfo");
            if (info != null)
            {
                var result = JsonConvert.DeserializeObject<UserInfo>(info);
                var id = result.UserId;
                var students = _context.Students.Include(x => x.Person).ThenInclude(x => x.UserProfile).ThenInclude(x=>x.User)
                    .Include(x=>x.SchoolStudents).ThenInclude(x=>x.School)
                    .Include(x=>x.SchoolStudents).ThenInclude(x=>x.School)
                    .Include(x=>x.Parent).ThenInclude(x=>x.UserProfile).ThenInclude(x => x.User)
                    .Include(x=>x.Siblings).ThenInclude(x=>x.Person).ThenInclude(x=>x.UserProfile).ThenInclude(x=>x.User)
                    .Include(x=>x.SchoolClassStudents).ThenInclude(x=>x.SchoolClass).ThenInclude(x=>x.School);
                var student = students.FirstOrDefault(x => x.Id.ToLower() == id.ToLower());
                var schools = new List<School>();
                foreach(SchoolStudent scs in student.SchoolStudents)
                {
                    if (!schools.Contains(scs.School))
                    {
                        schools.Add(scs.School);
                    }
                }
                

                var userObj = new StudentPersonalAccountViewModel()
                {
                    GetUserInfo = result,
                    Student=student,
                    Schools=schools
                };
                return View(userObj);
            }
            else
            {
                return RedirectToAction("Logout", "Account");
            }

        }

        [Authorize(Roles = "SchoolWorker")]
        [Route("Account/SchoolWorkerPersonalAccount")]
        public ActionResult SchoolWorkerPersonalAccount()
        {
            var info = HttpContext.Session.GetString("UserInfo");
            if (info != null)
            {
                var result = JsonConvert.DeserializeObject<UserInfo>(info);
                var id = result.UserId;
                var schoolWorkers = _context.SchoolWorkers
                    .Include(x=>x.School)
                    .Include(x => x.Person).ThenInclude(x => x.UserProfile).ThenInclude(x => x.User)
                    .Include(x => x.Subjects).ThenInclude(x=>x.SchoolClass)
                    .Include(x => x.Class).ThenInclude(x => x.School);
                
                var schoolWorker = schoolWorkers.FirstOrDefault(x => x.Id.ToLower() == id.ToLower());
                var schools = _context.Schools.Include(x => x.SchoolWorkers);
                var school = new School();
                foreach(School s in schools)
                {
                    if (s.SchoolWorkers.Contains(schoolWorker))
                    {
                        school = s;
                        
                    }
                }
                


                var userObj = new SchoolWorkerPersonalAccountViewModel()
                {
                    GetUserInfo = result,
                    SchoolWorker=schoolWorker, 
                    GetSchool=school
                };
                return View(userObj);
            }
            else
            {
                return RedirectToAction("Logout", "Account");
            }

        }

        [Authorize(Roles = "Parent")]
        [Route("Account/ParentPersonalAccount")]
        public ActionResult ParentPersonalAccount()
        {
            var info = HttpContext.Session.GetString("UserInfo");
            if (info != null)
            {
                var result = JsonConvert.DeserializeObject<UserInfo>(info);
                var id = result.UserId;
                var parents = _context.Parents.Include(x => x.UserProfile).ThenInclude(x=>x.User)
                    .Include(x => x.Kids).ThenInclude(x => x.Person).ThenInclude(x => x.UserProfile);
                var parent = parents.FirstOrDefault(x => x.Id.ToLower() == id.ToLower());
                var kids = parent.Kids.ToList();
                
                var userObj = new ParentPersonalAccountViewModel()
                {
                    GetUserInfo = result,
                    Parent=parent, 
                    Kids=kids
                };
                return View(userObj);
            }
            else
            {
                return RedirectToAction("Logout", "Account");
            }

        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpGet]
        [Route("Account/ChangePassword/{id}")]
        public IActionResult ChangePassword(string id)
        {
            return View();
        }

        //[HttpPost]
        //[Route("Account/ChangePassword/{id}")]
        //public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model, string id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        //        if (user == null)
        //        {
        //            ModelState.AddModelError("", "This User not register");
        //            return View(model);
        //        }
        //        var hash_password = _userManager.PasswordHasher.HashPassword(user, model.Password);
        //        user.PasswordHash = hash_password;
        //        var result = await _userManager.UpdateAsync(user);

        //    }
        //    return RedirectToAction("Login", "Account");
        //}

        //[HttpPost]
        //public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }
        //    var user = _context.Users
        //        .Include(x => x.UserProfile)
        //        .FirstOrDefault(u => u.Email == model.Email);
        //    if (user == null)
        //    {
        //        ModelState.AddModelError("", "Incorrect email");
        //        return View(model);
        //    }
        //    var userName = user.UserProfile.FirstName;

        //    EmailService emailService = new EmailService();
        //    string url = "http://localhost:50560/Account/ChangePassword/" + user.Id;

        //    await emailService.SendEmailAsync(model.Email, "ForgotPassword",
        //        $" Dummie {userName}," +
        //        $" <br/>" +
        //        $" To change your password" +
        //        $" <br/>" +
        //        $" you should visit this link <a href='{url}'>press</a>");

        //    //string url = "http://localhost:51286/Account/ChangePassword/" + user.Id;

        //    //await _myEmailSender.SendEmailAsync(model.Email, "ForgotPassword",
        //    //    $" Dummie {userName}," +
        //    //        $" <br/>" +
        //    //        $" To change your password" +
        //    //        $" <br/>" +
        //    //        $" you should visit this link <a href='{url}'>press</a>"



        //    //    );

        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }
        //    var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);
        //    if (user == null)
        //    {
        //        ModelState.AddModelError("", "Incorrect login");
        //        return View(model);
        //    }

        //    var result = _signInManager.PasswordSignInAsync(user, model.Password, false, false).Result;
        //    if (!result.Succeeded)
        //    {
        //        ModelState.AddModelError("", "Incorrect password");
        //        return View(model);
        //    }
        //    await _signInManager.SignInAsync(user, isPersistent: false);
        //    await Authenticate(model.Email);

        //    //var userInfo = new UserInfo()
        //    //{
        //    //    UserId = user.Id,
        //    //    Email = user.Email
        //    //};
        //    //HttpContext.Session.SetString("UserInfo", JsonConvert.SerializeObject(userInfo));

        //    return RedirectToAction("PersonalAccount", "Account");
        //}

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public IActionResult AccessDenied()
        {
            return RedirectToAction("Login", "Account");
        }


        
        [HttpPost, HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account", new { reason="Your session has expired. Please log in." });
        }

        [HttpGet]
        public IActionResult Login(string reason)
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Incorrect input data.");
                return View(model);
            }
            
            var result = _signInManager.PasswordSignInAsync(user, model.Password, false, false).Result;
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Incorrect password");
                return View(model);
            }
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, User);
            //await _signInManager.SignInAsync(user, isPersistent: false);
            await Authenticate(model.Email);
            
            var userInfo = new UserInfo()
            {
                UserId = user.Id,
                Email = user.Email
            };
            HttpContext.Session.SetString("UserInfo", JsonConvert.SerializeObject(userInfo));

            return RedirectToAction("PersonalAccount", "Account");

        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterViewModel model)
        //{
        //    var roleName = "User";

        //    UserProfile userProfile = new UserProfile()
        //    {
        //        FirstName = model.FirstName,
        //        MiddleName = model.MiddleName,
        //        LastName = model.LastName,
        //        RegistrationDate = DateTime.Now

        //    };

        //    DbUser dbUser = new DbUser()
        //    {
        //        Email = model.Email,
        //        UserName = model.Email,
        //        UserProfile = userProfile,
        //        SecurityStamp = Guid.NewGuid().ToString()
        //    };

        //    var result = await _userManager.CreateAsync(dbUser, model.Password);
        //    result = _userManager.AddToRoleAsync(dbUser, roleName).Result;


        //    if (result.Succeeded)
        //    {
        //        // установка куки
        //        await _signInManager.SignInAsync(dbUser, false);
        //        return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, error.Description);
        //        }
        //    }

        //    return View(model);
        //}
    }
}