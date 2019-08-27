using Microsoft.AspNetCore.Http;
using RM04.DBEntity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;


namespace Ruanmou04.NetCore.Service.Authorization
{
    /// 功能描述    ：CurrentUserInfo  
    /// 创 建 者    ：hbs
    /// 创建日期    ：2018/9/11 12:04:33 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2018/9/11 12:04:33 
    public class CurrentUserInfo: ICurrentUserInfo
    {
        IHttpContextAccessor contextAccessor;
        public CurrentUserInfo(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }
        public SysUser CurrentUser
        {
            get
            {
                var identity = contextAccessor.HttpContext.User;
                var users = new SysUser();
                if (identity.FindFirst(ClaimTypes.PrimarySid) != null)
                {
                    users.Id =Convert.ToInt32( identity.FindFirst(ClaimTypes.PrimarySid).Value);
                    users.Name = identity.FindFirst(ClaimTypes.Name).Value.ToString();
                }
                return users;
            }
        }
        
    }
}