using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackEnd.BussinessLogic;
using BackEnd.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dimension_Data_Collab.Controllers
{
    public class LoginController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(string email,string PasswordHash)
        {
            //check if user exists and entered the right stuff
            var exist = await LoginLogic.TryLogin(email, PasswordHash);
            //then call the losgin function...
            if (exist.Item1)//user exists
            {
                //login user here
                var prin = await LoginLogic.GetPrinciple(exist.Item2);
                await HttpContext.SignInAsync(prin);
                return Redirect("home/index");
            }
            else
            {
                ViewData["Error"] = "User does not exist or incorrect information was given.";
                return View();
            }
            
        }

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(PersonModel model)
        {
            if (ModelState.IsValid)
            {
                LoginLogic.RegisterUser(model.Name,model.Email,model.PasswordHash);
                return RedirectToAction("Login");
            }
            else
            {
                ViewData["Error"] = "You added null values. Please make sure you add valid values.";
                return RedirectToAction("Register");
            }
            //give the data to the register user backend
            
        }

        public async Task<IActionResult> ListAllUsers()
        {
            var data = await LoginLogic.GetAllUsers();
            return View(data);
        }
    }
}
