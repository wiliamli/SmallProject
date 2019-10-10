using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using Ruanmou04.NetCore.Dtos.SystemManager.RoleDtos;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos;
using Ruanmou04.NetCore.Interface;
using Ruanmou04.NetCore.Interface.SystemManager.Applications;
using Ruanmou04.NetCore.Interface.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;

namespace Ruanmou04.NetCore.Service
{
    public class CurrentUserInfo: ICurrentUserInfo
    {
        IHttpContextAccessor contextAccessor;
        ITokenService tokenService;
        IMemoryCache memoryCache;
        ISysRoleApplication sysRoleApplication;

        private const string TOKEN = "token";
        private const string AUTHORIZATION = "Authorization";

        public CurrentUserInfo(IHttpContextAccessor contextAccessor, ITokenService tokenService, 
            ISysRoleApplication sysRoleApplication, IMemoryCache memoryCache)
        {
            this.contextAccessor = contextAccessor;
            this.tokenService = tokenService;
            this.sysRoleApplication = sysRoleApplication;
            this.memoryCache = memoryCache;
        }

        /// <summary>
        /// 获取前台用户信息
        /// </summary>
        public CurrentUser CurrentUser
        {
            get
            {
               return GetCurrentUser(TOKEN);
            }
        }

        /// <summary>
        /// 获取后台用户信息
        /// </summary>
        public CurrentUser SysCurrentUser
        {
            get
            {
                return GetCurrentUser(AUTHORIZATION);
            }
        }

        private CurrentUser GetCurrentUser(string headerKey)
        {
            var identity = contextAccessor.HttpContext.User;

            CurrentUser users = null;

            if (contextAccessor.HttpContext.Request.Headers.ContainsKey(headerKey))
            {
                users = new CurrentUser();
                contextAccessor.HttpContext.Request.Headers.TryGetValue(headerKey, out StringValues val);
                identity = tokenService.GetClaims(val);
                users.Id = Convert.ToInt32(identity.FindFirst(ClaimTypes.PrimarySid).Value);
                users.Name = identity.FindFirst(ClaimTypes.Name).Value;
                var curRoles = GetRoleCache(val);
                if (curRoles != null && curRoles.Count() > 0)
                {
                    users.SysRoles = curRoles;
                }
                else
                {
                    curRoles = this.sysRoleApplication.GetUserRoles(users.Id);
                    System.Threading.Tasks.Task.Run(() => SetRoleCache(val, curRoles));
                    users.SysRoles = curRoles;
                }
            }
            return users;
        }


        private IEnumerable<SysRoleDto> GetRoleCache(string key)
        {
            this.memoryCache.TryGetValue(key, out IEnumerable<SysRoleDto> roleDtos);
            return roleDtos;
        }

        private void SetRoleCache(string key, IEnumerable<SysRoleDto> roleDtos)
        {
            this.memoryCache.Set(key, roleDtos);
        }
        
    }
}
