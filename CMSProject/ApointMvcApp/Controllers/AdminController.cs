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
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateRole(CreateRoleModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Roles.CreateRole(model.RoleName);
                    return RedirectToRoute("ViewAllRoles");
                }
                catch (Exception e) { ModelState.AddModelError("", e.Message); }
            }
            ModelState.AddModelError("anyerror", "Ошибка при создании роли");
            return View(model);
        }

        public ActionResult DeleteRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteRole(CreateRoleModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.RoleName == "Admin" || model.RoleName == "User")
                    {
                        ModelState.AddModelError("anyerror", "Нельзя удалить базовую роль");
                        return View(model);
                    }
                    Roles.DeleteRole(model.RoleName);
                    return RedirectToRoute("ViewAllRoles");
                }
                catch(Exception e) {ModelState.AddModelError("anyerror", e.Message); }
            }
            ModelState.AddModelError("anyerror", "Ошибка при удалении роли");
            return View(model);
        }

        public ActionResult ViewAllRoles()
        {
            return View(Roles.GetAllRoles());
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = core.UserRepository.FindByEmail(model.Email);
                if ( user != null)
                {
                    if (user.IsDeleted)
                    { 
                        user.IsDeleted = false; 
                        core.Submit();
                        ModelState.AddModelError("", "Пользователь с таким email был удален, теперь же он успешно восстановлен.");
                        return View(model);
                    }
                    ModelState.AddModelError("", "Пользователь с таким email уже зарегистрирован.");
                    return View(model);
                }
                var mailModel = new ConfirmationModel();
                mailModel.Email = model.Email;
                mailModel.Message = "Здравствуйте. Вам было выcлано приглашение стать пользователем на сайте APoint. Пройдите по ссылке:";
                core.VerificationRepository.Create(mailModel.Email);
                core.Submit();
                mailModel.ID = core.VerificationRepository.Find(mailModel.Email).ID;
                var mailController = new MailController();
                var email2send = mailController.Confirmation(mailModel);
                email2send.Deliver(); 
                return RedirectToRoute("AdminMainPage");
            }
            ModelState.AddModelError("anyerror", "Ошибка при попытке выслать приглашение");
            return View(model);
        }

        public ActionResult DeleteUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteUser(DeleteUserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = Membership.GetUser(model.Username,false);
                if (user != null)
                {
                    if (user.IsLockedOut)
                    {
                        ModelState.AddModelError("", "Пользователь уже удален");
                        return View(model);
                    }
                    var roles = Roles.GetRolesForUser(model.Username);
                    if (roles.Contains("Admin"))
                    {
                        ModelState.AddModelError("", "Пользователь-админ, а он у нас такой один, так что атата, нельзя Вас удалить");
                        return View(model);
                    }
                    Membership.DeleteUser(model.Username);
                    return RedirectToRoute("ViewAllUsers");
                }
            }
            ModelState.AddModelError("anyerror", "Ошибка при удалении - такой пользователь не зарегистрирован");
            return View(model);
        }

        public ActionResult ViewAllUsers()
        {
            int totalRecords;
            return View(Membership.GetAllUsers(10, 10, out totalRecords));
        }

    }
}
