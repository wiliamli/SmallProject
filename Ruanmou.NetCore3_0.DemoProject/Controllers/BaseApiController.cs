using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RM04.DBEntity;
using Ruanmou04.Core.Utility.MvcResult;

namespace Ruanmou04.NetCore.Project.Controllers
{
    /// <summary>
    /// WebApi基类
    /// </summary>
    public class BaseApiController : ControllerBase
    {
        private IMemoryCache _memoryCache;
        public BaseApiController(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }

        /// <summary>
        /// 标准数据返回
        /// </summary>
        /// <param name="action">action</param>
        /// <returns></returns>
        protected StandardJsonResult StandardAction(Action action)
        {
            var result = new StandardJsonResult();
            result.StandardAction(action);
            return result;
        }

        /// <summary>
        /// 标准数据返回
        /// </summary>
        /// <typeparam name="T">返回参数</typeparam>
        /// <param name="func">func</param>
        /// <returns></returns>
        protected StandardJsonResult<T> StandardAction<T>(Func<T> func)
        {
            var result = new StandardJsonResult<T>();
            result.StandardAction(() =>
            {
                result.Data = func();
            });
            return result;
        }

        protected SysUserOutputDto GetUserInfo()
        {
            string key = HttpContext.Request.Headers["token"].SingleOrDefault();

            if (key != null)
            {
                return this._memoryCache.Get(key) as SysUserOutputDto;
            }
            else
            {
                throw new Exception("未登录");
            }

            
        }
    }
}
