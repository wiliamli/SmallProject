using System;
using System.IdentityModel.Tokens.Jwt;
using Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Ruanmou04.Core.Utility.Extensions;
using Ruanmou04.EFCore.Dtos.DtoHelper;
using Ruanmou04.NetCore.Interface.Tokens;
using Ruanmou04.NetCore.Service.Core.Authorization.Tokens;

namespace Ruanmou04.NetCore.AOP.Filter
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
            if(token.IsNullOrEmpty())
            {
                token = context.HttpContext.Request.Query["token"];
            }
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
