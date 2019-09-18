using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using RM04.DBEntity;
using Ruanmou04.NetCore.Service.Core.Authorization.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ruanmou04.NetCore.Project.Models
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
