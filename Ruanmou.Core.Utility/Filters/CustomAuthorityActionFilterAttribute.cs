using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Ruanmou.NetCore2.MVC6.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ruanmou.Core.Utility.Filters
{
    /// <summary>
    /// Action的Filter,但是用来检测权限
    /// </summary>
    public class CustomAuthorityActionFilterAttribute : Attribute, IActionFilter
    {
        private ILogger<CustomActionFilterAttribute> _logger = null;
        public CustomAuthorityActionFilterAttribute(ILogger<CustomActionFilterAttribute> logger)
        {
            this._logger = logger;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            this._logger.LogDebug("CustomAuthorityActionFilterAttribute Executing!");
            string userString = context.HttpContext.Session.GetString("CurrentUser");
            if (!string.IsNullOrWhiteSpace(userString))
            {
                CurrentUser currentUser = Newtonsoft.Json.JsonConvert.DeserializeObject<CurrentUser>(userString);
                this._logger.LogDebug($"CustomAuthorityActionFilterAttribute 权限检查通过了 {currentUser.Name}登陆了系统!");
            }
            else
            {
                context.Result = new RedirectResult("~/Fourth/Login");//短路器
            }
        }


        public void OnActionExecuted(ActionExecutedContext context)
        {
            this._logger.LogDebug("CustomAuthorityActionFilterAttribute Executed!");
        }


    }
}