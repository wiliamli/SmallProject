using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Ruanmou04.Core.Utility.Extensions;
using Ruanmou04.EFCore.Dtos.DtoHelper;
using Ruanmou04.NetCore.Interface.SystemManager.Applications;
using System.Linq;
using System.Threading.Tasks;

namespace Ruanmou04.NetCore.AOP.Middleware
{
    /// <summary>
    /// app.UseMiddleware<ElevenMiddleware>();
    /// </summary>
    public class AuthorizeMiddleware//: IMiddleware 可以实现，也可以不实现
    {
        private readonly RequestDelegate _next;
        private readonly ILoginApplication _loginApplication;
        private readonly IConfiguration _configuration;
        public AuthorizeMiddleware(RequestDelegate next, ILoginApplication loginApplication, IConfiguration configuration)
        {
            _next = next;
            _loginApplication = loginApplication;
            _configuration = configuration;
        }
        public async Task Invoke(HttpContext context)
        {
            var execpetApiInterface = _configuration.GetSection("ExecpetApiInterface").Value;
            string[] execpetApiInterfaceAry = { };
            if (!execpetApiInterface.IsNullOrWhiteSpace())
            {
                execpetApiInterfaceAry = execpetApiInterface.Split(",");
            }
            var exceptResult = execpetApiInterfaceAry.Where(s => context.Request.Path.Value.IndexOf($"/{s}/") > -1 || context.Request.Path.Value.IndexOf($"/{s}") > -1);
            if (context.Request.Method == "OPTIONS" || (context.Request.Path.HasValue && exceptResult.Count() > 0))
            {
                await _next.Invoke(context);
            }
            else
            {
                var token = context.Request.Headers["Authorization"];
                AjaxResult ajaxResult = null;
                if (token.IsNullOrEmpty())
                {
                    token = context.Request.Query["token"];
                }
                if (token.IsNullOrEmpty())
                {
                    ajaxResult = new AjaxResult { Message = "token为空", Success = false };
                }
                else
                {
                    ajaxResult = await _loginApplication.ConfirmVerificationAsync(token);
                }
                if (!ajaxResult.Success)
                {
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(ajaxResult));
                }
                else
                {
                    await _next.Invoke(context);
                }
            }
            //await context.Response.WriteAsync($"{nameof(AuthorityMiddleware)}Eleven,Hello World1!<br/>");
            //await _next(context);
            //await context.Response.WriteAsync($"{nameof(AuthorityMiddleware)}Eleven,Hello World2!<br/>");
        }
    }
}
