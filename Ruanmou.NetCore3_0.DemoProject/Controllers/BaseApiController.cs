using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RM04.DBEntity;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.NetCore.Project.Models;

namespace Ruanmou04.NetCore.Project.Controllers
{
    /// <summary>
    /// WebApi基类
    /// </summary>
    public class BaseApiController : ControllerBase
    {
        private IMemoryCache _memoryCache;
        private ICurrentUserInfo _currentUserInfo;
        public BaseApiController(IMemoryCache memoryCache, ICurrentUserInfo currentUserInfo)
        {
            this._memoryCache = memoryCache;
            this._currentUserInfo = currentUserInfo;
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

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        protected SysUserOutputDto GetUserInfo()
        {
            var user = _currentUserInfo.CurrentUser;

            string key = HttpContext.Request.Headers["Authorization"].SingleOrDefault();
            SysUserOutputDto sysUser = null;
            if (key != null && user.Name != null)
            {
                sysUser = this._memoryCache.Get(key.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1]) as SysUserOutputDto;
            }
            return sysUser;
        }

        /// <summary>
        /// 退出
        /// </summary>
        protected void CleanUserInfo()
        {
            string key = HttpContext.Request.Headers["token"].SingleOrDefault();
            if (key != null)
            {
                 this._memoryCache.Remove(key);
            }
        }
    }
}
