using Microsoft.AspNetCore.Mvc.Filters;
using Ruanmou.Core.Utility;
using Ruanmou04.EFCore.Dtos.DtoHelper;
using Ruanmou04.NetCore.Interface.Tokens;
using System;
using System.Linq;

namespace Ruanmou04.NetCore.AOP.Filter
{
    public class VerifyAttribute : Attribute, IActionFilter
    {
        //private IMemoryCache _memoryCache;
       // private ICurrentUserInfo _currentUserInfo;
        private ITokenService _tokenService;
        public VerifyAttribute(ITokenService tokenService)
        {
            //this._memoryCache = memoryCache;
            //this._currentUserInfo = currentUserInfo;
            this._tokenService = tokenService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string key = context.HttpContext.Request.Headers["token"].SingleOrDefault();
            if (key == null)
            {
                context.HttpContext.Response.Redirect(StaticConstraint.PortalDefaultUrl);
            }
            else
            {
                AjaxResult result = this._tokenService.ConfirmVerification(key);
                if (!result.success)
                {
                    context.HttpContext.Response.Redirect(StaticConstraint.PortalDefaultUrl);
                }
            }
        }

        

    }
}
