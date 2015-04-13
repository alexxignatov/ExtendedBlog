using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExtendedBlog.WebUI.Models
{
    public class PageInfo
    {
        public int PostPerPage { get; set; }
        public int TotalPosts { get; set; }
        public int CurrentPageNumber { get; set; }

        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalPosts / PostPerPage); }
        }

    }
}