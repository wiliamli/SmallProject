using Microsoft.AspNetCore.Mvc.Filters;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.EFCore.Dtos.DtoHelper;
using Ruanmou04.NetCore.Interface.Token.Applications;
using System;
using System.Linq;

namespace Ruanmou04.NetCore.AOP.Filter
{
    public class SysVerifyAttribute : Attribute, IActionFilter
    {
        private ITokenApplication _tokenService;

        public SysVerifyAttribute(ITokenApplication tokenService)
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
            key = key.Replace("Bearer", string.Empty).Trim();
            if (key == null || key == "null")
            {
                context.Result = new StandardJsonResult();
            }
            else
            {
                AjaxResult result = this._tokenService.ConfirmVerification(key);
                if (!result.success)
                {
                    context.Result = new StandardJsonResult();
                }
            }
        }
    }
}
