using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExtendedBlog.WebUI.Models
{
    public class FileDescriptionViewModel
    {
        public HttpPostedFileBase UploadedFile { get; set; }
        public string LocalPath { get; set; }
    }
}