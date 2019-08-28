using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Aio.Domain.SystemManage.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RM04.DBEntity;
using Ruanmou.NetCore.Interface;
using Ruanmou.NetCore.Service;
using Ruanmou.NetCore3_0.DemoProject.Utility;
using Ruanmou04.Core.Utility.DtoUtilities;
using Ruanmou04.EFCore.Model.DtoHelper;
using Ruanmou04.NetCore.Service.Core.Authorization.Tokens;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Ruanmou.NetCore3_0.DemoProject.Controllers
{



    [Route("api/[controller]/[action]"), ApiController]
    public class LoginController : ControllerBase
    {
        #region MyRegion
        private ILoggerFactory _Factory = null;
        private ILogger<UsersController> _logger = null;
        private ISysUserService _IUserService = null;
        private ILoginService _ILoginService = null;
        private ITokenService _tokenService;

        public LoginController(ILoggerFactory factory,
            ILogger<UsersController> logger,
            ISysUserService userService
            ,            ILoginService loginService
            , ITokenService tokenService
            )
        {
            this._Factory = factory;
            this._logger = logger;
            this._IUserService = userService;
            this._ILoginService = loginService;
            this._tokenService = tokenService;
        }
        #endregion
        [HttpPostAttribute]
        public async Task< AjaxResult> LoginSystemManager(LoginInputDto loginInput)
        {
            var ajax = _ILoginService.Login(loginInput);
            if (ajax.success)
            {

                var sysuserdto =  ajax.data as SysUserDto;
                var generatedto = DataMapping<SysUserDto, Ruanmou04.NetCore.Service.Core.Tokens.Dtos.GenerateTokenDto>.Trans(sysuserdto);
                ajax= await _tokenService.GenerateTokenAsync(generatedto);
            }


            return ajax;
        }
    }
}