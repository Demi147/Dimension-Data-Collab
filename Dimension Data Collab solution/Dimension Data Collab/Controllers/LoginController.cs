using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackEnd.BussinessLogic;
using BackEnd.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core.Infrastructure;

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
        public IActionResult Login()
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
                if (returnUrl!=null)
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return Redirect("home/index");
                }
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
        public async Task<IActionResult> Register(PersonModel model)
        {
            //TODO: CHECK IF EMAIL UNIQUE >> DONE >> in backend > login logic
            if (ModelState.IsValid)
            {
                //TODO: Encrypt password here >>> Not here >> in Backend
                var result = await loginLogic.InsertRecord(model);
                if (result)
                {
                    return RedirectToAction("Login");
                }
                ViewData["Error"] = "This email adress allready exists..";
                return View();
            }
            else
            {
                ViewData["Error"] = "You added null values. Please make sure you add valid values.";
                return View();
            }
            
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

        [Authorize]
        public async Task<IActionResult> ViewAccount()
        {
            var email = HttpContext.User.Identities.ToList()[0].Claims.ToList()[2].Value;
            //get account by email
            var person = await loginLogic.GetUserByEmail(email);
            //push account into view

            return View(person);
        }

        [Authorize(Roles ="admin,manager")]
        [Route("Login/Index")]
        public async Task<IActionResult> ListAccounts()
        {
            //TODO: Add paging!!!!
            var data = await loginLogic.GetAllRecords(1,15);
            return View(data);
        }

        [Authorize(Roles = "admin,manager")]
        public async Task<IActionResult> EditAccount(string id)
        {
            //check a few things
            //edit own account??
            //is manager
            //is admin?
            var data = await loginLogic.GetRecordById(new MongoDB.Bson.ObjectId(id));
            return View(data);
        }

        [Authorize(Roles = "admin,manager")]
        [HttpPost]
        public IActionResult EditAccount(string id,PersonModel model)
        {
            //check a few things
            //edit own account??
            //is manager
            //is admin?
            //var data = await loginLogic.GetRecordById(new MongoDB.Bson.ObjectId(id));
            if (model.Role!=null)
            {
                loginLogic.UpdatePersonEmailNameRole(new MongoDB.Bson.ObjectId(id), model.Name, model.Email,model.Role);
                //IF CURRENT USER : LOGOUT
                //TODO
            }
            else
            {
                loginLogic.UpdatePersonEmailName(new MongoDB.Bson.ObjectId(id), model.Name, model.Email);
            }
            return RedirectToAction("ListAccounts");
        }

        [Authorize(Roles = "admin,manager")]
        public async Task<IActionResult> Details(string id)
        {
            var data = await loginLogic.GetRecordById(new MongoDB.Bson.ObjectId(id));
            return View(data);
        }

        [Authorize(Roles = "admin,manager")]
        [Route("Login/Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var data = await loginLogic.GetRecordById(new MongoDB.Bson.ObjectId(id));
            return View(data);
        }

        [Authorize(Roles = "admin,manager")]
        [HttpPost]
        [Route("Login/Delete/{id}")]
        public IActionResult DeletePost(string id)
        {
            //DELETE RECORD HERE
            loginLogic.DeleteRecord(new MongoDB.Bson.ObjectId(id));
            return RedirectToAction("ListAccounts");
        }
    }
}
