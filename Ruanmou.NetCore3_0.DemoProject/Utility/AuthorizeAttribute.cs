using Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Ruanmou.Core.Utility.Filters;
using Ruanmou.NetCore.Application;
using Ruanmou.NetCore.Service;
using Ruanmou04.Core.Utility.Extensions;
using Ruanmou04.EFCore.Model.DtoHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Ruanmou04.NetCore.Project.Utility
{
    public class AuthorizeAttribute : Attribute, IActionFilter
    {
        private  ILoginApplication _loginApplication;
        private readonly ILogger<AuthorizeAttribute> _logger = null;
        public AuthorizeAttribute()//(ILogger<AuthorityAttribute> logger, ILoginApplication loginApplication)
        {
           
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            //containerBuilder.RegisterType<LoginService>().As<ILoginService>();
            containerBuilder.RegisterType<LoginApplication>().As<ILoginApplication>();

            var container = containerBuilder.Build();
            //var LoginService = container.Resolve<ILoginService>();
            _loginApplication = container.Resolve<ILoginApplication>();
            var token = context.HttpContext.Request.Headers["Authorization"];
            AjaxResult ajaxResult = null;
            if (token.IsNullOrEmpty())
            {
                ajaxResult = new AjaxResult { msg = "token为空", success = false };
            }
            else
            {
                ajaxResult = _loginApplication.ConfirmVerificationAsync(token).GetAwaiter().GetResult();
            }
            if (!ajaxResult.success)
            {
                context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(ajaxResult));
            }
        }
    }
}
