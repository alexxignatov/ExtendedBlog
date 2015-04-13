using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedBlog.WebUI.Infrastructure.Abstract
{
    public interface IAuthProvider
    {
        string CurrentUserName { get; }
        bool IsAuthenticated { get; }
        bool Login(string username, string password, bool persistCookies = false);
        string CreateUserAndAccount(string userName, string password, object propertyValues = null);
        void Logout();
    }
}
