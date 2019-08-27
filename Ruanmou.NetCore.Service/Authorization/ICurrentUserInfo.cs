using Abp.Domain.Services;
using Microsoft.AspNetCore.Http;
using RM04.DBEntity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;


namespace Ruanmou04.NetCore.Service.Authorization
{
    /// 功能描述    ：CurrentUserInfo  
    /// 创 建 者    ：magic
    /// 创建日期    ：2019/8/27 12:04:33 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2019/8/27 12:04:33 
    public interface ICurrentUserInfo 
    {
        SysUser CurrentUser { get;  }

    }
}