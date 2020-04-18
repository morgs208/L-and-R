using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LoginRegistration.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;


namespace LoginRegistration.Controllers
{
    public class LoginController : Controller
    {
        private LoginRegContext db;

        public LoginController(LoginRegContext context)
        {
            db = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Register")]
        public IActionResult Register(User newUser)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Any(u => u.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email already is use");
                }
            }
            if (ModelState.IsValid == false)
            {
                return View("Index");
            }

            // hash pw
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            newUser.Password = hasher.HashPassword(newUser, newUser.Password);

            newUser.CreatedAt = DateTime.Now;
            newUser.UpdatedAt = DateTime.Now;
            db.Users.Add(newUser);
            db.SaveChanges();

            HttpContext.Session.SetInt32("UserId", newUser.UserId);
            return RedirectToAction("Success");

        }

        [HttpPost("Login")]
        public IActionResult Login(LoginUser loginUser)
        {
            // string genericErrMsg = "Invalid Email or Password";

            if (ModelState.IsValid == false)
            {
                return View("Index");
            }

            User dbUser = db.Users.FirstOrDefault(user => user.Email == loginUser.LoginEmail);

            if (dbUser == null)
            {
                ModelState.AddModelError("LoginEmail", "Invalid Email or Password");
            }
            else
            {
                PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
                PasswordVerificationResult result = hasher.VerifyHashedPassword(loginUser, dbUser.Password, loginUser.LoginPassword);

                if (result == 0)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid Email or Password");
                }
            }
            if (ModelState.IsValid == false)
            {
                return View("Index");
            }

            HttpContext.Session.SetInt32("UserId", dbUser.UserId);
            return RedirectToAction("Success");
        }

        
        
        [HttpGet("Success")]
        public IActionResult Success()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
             {
                 return RedirectToAction("Index");
             }
            return View();
        }
            
        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
   
    }
}