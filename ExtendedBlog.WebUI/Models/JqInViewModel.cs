using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExtendedBlog.WebUI.Models
{
    public class JqInViewModel
    {
        /// <summary>
        /// number of rows to fetch
        /// </summary>
        public int rows { get; set; }

        /// <summary>
        /// the page index
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// sort column name
        /// </summary>
        public string sidx { get; set; }

        /// <summary>
        /// sort order "asc" or "desc"
        /// </summary>
        public string sord { get; set; }
    }
}