using Microsoft.AspNetCore.Mvc.Filters;
using Ruanmou.Core.Utility;
using Ruanmou04.EFCore.Dtos.DtoHelper;
using Ruanmou04.NetCore.Interface.Tokens;
using System;
using System.Linq;

namespace Ruanmou04.NetCore.AOP.Filter
{
    public class SysVerifyAttribute : Attribute, IActionFilter
    {
        private ITokenService _tokenService;

        public SysVerifyAttribute(ITokenService tokenService)
        {
            this._tokenService = tokenService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string key = context.HttpContext.Request.Headers["Authorization"].SingleOrDefault();
            if (key == null)
            {
                context.HttpContext.Response.Redirect(StaticConstraint.SysDefaultUrl);
            }
            else
            {
                AjaxResult result = this._tokenService.ConfirmVerification(key);
                if (!result.success)
                {
                    context.HttpContext.Response.Redirect(StaticConstraint.SysDefaultUrl);
                }
            }
        }
    }
}
