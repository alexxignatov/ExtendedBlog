using ExtendedBlog.WebUI.Infrastructure.Abstract;
using ExtendedBlog.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExtendedBlog.WebUI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IAuthProvider authProvider;
        private IRoleProvider roleProvider;

        public AccountController(IAuthProvider authProvider, IRoleProvider roleProvider)
        {
            this.authProvider = authProvider;
            this.roleProvider = roleProvider;
        }

        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (authProvider.IsAuthenticated)
            {
                if (roleProvider.IsUserInRole(authProvider.CurrentUserName, "User"))
                    return RedirectToAction("List", "Post");
                //return Redirect(Url.Action("List", "Post"));
                else
                    return RedirectToUrl(returnUrl);
            }

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
            if (ModelState.IsValid && authProvider.Login(model.UserName, model.Password, model.RememberMe))
            {
                return RedirectToUrl(returnUrl);
            }

            ModelState.AddModelError("", "Логин или пароль неверны");
            return View(model);
        }

        public ActionResult Logout()
        {
            authProvider.Logout();

            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    authProvider.CreateUserAndAccount(model.UserName, model.Password,
                                                        new
                                                        {
                                                            EMail = model.EMail,
                                                            PhoneNumber = model.PhoneNumber
                                                        });
                    roleProvider.AddToRole(model.UserName, "User");
                    authProvider.Login(model.UserName, model.Password);
                    return RedirectToAction("List", "Post");
                }
                catch (System.Web.Security.MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", string.Format("error status code = {0}", e.StatusCode));
                    //ErrorCodeToString(e.StatusCode);
                }
            }

            ModelState.AddModelError("", "Неверно заполнена форма регистрации");
            return View(model);
        }

        [AllowAnonymous]
        public PartialViewResult AdminButtons()
        {
            if (authProvider.IsAuthenticated)
            {
                ViewBag.UserName = authProvider.CurrentUserName;
                return PartialView("_UserDropdown");
            }

            return null;
        }

        private RedirectResult RedirectToUrl(string url)
        {
            if (Url.IsLocalUrl(url))
            {
                return Redirect(url);
            }
            else
            {
                return Redirect(Url.Action("Manage", "Admin"));
            }
        }
    }
}