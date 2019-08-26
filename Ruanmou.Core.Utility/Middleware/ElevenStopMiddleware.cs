using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruanmou.Core.Utility.Middleware
{
    /// <summary>
    /// app.UseMiddleware<ElevenMiddleware>();
    /// </summary>
    public class ElevenStopMiddleware
    {
        private readonly RequestDelegate _next;

        public ElevenStopMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value.Contains("Eleven"))//中文输出不乱码  需要配置context的头
                await context.Response.WriteAsync($"{nameof(ElevenStopMiddleware)}这里是Eleven的终结点<br/>", System.Text.Encoding.UTF8);
            else
            {
                await context.Response.WriteAsync($"{nameof(ElevenStopMiddleware)}Eleven,Hello World Stop1!<br/>");
                await _next(context);
                await context.Response.WriteAsync($"{nameof(ElevenStopMiddleware)}Eleven,Hello World Stop2!<br/>");
            }
        }
    }
}
