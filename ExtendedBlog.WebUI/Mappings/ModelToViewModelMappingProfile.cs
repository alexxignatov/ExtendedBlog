using AutoMapper;
using ExtendedBlog.Model.Data;
using ExtendedBlog.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExtendedBlog.WebUI.Mappings
{
    public class ModelToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "ModelToViewModelMapping";
            }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<UserProfile, UserModel>();
        }
    }
}