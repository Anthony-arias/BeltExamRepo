using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeltExam.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;


namespace BeltExam.Controllers
{
    public class DojoActivitiesController : Controller
    {
        private MyContext _context;

        public DojoActivitiesController(MyContext context)
        {
            _context = context;
        }

        [HttpPost("dojoActivity/create")]
        public IActionResult CreateDojoActivity(DojoActivity dojoActivity)
        {
            if (!ModelState.IsValid)
            {
                TempData["LogedUser"] = (int)HttpContext.Session.GetInt32("CurrentUser");
                return View("DojoActivityForm");
            }
            else {
                Console.WriteLine("Adding to Db");
                _context.Add(dojoActivity);
                _context.SaveChanges();
                return RedirectToAction("ViewDojoActivityInfo", "Home", new { id = dojoActivity.DojoActivityId });
            }  
        }

        [HttpGet("dojoActivity/addAttendee")]
        public IActionResult AddAttendeeToDojoActivity(int DojoActivityId, int UserId)
        {
            _context.Add(new Association { DojoActivityId = DojoActivityId, UserId = UserId });
            _context.SaveChanges();
            return RedirectToAction("ViewDashboard", "Home"); // change this
        }

        [HttpGet("dojoActivity/removeAttendee")]
        public IActionResult RemoveAttendeeFromDojoActivity(int DojoActivityId, int UserId)
        {
            Association thisAssociation = _context.Associations
                .FirstOrDefault(This => This.DojoActivityId == DojoActivityId && This.UserId == UserId);

            _context.Remove(thisAssociation);
            _context.SaveChanges();
            return RedirectToAction("ViewDashboard", "Home");
        }

        [HttpGet("dojoActivity/delete")]
        public IActionResult RemoveDojoActivity(int DojoActivityId)
        {
            DojoActivity ThisDojoActivity = _context.DojoActivities
                .FirstOrDefault(dojoActivity => dojoActivity.DojoActivityId == DojoActivityId);

            _context.Remove(ThisDojoActivity);
            _context.SaveChanges();
            return RedirectToAction("ViewDashboard", "Home");
        }

    }
}
