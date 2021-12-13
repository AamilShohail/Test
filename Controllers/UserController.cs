using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Test.Context;
using Test.Models;

namespace Test.Controllers
{
    public class UserController : Controller
    {
        private readonly ProjectDBContext db = new ProjectDBContext();
        private readonly UserRepository userRepository = new UserRepository();

        // GET: User
        public ActionResult Index()
        {
            return View(userRepository.GetUsers().ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userRepository.GetUserById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost, ActionName("Confirm")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmEmail()
        {
            string code = "1";
            User user = userRepository.GetUserById(int.Parse(code));
            user.IsEmailConfirmed = true;
            userRepository.UpdateUser(user);
            return View();
        }
        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "UserId")] User user)
        {
            if (ModelState.IsValid)
            {
                user.IsEmailConfirmed = false;
                var resUserId = userRepository.InsertUser(user);
                var confirmationLink = Url.Action("Index", "Home", new { code = resUserId }, Request.Url.Scheme.ToString());
                SendEmail(confirmationLink, user);
                return RedirectToAction("Index");
            }

            return View(user);
        }
        [NonAction]
        public void SendEmail(string confirmationLink, User user)
        {
            MailMessage mailMessage = new MailMessage("aamilshohail@gmail.com", user.Email)
            {
                Subject = "Confirmation Link",
                Body = "Click here to confirm your email " + EmailHTMLBody(confirmationLink) + "\n" + "Username: " + user.Email + "\n" + "Password: " + user.Password,
                IsBodyHtml = true,
            };
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = "aamilshohail@gmail.com",
                    Password = "umrgniiniltvfjld",
                },
                EnableSsl = true

            };
            smtpClient.Send(mailMessage);
        }
        [NonAction]
        private static string EmailHTMLBody(string confirmationLink)
        {
            return
                "<html>" +
                    "<head>" +
                        "<style>" +
                            "button" +
                                "{" +
                                    "display: inline-block;" +
                                    "background-color: #7b38d8;" +
                                    "border-radius: 10px;" +
                                    "border: 4px double #cccccc;" +
                                    "color: #eeeeee;" +
                                    "text-align: center;" +
                                    "font-size: 28px;padding: 20px;width: 200px;" +
                                    "margin: 5px;" +
                                "}" +
                            "a" +
                                "{" +
                                    "text-decoration: none;" +
                                    "color: aliceblue;" +
                                "}" +
                        "</style>" +
                    "</head>" +
                    "<body>" +
                        "<button type =" +
                        "submit" +
                        ">" +
                            "<a href=" + confirmationLink + ">" +
                            "Confirm" +
                            "</a>" +
                        "</button>" +
                    "</body>" +
                "</html>"
                ;
        }
        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userRepository.GetUserById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                userRepository.UpdateUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userRepository.GetUserById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = userRepository.GetUserById(id);
            userRepository.DeleteUser(user);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
