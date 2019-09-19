﻿using Autofac;
using Microsoft.EntityFrameworkCore;
using Ruanmou.EFCore3_0.Model;
using Ruanmou.NetCore.Interface;
using Ruanmou04.NetCore.Interface;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System;

namespace Ruanmou04.NetCore.AOP.IOC
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
            var assembly = AppDomain.CurrentDomain.GetAssemblies().ToArray();  //AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("Ruanmou04.NetCore.Service"));

            containerBuilder.RegisterAssemblyTypes(assembly)
                .Where(t => (typeof(IBaseService).IsAssignableFrom(t) || typeof(IApplication).IsAssignableFrom(t)) && t != typeof(IBaseService) && t != typeof(IApplication))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();



            containerBuilder.RegisterType<JDDbContext>().As<DbContext>();
            containerBuilder.RegisterType<JwtSecurityTokenHandler>().As<JwtSecurityTokenHandler>();


        }
    }
}
