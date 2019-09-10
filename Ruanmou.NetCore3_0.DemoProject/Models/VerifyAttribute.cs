using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using RM04.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruanmou04.NetCore.Project.Models
{
    public class VerifyAttribute : Attribute, IActionFilter
    {
        private IMemoryCache _memoryCache;
        public VerifyAttribute(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string key = context.HttpContext.Request.Headers["token"].SingleOrDefault();

            if (key == null)
            {
                throw new Exception("请登录后使用");
            }
            else
            {
               object userInfo = this._memoryCache.Get(key);

            }
        }
    }
}
