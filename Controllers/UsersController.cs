using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeltExam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BeltExam.Controllers
{
    
    public class UsersController : Controller
    {
        private MyContext _context;

        public UsersController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/"); //Change Redirect
        }

        [HttpPost("user/create")]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("Index"); 
                }
                else
                {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    user.Password = Hasher.HashPassword(user, user.Password);
                    _context.Add(user);
                    _context.SaveChanges();
                    HttpContext.Session.SetInt32("CurrentUser", user.UserId);
                    return RedirectToAction("ViewDashboard", "Home");
                }
            }
            return View("Index"); //Change View
        }

        [HttpPost("user/login")]
        public IActionResult Login(LoginUser user)
        {
            Console.WriteLine("in login function");
            if (ModelState.IsValid)
            {
                var ThisUser = _context.Users.FirstOrDefault(u => u.Email == user.LoginEmail);
                if (ThisUser == null)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
                    return View("Index"); //Change View
                }

                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(user, ThisUser.Password, user.LoginPassword);
                if (result == 0)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
                    return View("Index"); //Change View
                }
                else
                {
                    HttpContext.Session.SetInt32("CurrentUser", ThisUser.UserId);
                    return RedirectToAction("ViewDashboard", "Home"); //Change Redirect
                }
            }
            return View("Index"); //Change View
        }
    }
}
