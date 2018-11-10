using Autofac;
using DAL.Abstract;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            builder.RegisterType<CustomRoleStore>()
                .As<IRoleStore<CustomRole, int>>().InstancePerRequest();

            base.Load(builder);
        }
    }
}
