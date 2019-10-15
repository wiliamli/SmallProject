using Microsoft.AspNetCore.Mvc.Filters;
using Ruanmou.Core.Utility;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.EFCore.Dtos.DtoHelper;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos;
using Ruanmou04.NetCore.Interface.Token.Applications;
using System;
using System.Linq;

namespace Ruanmou04.NetCore.AOP.Filter
{
    public class VerifyAttribute : Attribute, IActionFilter
    {
        private ITokenApplication _tokenService;
        public VerifyAttribute(ITokenApplication tokenService)
        {
            this._tokenService = tokenService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string key = context.HttpContext.Request.Headers["token"].SingleOrDefault();
            
            if (key == null || key =="null")
            {
                context.Result = new StandardJsonResult();
            }
            else
            {
                key = key.Replace("Bearer", string.Empty).Trim();
                AjaxResult result = this._tokenService.ConfirmVerification(key);
                if (!result.success)
                {
                    context.Result = new StandardJsonResult();
                }
            }
        }
    }
}
