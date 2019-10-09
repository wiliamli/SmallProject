using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;       
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos;
using Ruanmou04.NetCore.Interface;      

namespace Ruanmou04.NetCore.Project.Controllers
{
    /// <summary>
    /// WebApi基类
    /// </summary>
    public class BaseApiController : ControllerBase
    {
        private ICurrentUserInfo currentUserInfo;
        public BaseApiController(ICurrentUserInfo currentUserInfo)
        {
            this.currentUserInfo = currentUserInfo;
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
        /// 获取前台用户信息
        /// </summary>
        /// <returns></returns>
        protected CurrentUser GetUserInfo()
        {
            return currentUserInfo.CurrentUser;
        }

        /// <summary>
        /// 获取后台用户信息
        /// </summary>
        /// <returns></returns>
        protected CurrentUser GetSysUserInfo()
        {
            return currentUserInfo.SysCurrentUser;
        }

        /// <summary>
        /// 退出
        /// </summary>
        //protected void CleanUserInfo()
        //{
        //    string key = HttpContext.Request.Headers["token"].SingleOrDefault();
        //    if (key != null)
        //    {
        //         this._memoryCache.Remove(key);
        //    }
        //}
    }
}
