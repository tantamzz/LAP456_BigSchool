using BigSchool.Models;
using BigSchool.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigSchool.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _dbContext;

        public HomeController()
        {
            _dbContext = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var upcommingCourses = _dbContext.Courses
                .Include(c => c.Lecturer)
                .Include(c => c.Category)
                .Where(c => c.DateTime > DateTime.Now && !c.IsCanceld);

            var viewModel = new CoursesViewModel
            {
                UpcommingCourses = upcommingCourses,
                ShowAction = User.Identity.IsAuthenticated
            };
            var userId = User.Identity.GetUserId();
            var courses = _dbContext.Attdendances

                            .Where(a => a.AttendeeId == userId)
                            .Select(p => p.CourseId)
                            .ToList();
            var couresese = _dbContext.Courses.Where(p => courses.Contains(p.Id)).Include(p => p.Lecturer).Include(p => p.Category).ToList();
            var following = _dbContext.Followings
                          .Where(a => a.FollowerId == userId)
                          .Select(p => p.Followee).ToList();
            ViewBag.following = following;
            ViewBag.couresese = couresese;
            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}