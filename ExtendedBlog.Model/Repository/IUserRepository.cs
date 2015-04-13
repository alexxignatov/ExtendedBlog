using ExtendedBlog.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedBlog.Model.Repository
{
    public interface IUserRepository
    {
        UserProfile GetProfile(string userName);
        UserProfile GetProfile(int userId);
        void EditProfile(UserProfile profile);
        FileDescription GetAvatar(int userId);
        int AddAvatar(FileDescription file);
        void EditAvatar(FileDescription file);
    }
}
