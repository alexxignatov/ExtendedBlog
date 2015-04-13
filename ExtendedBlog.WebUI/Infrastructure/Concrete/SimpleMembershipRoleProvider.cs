using ExtendedBlog.WebUI.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ExtendedBlog.WebUI.Infrastructure.Concrete
{
    public class SimpleMembershipRoleProvider : IRoleProvider
    {
        public bool IsUserInRole(string userName, string roleName)
        {
            return Roles.IsUserInRole(userName, roleName);
        }

        public void AddToRole(string userName, string roleName)
        {
            Roles.AddUserToRole(userName, roleName);
        }
    }
}