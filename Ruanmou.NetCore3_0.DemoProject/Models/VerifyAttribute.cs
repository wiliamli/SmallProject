using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using RM04.DBEntity;
using Ruanmou04.EFCore.Model.DtoHelper;
using Ruanmou04.NetCore.Service.Core.Authorization.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruanmou04.NetCore.Project.Models
{
    public class VerifyAttribute : Attribute, IActionFilter//,IAsyncActionFilter
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
            string key = context.HttpContext.Request.Headers["Authorization"].SingleOrDefault();
            if (key == null || key == "null")
            {
                throw new Exception("请登录后使用");
            }
            else
            {
                AjaxResult result = this._tokenService.ConfirmVerification(key);
                if (!result.success)
                {
                    throw new Exception("请登录后使用");
                }
            }
        }

        

    }
}
