using ExtendedBlog.Model.Context;
using ExtendedBlog.Model.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedBlog.Model.Repository
{
    public class UserRepository : IUserRepository
    {
        private EFDbContext context;

        public UserRepository(EFDbContext context)
        {
            this.context = context;
        }

        public UserProfile GetProfile(string userName)
        {
            return context.UserProfile.FirstOrDefault(v => v.UserName == userName);
        }

        public void EditProfile(UserProfile profile)
        {
            context.Entry(profile).State = EntityState.Modified;
            context.SaveChanges();
        }

        public UserProfile GetProfile(int userId)
        {
            return context.UserProfile.Include(z => z.Files).FirstOrDefault(x => x.UserId == userId);
        }

        public FileDescription GetAvatar(int userId)
        {
            return context.Files.FirstOrDefault(x => x.Profile.UserId == userId);
        }

        public int AddAvatar(FileDescription file)
        {
            context.Files.Add(file);
            context.SaveChanges();

            return file.FileDescriptionId;
        }

        public void EditAvatar(FileDescription file)
        {
            context.Entry(file).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
