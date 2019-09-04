using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Ruanmou.NetCore.Application;
using Ruanmou04.Core.Utility.Extensions;
using Ruanmou04.EFCore.Model.DtoHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruanmou.Core.Utility.Middleware
{
    /// <summary>
    /// app.UseMiddleware<ElevenMiddleware>();
    /// </summary>
    public class AuthorityMiddleware//: IMiddleware 可以实现，也可以不实现
    {
        private readonly RequestDelegate _next;
        private readonly ILoginApplication _loginApplication;
        private readonly IConfiguration _configuration;
        public AuthorityMiddleware(RequestDelegate next, ILoginApplication loginApplication, IConfiguration configuration)
        {
            _next = next;
            _loginApplication = loginApplication;
            _configuration = configuration;
        }
        public async Task Invoke(HttpContext context)
        {
            var requestController = context.Response;

            if (context.Request.Path.HasValue && context.Request.Path.Value.Contains("/api/Login/"))
            {
                await _next.Invoke(context);
            }
            else
            {
                var token = context.Response.Headers["Authorization"];
                AjaxResult ajaxResult = null;
                if (token.IsNullOrEmpty())
                {
                    ajaxResult = new AjaxResult { msg = "token为空",success=false };
                }
                else
                {
                    ajaxResult = await _loginApplication.ConfirmVerificationAsync(token);
                }
                if (!ajaxResult.success)
                {
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(ajaxResult));
                }
            }
            //await context.Response.WriteAsync($"{nameof(AuthorityMiddleware)}Eleven,Hello World1!<br/>");
            //await _next(context);
            //await context.Response.WriteAsync($"{nameof(AuthorityMiddleware)}Eleven,Hello World2!<br/>");
        }
    }
}
