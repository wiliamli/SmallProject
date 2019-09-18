
using Microsoft.AspNetCore.Mvc.Filters;
using Ruanmou04.NetCore.Service.Core.Authorization.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruanmou04.NetCore.Project.Models
{
    public class AsyncVerifyAttribute : Attribute, IAsyncActionFilter
    {
        private ITokenService _tokenService;
        public AsyncVerifyAttribute(ITokenService tokenService)
        {
            this._tokenService = tokenService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string key = context.HttpContext.Request.Headers["Authorization"].SingleOrDefault();

            if (key == null)
            {
                throw new Exception("请登录后使用");
            }
            else
            {
                var result = await this._tokenService.ConfirmVerificationAsync(key);
                if (!result.success)
                {
                    throw new Exception("请登录后使用");
                }
            }
        }
    }
}
