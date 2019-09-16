using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using RM04.DBEntity;
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
        //IMemoryCache memoryCache;
        public CurrentUserInfo(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
            //this.memoryCache = memoryCache;
        }
        public CurrentUser CurrentUser
        {
            get
            {
                var identity = contextAccessor.HttpContext.User;
                var users = new CurrentUser();
                if (identity.FindFirst(ClaimTypes.PrimarySid) != null)
                {
                    users.Id =Convert.ToInt32( identity.FindFirst(ClaimTypes.PrimarySid).Value);
                    users.Name = identity.FindFirst(ClaimTypes.Name).Value;
                    //memoryCache.Get<SysUserOutputDto>();
                    //if ()

                }
                return users;
            }
        }
    }
}
