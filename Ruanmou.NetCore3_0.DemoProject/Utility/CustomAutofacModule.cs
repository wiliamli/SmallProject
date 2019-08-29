using Autofac;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Ruanmou.EFCore3_0.Model;
using Ruanmou.NetCore.Interface;
using Ruanmou.NetCore.Service;
using Ruanmou04.NetCore.Service.Core.Authorization.Tokens;
using Ruanmou04.NetCore.Application.Forum;
using Ruanmou04.NetCore.Interface;
using Ruanmou04.NetCore.Interface.Forum.Applications;
using Ruanmou04.NetCore.Interface.Forum.Service;
using Ruanmou04.NetCore.Service.Forum;
using System.Linq;
using System.Reflection;

namespace Ruanmou.NetCore3_0.DemoProject.Utility
{
    public class CustomAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            var assembly = this.GetType().GetTypeInfo().Assembly;
            var builder = new ContainerBuilder();
            var manager = new ApplicationPartManager();
            manager.ApplicationParts.Add(new AssemblyPart(assembly));
            manager.FeatureProviders.Add(new ControllerFeatureProvider());
            var feature = new ControllerFeature();
            manager.PopulateFeature(feature);
            builder.RegisterType<ApplicationPartManager>().AsSelf().SingleInstance();
            builder.RegisterTypes(feature.Controllers.Select(ti => ti.AsType()).ToArray()).PropertiesAutowired();


            containerBuilder.Register(c => new CustomAutofacAop());//aop注册

            containerBuilder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(IBaseService)))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(IApplication)))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

            #region 上面的方式不行？！

            containerBuilder.RegisterType<ForumChannelService>().As<IForumChannelService>();
            containerBuilder.RegisterType<ForumChannelApplication>().As<IForumChannelApplication>();

            #endregion

            containerBuilder.RegisterType<JDDbContext>().As<DbContext>();

            containerBuilder.RegisterType<SysUserService>().As<ISysUserService>();
            containerBuilder.RegisterType<LoginService>().As<ILoginService>();
            containerBuilder.RegisterType<TokenService>().As<ITokenService>();

            //containerBuilder.RegisterType<SysUserService>().As<ISysUserService>();




        }
    }
}
