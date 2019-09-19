using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Ruanmou.Core.Utility.Filters
{
    /// <summary>
    /// 异常处理的Filter
    /// </summary>
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;
        private ILogger<CustomExceptionFilterAttribute> _logger = null;

        /// <summary>
        /// ioc来的
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        /// <param name="modelMetadataProvider"></param>
        public CustomExceptionFilterAttribute(
            //IHostingEnvironment hostingEnvironment,
            //IModelMetadataProvider modelMetadataProvider,
            Logger<CustomExceptionFilterAttribute> logger)
        {
            //_hostingEnvironment = hostingEnvironment;
            //_modelMetadataProvider = modelMetadataProvider;
            _logger = logger;
        }

        /// <summary>
        /// 没有处理的异常，就会进来
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)//异常有没有被处理过
            {
                _logger.LogError(filterContext.Exception, filterContext.HttpContext.Request.Path + "---请求出错");
                var ajaxResult = new { success = false,msg= "请求出错,请联系管理员" };
                var result = JsonConvert.SerializeObject(ajaxResult);
                byte[] tempBytes = System.Text.Encoding.UTF8.GetBytes(result);
                string res = System.Text.Encoding.UTF8.GetString(tempBytes);
                filterContext.HttpContext.Response.WriteAsync(res);
            }
        }


        private bool IsAjaxRequest(HttpRequest request)
        {
            string header = request.Headers["X-Requested-With"];
            return "XMLHttpRequest".Equals(header);
        }
    }
}