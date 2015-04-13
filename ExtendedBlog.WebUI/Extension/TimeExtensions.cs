using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExtendedBlog.WebUI.Extension
{
    public static class TimeExtensions
    {
        public static string ToArticleTime(this DateTime time)
        {
            return string.Format("{0:d MMM yyy}", time);
        }
    }
}