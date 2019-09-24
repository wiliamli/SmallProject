using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou.Core.Utility.Filters
{
    /// <summary>
    /// 跟以前一样，可以支持匿名的
    /// </summary>
    public class CustomAllowAnonymousAttribute : Attribute, IAllowAnonymous
    {
    }
}
