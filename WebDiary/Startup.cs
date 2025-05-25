using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebDiary.Data.EFContext;
using WebDiary.Data.Interfaces;
using WebDiary.Data.Models;
using WebDiary.Data.Repository;

namespace WebDiary
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });
            
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            
            services.AddDbContext<EFDbContext>(options =>
            options.UseSqlServer(
              Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<DbUser, DbRole>(options => options.Stores.MaxLengthForKeys = 128)
                .AddEntityFrameworkStores<EFDbContext>()
                .AddDefaultTokenProviders();


            
            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
            });
            services.AddTransient<ISchool, SchoolRepository>();
            services.AddTransient<IJournal, JournalRepository>();
            services.AddTransient<IParent, ParentRepository>();
            services.AddTransient<ISchedule, ScheduleRepository>();
            services.AddTransient<ISchoolWorker, SchoolWorkerRepository>();
            services.AddTransient<ISchoolClass, SchoolClassRepository>();
            services.AddTransient<ISubject, SubjectRepository>();
            services.AddTransient<IStudent, StudentRepository>();

            services.AddMemoryCache();
            services.AddSession();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            //app.UseCookiePolicy();
            SeederDB.SeedData(app.ApplicationServices, env, this.Configuration);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "studentpersonalaccount",
                    template: "Account/{action}/{id?}",
                    defaults: new { Controller = "Account", action = "StudentPersonalAccount" });
                routes.MapRoute(
                    name: "parentpersonalaccount",
                    template: "Account/{action}/{id?}",
                    defaults: new { Controller = "Account", action = "ParentPersonalAccount" });
                routes.MapRoute(
                    name: "schoolworkerpersonalaccount",
                    template: "Account/{action}/{id?}",
                    defaults: new { Controller = "Account", action = "SchoolWorkerPersonalAccount" });
                routes.MapRoute(
                    name: "schoolbyid",
                    template: "School/{action}/{id?}",
                    defaults: new { Controller = "School", action = "GetSchoolById" });
                routes.MapRoute(
                    name: "studentfilter",
                    template: "Student/{action}/{schoolId?}/{classId?}/{subjectId?}/{searchString?}",
                    defaults: new { Controller = "Student", action = "GetStudents" });
                
                routes.MapRoute(
                    name: "studentpublicprofile",
                    template: "Student/{action}/{id?}",
                    defaults: new { Controller = "Student", action = "StudentPublicAccount" });
                routes.MapRoute(
                    name: "schoolworkerpublicprofile",
                    template: "SchoolWorker/{action}/{id?}",
                    defaults: new { Controller = "SchoolWorker", action = "SchoolWorkerPublicAccount" });
                routes.MapRoute(
                    name: "schoolclassfilter",
                    template: "SchoolClass/{action}/{schoolId?}/{studentId?}/{searchString?}",
                    defaults: new { Controller = "SchoolClass", action = "SchoolClassesList" });
                routes.MapRoute(
                    name: "schoolclassbyid",
                    template: "SchoolClass/{action}/{id?}",
                    defaults: new { Controller = "SchoolClass", action = "SchoolClass" });
                routes.MapRoute(
                    name: "schoolworkersfilter",
                    template: "SchoolWorker/{action}/{schoolId?}/{searchString?}",
                    defaults: new { Controller = "SchoolWorker", action = "SchoolWorkersList" });
                routes.MapRoute(
                    name: "scheduleschoolclass",
                    template: "SchoolClass/{action}/{schoolClassId?}",
                    defaults: new { Controller = "Schedule", action = "GetScheduleSchoolClass" });
                routes.MapRoute(
                    name: "subjectfilter",
                    template: "Subject/{action}/{schoolClassId?}/{schoolWorkerId?}",
                    defaults: new { Controller = "Subject", action = "GetSubjectsList" });
                routes.MapRoute(
                   name: "journalteacher",
                   template: "Journal/{action}/{schoolWorkerId?}",
                   defaults: new { Controller = "Journal", action = "GetJournalTeacher" });
                routes.MapRoute(
                   name: "journalstudent",
                   template: "Journal/{action}/{studentId?}",
                   defaults: new { Controller = "Journal", action = "GetJournalsStudent" });
                routes.MapRoute(
                   name: "personalaccountredirect",
                   template: "Account/{action}/",
                   defaults: new { Controller = "Account", action = "PersonalAccount" });
            });
        }
    }
}
