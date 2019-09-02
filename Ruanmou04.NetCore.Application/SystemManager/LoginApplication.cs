

using Aio.Domain.SystemManage.Dtos;
using Microsoft.EntityFrameworkCore;
using RM04.DBEntity;
using Ruanmou.EFCore3_0.Model;
using Ruanmou.NetCore.Interface;
using Ruanmou.NetCore.Service;
using Ruanmou04.EFCore.Model.DtoHelper;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.NetCore.Application
{
    public class LoginApplication : ILoginApplication
    {

        private readonly ILoginService _loginService;
        public LoginApplication(ILoginService loginService)
        {
            _loginService = loginService;
        }
        public AjaxResult Login(LoginInputDto loginInput)
        {
            return _loginService.Login(loginInput);
        }



    }
}
