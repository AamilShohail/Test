using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Context;
using Test.Models;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserRepository userRepository = new UserRepository();
        private readonly PersonRepository personRepository = new PersonRepository();
        private readonly ProjectDBContext projectDBContext = new ProjectDBContext();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var regUsers = userRepository.GetUserByEmailandPassword(login);
                if (regUsers.Count() == 0)
                {
                    Session["Error"] = "Login failed, Email or Password is incorrect";
                    return RedirectToAction("Login");
                }
                if (!regUsers.FirstOrDefault().IsEmailConfirmed)
                {
                    Session["Error"] = "Email is not verified";
                    return RedirectToAction("Login");
                }
                //add session
                Session["FullName"] = regUsers.FirstOrDefault().Name;
                Session["Email"] = regUsers.FirstOrDefault().Email;
                Session["UserId"] = regUsers.FirstOrDefault().UserId;
                Session["Role"] = regUsers.FirstOrDefault().Role;

                return RedirectToAction("Index", "User");
            }
                return View();
        }
        //Logout
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            //remove session
            Session.Clear();
            return RedirectToAction("Login");
        }
        public ActionResult Index(string code)
        {
            User user = userRepository.GetUserById(int.Parse(code));
            user.IsEmailConfirmed = true;
            userRepository.UpdateUser(user);
            return RedirectToAction("About", "Home");
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