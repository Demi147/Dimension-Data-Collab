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
        private readonly LoginLogic loginLogic;
        public LoginController(LoginLogic _loginLogic)
        {
            loginLogic = _loginLogic;
        }

        [Route("Login")]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }
         
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(string returnUrl,string email,string PasswordHash )
        {
            var result = await loginLogic.TryLogin(email,PasswordHash);

            if (result.Item1)
            {
                await HttpContext.SignInAsync(result.Item2);
                return Redirect(returnUrl);
                //return to return url or view;
            }
            else
            {
                ViewData["error"] = result.Item3;
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
                //TODO: Encrypt password here
                loginLogic.InsertRecord(model);
                return RedirectToAction("Login");
            }
            else
            {
                ViewData["Error"] = "You added null values. Please make sure you add valid values.";
                return RedirectToAction("Register");
            }
            //give the data to the register user backend
            
        }
    }
}
