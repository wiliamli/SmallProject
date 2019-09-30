using Autofac;
using Microsoft.IdentityModel.Tokens;
using Ruanmou04.Core.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou.Core.Utility
{
    public class StaticConstraint
    {
        public static void Init(Func<string, string> func)
        {
            //循环--反射的方式初始化多个

            JDDbConnection = func.Invoke("ConnectionStrings:JDDbConnectionString");

            DefaultPageSize = Convert.ToInt32(func.Invoke("DefaultPageSize"));
            DefaultPageSize = DefaultPageSize == 0 ? 20 : DefaultPageSize;
            //MaxPageSize = Convert.ToInt32( func.Invoke("MaxPageSize"));
            //MaxPageSize = MaxPageSize == 0 ? 20 : MaxPageSize;

            var expirationTimespan = func.Invoke("Expiration");
            //expirationTimespan = expirationTimespan == 0 ? 480 : expirationTimespan;

            //Expiration = new TimeSpan(expirationTimespan) ;
            Expiration = new TimeSpan(expirationTimespan.IsNullOrEmpty() ? Convert.ToInt32(expirationTimespan) / 60 : 8, 0, 0);
            SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(func.Invoke("Authentication:JwtBearer:SecurityKey")));
            Issuer = func.Invoke("Authentication:JwtBearer:Issuer");
            Audience = func.Invoke("Authentication:JwtBearer:Audience");
            SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
            var expiration = func.Invoke("Authentication:JwtBearer:Expiration");

        }

        /// <summary>
        /// 以前直接读配置文件
        /// ConnectionStrings:JDDbConnectionString
        /// </summary>
        public static string JDDbConnection { get; private set; }
        /// <summary>
        /// 默认页条数,最大条数
        /// </summary>
        public static int DefaultPageSize { get; private set; }
        public const int MaxPageSize = 1000;
        //权限cookie名称
        public const string AuthenticationScheme = "Cookies";


        #region token属性
        public static SymmetricSecurityKey SecurityKey { get; private set; }

        public static string Issuer { get; private set; }

        public static string Audience { get; private set; }

        public static SigningCredentials SigningCredentials { get; private set; }
        //登录过期时间
        public static TimeSpan Expiration { get; private set; }

        public const string DefaultPassPhrase = "gsKxGZ012HLL3MI5";

        #endregion


        #region  默认密码
        public readonly static string DefaultPwd = "DefaultPassword";
        #endregion
    }
}
