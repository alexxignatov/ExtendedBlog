using ExtendedBlog.WebUI.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace ExtendedBlog.WebUI.Infrastructure.Concrete
{
    public class SimpleMembershipAuthProvider : IAuthProvider
    {
        public bool IsAuthenticated
        {
            get { return WebSecurity.IsAuthenticated; }
        }

        public void Logout()
        {
            WebSecurity.Logout();
        }

        public bool Login(string username, string password, bool persistCookies = false)
        {
            return WebSecurity.Login(username, password, persistCookies);
        }

        public string CreateUserAndAccount(string userName, string password, object propertyValues = null)
        {
            return WebSecurity.CreateUserAndAccount(userName, password, propertyValues);
        }

        public string CurrentUserName
        {
            get { return WebSecurity.CurrentUserName; }
        }

    }
}