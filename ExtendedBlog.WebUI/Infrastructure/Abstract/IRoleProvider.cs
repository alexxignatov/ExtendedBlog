using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedBlog.WebUI.Infrastructure.Abstract
{
    public interface IRoleProvider
    {
        bool IsUserInRole(string userName, string roleName);
        void AddToRole(string userName, string roleName);
    }
}
