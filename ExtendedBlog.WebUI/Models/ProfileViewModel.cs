using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using ExtendedBlog.Model.Repository;
using ExtendedBlog.Model.Data;

namespace ExtendedBlog.WebUI.Models
{
    public class ProfileViewModel
    {
        private IUserRepository repository;

        public ProfileViewModel(){}

        public ProfileViewModel(IUserRepository repository, string userName)
        {
            this.repository = repository;
            UserProfile userProfile = repository.GetProfile(userName);
            if (userProfile != null)
            {
                User = Mapper.Map<UserProfile, UserModel>(userProfile);
                
                var avatarImg = userProfile.Files.FirstOrDefault(v => v.Type == 1);
                if (avatarImg != null)
                {
                    AvatarImg = new FileDescriptionViewModel
                    {
                        LocalPath = avatarImg.LocalPath
                    };
                }
            }
        }

        public UserModel User { get; set; }
        public FileDescriptionViewModel AvatarImg { get; set; }
    }
}