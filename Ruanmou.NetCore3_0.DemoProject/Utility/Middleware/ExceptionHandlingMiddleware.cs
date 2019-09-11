using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Ruanmou04.EFCore.Model.DtoHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruanmou04.NetCore.Project.Utility.Middleware
{
    /// <summary>
    /// 错误处理中间件
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<ExceptionHandlingMiddleware> _logger = null;
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var statusCode = context.Response.StatusCode;
                await HandleExceptionAsync(context, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exp)
        {
            AjaxResult ajaxResult = new AjaxResult { success=false,msg= "请求出错,请联系管理员" };
            _logger.LogError(exp, "请求出错");
            return context.Response.WriteAsync(JsonConvert.SerializeObject(ajaxResult));
        }
    }
}
