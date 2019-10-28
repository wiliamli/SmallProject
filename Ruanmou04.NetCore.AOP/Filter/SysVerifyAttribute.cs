using Microsoft.AspNetCore.Mvc.Filters;
using Ruanmou04.Core.Utility.Enums;
using Ruanmou04.Core.Utility.Extensions;
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
            // throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string key = context.HttpContext.Request.Headers["Authorization"].SingleOrDefault();
            var result = new StandardJsonResult()
            {

                Success = false,
                StatusCode = StatusCodeEnum.Authenticate_Failed.ToString(),
            };
            if (key.IsNullOrWhiteSpace())
            {
                result.Message = "请先登录";
                context.Result = result;
            }
            else
            {
                key = key.Replace("Bearer", string.Empty).Trim();
                AjaxResult ajaxResult = this._tokenService.ConfirmVerification(key);
                if (!ajaxResult.success)
                {
                    result.Message = ajaxResult.msg;
                    context.Result = result;
                }
            }
        }

        //public void OnActionExecuting(ActionExecutingContext context)
        //{
        //    string key = context.HttpContext.Request.Headers["Authorization"].SingleOrDefault();
        //    if (key.IsNullOrWhiteSpace())
        //    {
        //        context.Result = new StandardJsonResult()
        //        {
        //            Success = false,
        //            StatusCode = StatusCodeEnum.Authenticate_Failed.ToString(),
        //            Message = "请先登录"
        //        };
        //    }
        //    else
        //    {
        //        key = key.Replace("Bearer", string.Empty).Trim();
        //        AjaxResult ajaxResult = this._tokenService.ConfirmVerification(key);
        //        if (!ajaxResult.success)
        //            context.Result = new StandardJsonResult()
        //            {
        //                Success = false,
        //                StatusCode = StatusCodeEnum.Authenticate_Failed.ToString(),
        //                Message = ajaxResult.msg
        //            }; ;
        //    }
        //}
    }
}
