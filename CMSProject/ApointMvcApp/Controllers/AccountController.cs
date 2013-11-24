using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApointMvcApp.Filters;
using System.Web.Security;
using ApointMvcApp.Providers;
using ApointMvcApp.Models;
using AppCore;

namespace ApointMvcApp.Controllers
{
   // [Authorize]
    public class AccountController : BaseController
    {
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
               return RedirectToRoute("UserMainPage");      
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
                try
                {
                    if (Membership.ValidateUser(model.UserName, model.Password))
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        core.UserRepository.ChangeOnlineState(model.UserName, true);
                        core.Submit();
                        if (Url.IsLocalUrl(returnUrl))
                            return Redirect(returnUrl);
                        else
                        {
                            logger.Info(String.Format("Пользователь {0} вошел в систему", model.UserName));
                            if (User.IsInRole("Admin"))
                                return RedirectToRoute("AdminMainPage");
                            else return RedirectToRoute("UserMainPage");
                        }
                    }
                }
                catch(Exception e) { ModelState.AddModelError("",e.Message); }
            ModelState.AddModelError("", "Ошибка в процессе аутентификации");
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            core.UserRepository.ChangeOnlineState(User.Identity.Name, false);
            core.Submit();
            FormsAuthentication.SignOut();
            logger.Info(String.Format("Пользователь {0} вышел из системы", User.Identity.Name));
            return RedirectToAction("Login", "Account");
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register(string email, string verifyingID)
        {
            ViewBag.Email = email;
            ViewBag.VerifyingID = verifyingID;
            if (User.Identity.IsAuthenticated)
                return RedirectToRoute("UserMainPage");
            //if there are 0 users in the db - registering the admin
            if (core.UserRepository.ShouldCreateAdmin())
                ViewBag.Title = "Зарегистрировать администратора";
            //if any user on this machine is online, dont; pass the new user to register
            else
            {
                Guid guid;
                var parse_result = Guid.TryParse(verifyingID, out guid);
                //if anyone is trying to get access to the page without the link in the mail, redirect this anyone
                if (!parse_result || email == null)
                    return RedirectToRoute("Login");
                //the same, but with a wrong link
                if (!core.VerificationRepository.IsEmailWithSuchGuidRegistered(email, guid))
                    return RedirectToRoute("Login");
                //if it's the admin by the mail's link, login&redirect them to their admin's main page
                else if(Membership.GetUser(email, false) != null)                   
                    {
                        core.UserRepository.VerifyUser(email);
                        core.VerificationRepository.Delete(core.VerificationRepository.Find(email).ID);
                        core.Submit();
                        FormsAuthentication.SetAuthCookie(email, false);
                        core.UserRepository.ChangeOnlineState(email, true);
                        return RedirectToRoute("AdminMainPage");
                    }
                ViewBag.Title = "Зарегистрировать нового пользователя";
            }
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model, string email, string verifyingID)
        {
            if (core.UserRepository.ShouldCreateAdmin())
                ViewBag.Title = "Зарегистрировать администратора";
            else ViewBag.Title = "Зарегистрировать нового пользователя";
            if (ModelState.IsValid)
            {
                try
                {
                    MembershipUser membershipUser = ((CustomMembershipProvider)Membership.Provider).CreateUser(model.Login, model.Email, model.Password);
                    if (membershipUser != null)
                        //if the admin is registered, send him a letter and redirect to the login
                        if (core.UserRepository.IsInRole(core.UserRepository.FindByEmail(model.Email), core.RoleRepository.Find("Admin")))
                        {
                            var mailModel = new ConfirmationModel();
                            mailModel.Email = membershipUser.Email;
                            mailModel.Message = "Здравствуйте. По всей видимости, Вы стали админом на сайте APoint. Для подтверждения почтового адреса перейдите по ссылке.";
                            core.VerificationRepository.Create(mailModel.Email);
                            core.Submit();
                            mailModel.ID = core.VerificationRepository.Find(mailModel.Email).ID;
                            var mailController = new MailController();
                            var email2send = mailController.Confirmation(mailModel);
                            email2send.Deliver();
                            return RedirectToRoute("Login");
                        }
                        //if the user is registered, verify him and redirect to his main page
                        else
                        {
                            core.UserRepository.VerifyUser(model.Email);
                            core.VerificationRepository.Delete(core.VerificationRepository.Find(model.Email).ID);
                            core.Submit();
                            FormsAuthentication.SetAuthCookie(model.Email, false);
                            core.UserRepository.ChangeOnlineState(model.Email,true);
                            core.Submit();
                            return RedirectToRoute("UserMainPage");
                        }
                }
                catch (Exception e) { ModelState.AddModelError("", e.Message); }
            }
            ModelState.AddModelError("", "Ошибка при регистрации");
            ViewBag.Email = email;
            ViewBag.VerifyingID = verifyingID;
            return View(model);
        }


    }
}
