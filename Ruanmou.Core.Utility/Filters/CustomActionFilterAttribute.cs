using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Ruanmou.Core.Utility.Filters
{
    /// <summary>
    /// Action的Filter
    /// </summary>
    public class CustomActionFilterAttribute :  Attribute, IActionFilter
    {
        

        private ILogger<CustomActionFilterAttribute> _logger = null;
        public CustomActionFilterAttribute(ILogger<CustomActionFilterAttribute> logger)
        {
            this._logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //context.HttpContext.Response.WriteAsync("ActionFilter Executed!");
            Console.WriteLine("ActionFilter Executed!");
            this._logger.LogDebug("ActionFilter Executed!");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //context.HttpContext.Response.WriteAsync("ActionFilter Executing!");
            Console.WriteLine("ActionFilter Executing!");
            this._logger.LogDebug("ActionFilter Executing!");
        }
    }

    public class CustomControllerActionFilterAttribute : Attribute, IActionFilter
    {
        private ILogger<CustomControllerActionFilterAttribute> _logger = null;
        public CustomControllerActionFilterAttribute(ILogger<CustomControllerActionFilterAttribute> logger)
        {
            this._logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("CustomControllerActionFilterAttribute Executed!");
            this._logger.LogDebug("CustomControllerActionFilterAttribute Executed!");
            //context.HttpContext.Response.WriteAsync("CustomControllerActionFilterAttribute Executed!");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("CustomControllerActionFilterAttribute Executing!");
            this._logger.LogDebug("CustomControllerActionFilterAttribute Executing!");
            //context.HttpContext.Response.WriteAsync("CustomControllerActionFilterAttribute Executing!");
        }
    }

    public class CustomGlobalActionFilterAttribute : Attribute, IActionFilter
    {
        private ILogger<CustomActionFilterAttribute> _logger = null;
        public CustomGlobalActionFilterAttribute(ILogger<CustomActionFilterAttribute> logger)
        {
            this._logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("CustomGlobalActionFilterAttribute Executed!");
            this._logger.LogDebug("CustomGlobalActionFilterAttribute Executed!");
            //context.HttpContext.Response.WriteAsync("CustomGlobalActionFilterAttribute Executed!");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("CustomGlobalActionFilterAttribute Executing!");
            this._logger.LogDebug("CustomGlobalActionFilterAttribute Executing!");
            //context.HttpContext.Response.WriteAsync("CustomGlobalActionFilterAttribute Executing!");
        }
    }
}