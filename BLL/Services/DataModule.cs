using Autofac;
using BLL.Services.Identity;
using DAL.Abstract;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL.Services
{
    public class DataModule : Module
    {
        private string _connStr;
        private IAppBuilder _app;
        public DataModule(string connString, IAppBuilder app)
        {
            _connStr = connString;
            _app = app;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new ApplicationDbContext(this._connStr))
                .As<IAppDBContext>().InstancePerRequest();
            builder.Register(ctx =>
            {
                var context = (ApplicationDbContext)ctx.Resolve<IAppDBContext>();
                return context;
            }).AsSelf().InstancePerDependency();
            builder.RegisterType<CustomUserStore>()
                .As<IUserStore<ApplicationUser, int>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>()
                .AsSelf().InstancePerRequest();
            builder.RegisterType<CustomRoleStore>()
                .As<IRoleStore<CustomRole, int>>().InstancePerRequest();
            builder.RegisterType<ApplicationRoleManager>()
                .AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>()
                .AsSelf().InstancePerRequest();
            builder.Register<IAuthenticationManager>(c => 
                HttpContext.Current.GetOwinContext().Authentication)
                .InstancePerRequest();
            builder.Register<IDataProtectionProvider>(c => 
                _app.GetDataProtectionProvider()).InstancePerRequest();

            base.Load(builder);
        }
    }
}
