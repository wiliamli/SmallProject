using System.Threading.Tasks;
using System.Web.Http;
using Aio.Domain.SystemManage.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RM04.DBEntity;
using Ruanmou.NetCore.Interface;
using Ruanmou.NetCore.Service;
using Ruanmou04.Core.Model.DtoHelper;
using Ruanmou04.EFCore.Model.DtoHelper;
using Ruanmou04.NetCore.Service.Core.Authorization.Tokens;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
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

                var sysuserdto =  ajax.data as SysUserOutputDto;
                var generatedto = DataMapping<SysUserOutputDto, Ruanmou04.NetCore.Service.Core.Tokens.Dtos.GenerateTokenDto>.Trans(sysuserdto);
                ajax= await _tokenService.GenerateTokenAsync(generatedto);
            }


            return ajax;
        }
    }
}