using Autofac;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Ruanmou.EFCore3_0.Model;
using Ruanmou.NetCore.Interface;
using Ruanmou.NetCore.Service;
using Ruanmou04.NetCore.Service.Core.Authorization.Tokens;
using Ruanmou04.NetCore.Application;
using Ruanmou04.NetCore.Application.Forum;
using Ruanmou04.NetCore.Interface;
using Ruanmou04.NetCore.Interface.Forum.Applications;
using Ruanmou04.NetCore.Interface.Forum.Service;
using Ruanmou04.NetCore.Service.Forum;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.AspNetCore.Http;
using Autofac.Core;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Ruanmou.NetCore3_0.DemoProject.Utility
{
    /// <summary>
    /// Autofac配置
    /// </summary>
    public class CustomAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {

            containerBuilder.Register(c => new CustomAutofacAop());//aop注册

            //自动注册service 下面 实现 了IBaseService或者 IApplication 的所有类
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("Ruanmou04.NetCore.Service"));

            containerBuilder.RegisterAssemblyTypes(assembly)
                .Where(t => (typeof(IBaseService).IsAssignableFrom(t) || typeof(IApplication).IsAssignableFrom(t)) && t != typeof(IBaseService) && t != typeof(IApplication))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

            containerBuilder.RegisterType<JDDbContext>().As<DbContext>();
            containerBuilder.RegisterType<JwtSecurityTokenHandler>().As<JwtSecurityTokenHandler>();

            #region 上面的方式不行？！

            //containerBuilder.RegisterType<ForumChannelService>().As<IForumChannelService>();
            //containerBuilder.RegisterType<ForumChannelApplication>().As<IForumChannelApplication>();

            //containerBuilder.RegisterType<ForumAttachmentService>().As<IForumAttachmentService>();
            //containerBuilder.RegisterType<ForumAttachmentApplication>().As<IForumAttachmentApplication>();

            //containerBuilder.RegisterType<ForumCheckInService>().As<IForumCheckInService>();
            //containerBuilder.RegisterType<ForumCheckInApplication>().As<IForumCheckInApplication>();

            //containerBuilder.RegisterType<ForumConcernService>().As<IForumConcernService>();
            //containerBuilder.RegisterType<ForumConcernApplication>().As<IForumConcernApplication>();

            //containerBuilder.RegisterType<ForumInvitationService>().As<IForumInvitationService>();
            //containerBuilder.RegisterType<ForumInvitationApplication>().As<IForumInvitationApplication>();

            //containerBuilder.RegisterType<ForumTopicService>().As<IForumTopicService>();
            //containerBuilder.RegisterType<ForumTopicApplication>().As<IForumTopicApplication>();

            #endregion

            //containerBuilder.RegisterType<TokenAuthConfiguration>().As<TokenAuthConfiguration>().SingleInstance();
            //containerBuilder.RegisterType<IHttpContextAccessor>().As<HttpContextAccessor>();

            //containerBuilder.RegisterType<SysUserService>().As<ISysUserService>();
            //containerBuilder.RegisterType<LoginService>().As<ILoginService>();
            //containerBuilder.RegisterType<TokenService>().As<ITokenService>();



        }
    }
}
