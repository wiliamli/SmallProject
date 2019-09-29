using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos;
using Ruanmou04.NetCore.Interface;
using Ruanmou04.NetCore.Interface.Tokens;
using System;
using System.Security.Claims;

namespace Ruanmou04.NetCore.Service
{
    public class CurrentUserInfo: ICurrentUserInfo
    {
        IHttpContextAccessor contextAccessor;
        ITokenService tokenService;
        public CurrentUserInfo(IHttpContextAccessor contextAccessor, ITokenService tokenService)
        {
            this.contextAccessor = contextAccessor;
            this.tokenService = tokenService;
        }
        public CurrentUser CurrentUser
        {
            get
            {
                var identity = contextAccessor.HttpContext.User;
                var users = new CurrentUser();
                if (identity.FindFirst(ClaimTypes.PrimarySid) == null)
                {
                    if (contextAccessor.HttpContext.Request.Headers.ContainsKey("Authorization"))
                    {
                        contextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues val);
                        identity = tokenService.GetClaims(val);
                    }
                    else
                    {
                        return users;
                    }
                }
                users.Id = Convert.ToInt32(identity.FindFirst(ClaimTypes.PrimarySid).Value);
                users.Name = identity.FindFirst(ClaimTypes.Name).Value;
                return users;
            }
        }
    }
}
