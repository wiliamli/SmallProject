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
using Ruanmou04.NetCore.Service.Core.Authorization.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Ruanmou04.NetCore.Project.Utility
{
    public class CustomAuthorizeAttribute : Attribute, IActionFilter
    {
        private ITokenConfirmService _tokenConfirmService;
        public CustomAuthorizeAttribute()
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<TokenConfirmService>().As<ITokenConfirmService>();
            containerBuilder.RegisterType<JwtSecurityTokenHandler>().As<JwtSecurityTokenHandler>();

            var container = containerBuilder.Build();
            _tokenConfirmService = container.Resolve<ITokenConfirmService>();
            
            var token = context.HttpContext.Request.Headers["Authorization"];
            AjaxResult ajaxResult = null;
            if (token.IsNullOrEmpty())
            {
                ajaxResult = new AjaxResult { msg = "token为空", success = false };
            }
            else
            {
                ajaxResult = _tokenConfirmService.ConfirmVerificationAsync(token).GetAwaiter().GetResult();
            }
            if (!ajaxResult.success)
            {
                context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(ajaxResult));
            }
        }
    }
}
