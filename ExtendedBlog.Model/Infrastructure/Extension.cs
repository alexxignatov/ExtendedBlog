using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedBlog.Model.Infrastructure
{
    public static class Extension
    {
        public static DateTime ToSqlDateTime(this DateTime time)
        {
            return new SqlDateTime(time).Value;
        }
    }
}
