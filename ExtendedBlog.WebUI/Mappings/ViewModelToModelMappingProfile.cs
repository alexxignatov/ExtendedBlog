using AutoMapper;
using ExtendedBlog.Model.Data;
using ExtendedBlog.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExtendedBlog.WebUI.Mappings
{
    public class ViewModelToModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "ViewModelToModelMapping";
            }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<UserModel, UserProfile>();
        }
    }
}