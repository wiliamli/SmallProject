

using Aio.Domain.SystemManage.Dtos;
using Microsoft.EntityFrameworkCore;
using RM04.DBEntity;
using Ruanmou.EFCore3_0.Model;
using Ruanmou.NetCore.Interface;
using Ruanmou04.EFCore.Model.DtoHelper;
using Ruanmou04.NetCore.Interface;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.NetCore.Application
{
    public interface ILoginApplication : IApplication
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginInput"></param>
        /// <returns></returns>

        AjaxResult Login(LoginInputDto loginInput);

        /// <summary>
        /// 验证token
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>AjaxResult</returns>
        Task<AjaxResult> ConfirmVerificationAsync(string token);

    }
}
