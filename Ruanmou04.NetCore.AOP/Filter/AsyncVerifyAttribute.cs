
using Microsoft.AspNetCore.Mvc.Filters;
using Ruanmou04.NetCore.Interface.Token.Applications;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ruanmou04.NetCore.AOP.Filter
{
    public class AsyncVerifyAttribute : Attribute, IAsyncActionFilter
    {
        private ITokenApplication _tokenService;
        public AsyncVerifyAttribute(ITokenApplication tokenService)
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
