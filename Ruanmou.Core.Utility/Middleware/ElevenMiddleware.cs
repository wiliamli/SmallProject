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
    public class ElevenMiddleware//: IMiddleware 可以实现，也可以不实现
    {
        private readonly RequestDelegate _next;
        public ElevenMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync($"{nameof(ElevenMiddleware)}Eleven,Hello World1!<br/>");
            await _next(context);
            await context.Response.WriteAsync($"{nameof(ElevenMiddleware)}Eleven,Hello World2!<br/>");
        }
    }
}
