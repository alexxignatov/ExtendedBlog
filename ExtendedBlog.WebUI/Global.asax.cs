using ExtendedBlog.Model.Context;
using ExtendedBlog.Model.Data;
using ExtendedBlog.Model.Repository;
using ExtendedBlog.WebUI.Binder;
using ExtendedBlog.WebUI.Infrastructure.Abstract;
using ExtendedBlog.WebUI.Infrastructure.Concrete;
using ExtendedBlog.WebUI.Mappings;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using WebMatrix.WebData;

namespace ExtendedBlog.WebUI
{
    // Примечание: Инструкции по включению классического режима IIS6 или IIS7 
    // см. по ссылке http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : NinjectHttpApplication
    {
        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            kernel.Load(Assembly.GetExecutingAssembly());
            //DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));

            kernel.Bind<IPostRepository>().To<PostRepository>().InRequestScope();
            kernel.Bind<IUserRepository>().To<UserRepository>().InRequestScope();
            kernel.Bind<IAuthProvider>().To<SimpleMembershipAuthProvider>();
            kernel.Bind<IRoleProvider>().To<SimpleMembershipRoleProvider>();

            return kernel;
        }

        protected override void OnApplicationStarted()
        {
            AreaRegistration.RegisterAllAreas();

            BundleConfig.RegisterBundles(BundleTable.Bundles);
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders.Add(typeof(Post), new PostModelBinder(Kernel));
            //ModelBinders.Binders.Add(typeof(Category), new CategoryModelBinder());

            //AutoMapper init configuration
            AutoMapperConfiguration.Configure();

            System.Data.Entity.Database.SetInitializer(new BlogContextSeedInitializer());

            WebSecurity.InitializeDatabaseConnection("EFDbContext",
                                                     "UserProfile",
                                                     "UserId",
                                                     "UserName",
                                                     autoCreateTables: true);

            new EFDbContext().UserProfile.Find(1);
            CreateAdminAndRoles();

            base.OnApplicationStarted();
        }

        private void CreateAdminAndRoles()
        {
            var z = Membership.GetUser(adminUser);
            if (z == null)
                WebSecurity.CreateUserAndAccount(adminUser, "123456");

            if (!Roles.RoleExists(adminRole))
                Roles.CreateRole(adminRole);

            if (!Roles.RoleExists(userRole))
                Roles.CreateRole(userRole);

            if (!Roles.IsUserInRole(adminUser, adminRole))
                Roles.AddUserToRole(adminUser, adminRole);
        }

        private string adminUser = "Admin";
        private string adminRole = "Administrator";
        private string userRole = "User";
    }
}