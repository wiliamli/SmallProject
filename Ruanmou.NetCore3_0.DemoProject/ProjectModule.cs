using Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Ruanmou04.Core.Utility.Extensions;
using Ruanmou04.NetCore.Service;
using Ruanmou04.NetCore.Service.Core.Authorization.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou04.NetCore.Project
{
    public class ProjectModule : RuanMouModule
    {

        public override void OnInited(IContainer container)
        {
            //ConfigureTokenAuth(container);
        }
        /// <summary>
        /// 初始化配置文件
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <param name="_appConfiguration"></param>
        public static void Init(ContainerBuilder containerBuilder, IConfiguration _appConfiguration)
        {
            //IContainer container = containerBuilder.Build();
            //var tokenAuthConfig = container.Resolve<TokenAuthConfiguration>();
            //tokenAuthConfig.SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appConfiguration["Authentication:JwtBearer:SecurityKey"]));
            //tokenAuthConfig.Issuer = _appConfiguration["Authentication:JwtBearer:Issuer"];
            //tokenAuthConfig.Audience = _appConfiguration["Authentication:JwtBearer:Audience"];
            //tokenAuthConfig.SigningCredentials = new SigningCredentials(tokenAuthConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
            //var expiration = _appConfiguration["Authentication:JwtBearer:Expiration"];
            //tokenAuthConfig.Expiration = new TimeSpan(expiration.IsNullOrEmpty() ? Convert.ToInt32(expiration) : 10, 0, 0);
        }
    }
}
