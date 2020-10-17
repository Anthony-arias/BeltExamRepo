using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BeltExam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BeltExam.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;
        public HomeController(MyContext context)
        {
            _context = context;
        }

        
       /* [HttpGet("")]
        public IActionResult Index()
        {
            return View("Index");
        }*/

        [HttpGet("")] // Login Reg Form
        public IActionResult Index()
        {
            return RedirectToAction("ViewUserForm");
        }

        [HttpGet("signin")] // Login Reg Form
        public IActionResult ViewUserForm()
        {
            return View("Index");
        }

        [HttpGet("home")]
        public ViewResult ViewDashboard()
        {
            List<DojoActivity> AllDojoActivities = _context.DojoActivities
                .Include(act => act.Creator)
                .Include(act => act.Attendees)
                .ThenInclude(sub => sub.User)
                .ToList();

            TempData["LogedUser"] = (int)HttpContext.Session.GetInt32("CurrentUser");
            User ThisUser = _context.Users
                .FirstOrDefault(user => user.UserId == (int)TempData["LogedUser"]);
            //Console.WriteLine((int)HttpContext.Session.GetInt32("CurrentUser"));
            ViewBag.UserName = ThisUser.FirstName;
            return View("Dashboard", AllDojoActivities);
        }

        [HttpGet("activity/{id}")]
        public ViewResult ViewDojoActivityInfo(int id)
        {
            DojoActivity ThisDojoActivity = _context.DojoActivities
                .Include(act => act.Creator)
                .Include(act => act.Attendees)
                .ThenInclude(sub => sub.User)
                .FirstOrDefault(act => act.DojoActivityId == id);
            TempData["LogedUser"] = (int)HttpContext.Session.GetInt32("CurrentUser");
            return View("DojoActivityInfo", ThisDojoActivity);
        }

        [HttpGet("new")]
        public ViewResult ViewDojoActivityForm()
        {
            TempData["LogedUser"] = (int)HttpContext.Session.GetInt32("CurrentUser");
            return View("DojoActivityForm");
        }
    }
}
