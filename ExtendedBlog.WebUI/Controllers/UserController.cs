using AutoMapper;
using ExtendedBlog.Model.Data;
using ExtendedBlog.Model.Repository;
using ExtendedBlog.WebUI.Infrastructure.Abstract;
using ExtendedBlog.WebUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExtendedBlog.WebUI.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository repository;
        private IAuthProvider provider;

        public UserController(IUserRepository repository, IAuthProvider provider)
        {
            this.provider = provider;
            this.repository = repository;
        }

        public ActionResult Profile(string userName)
        {
            ProfileViewModel viewModel = new ProfileViewModel(repository, userName);
            if (viewModel.User == null)
                return HttpNotFound();

            return View(viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Profile(ProfileViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var domain = repository.GetProfile(viewModel.User.UserId);
                UserProfile profile = Mapper.Map<UserModel, UserProfile>(viewModel.User, domain);

                repository.EditProfile(profile);

                return RedirectToAction("List", "Post");
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadAvatar(ProfileViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.AvatarImg.UploadedFile != null)
                {
                    string fileName = Path.GetFileName(viewModel.AvatarImg.UploadedFile.FileName);

                    string serverPath = "~/Content/images/" + fileName;
                    viewModel.AvatarImg.UploadedFile.SaveAs(Server.MapPath(serverPath));

                    /*var domain = repository.GetProfile(viewModel.User.UserId);
                    UserProfile profile = Mapper.Map<UserModel, UserProfile>(viewModel.User, domain);*/

                    FileDescription file = repository.GetAvatar(viewModel.User.UserId);
                    if (file != null)
                    {
                        file.LocalPath = serverPath;
                        //repository.
                        var z = file.Profile;

                        repository.EditAvatar(file);
                    }
                    else
                    {
                        file = new FileDescription
                        {
                            LocalPath = serverPath,
                            Type = 1,
                            Profile = repository.GetProfile(viewModel.User.UserId)
                        };

                        repository.AddAvatar(file);
                    }

                    return RedirectToAction("Profile", new { userName = provider.CurrentUserName });
                }

                ModelState.AddModelError("", "select some image!!!");
            }

            return RedirectToAction("Profile", new { userName = provider.CurrentUserName });
        }
    }
}