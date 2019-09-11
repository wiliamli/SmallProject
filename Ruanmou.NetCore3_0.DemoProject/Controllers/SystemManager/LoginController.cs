using System.Threading.Tasks;
using Aio.Domain.SystemManage.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using RM04.DBEntity;
using Ruanmou.NetCore.Application;
using Ruanmou.NetCore.Interface;
using Ruanmou.NetCore.Service;
using Ruanmou04.Core.Model.DtoHelper;
using Ruanmou04.EFCore.Model.DtoHelper;
using Ruanmou04.NetCore.Service.Core.Authorization.Tokens;
using Ruanmou04.NetCore.Service.Core.Tokens.Dtos;
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
        private ILoginApplication _loginApplication = null;
        private ITokenService _tokenService;
        private IMemoryCache _memoryCache;
        private ISysRoleApplication _sysRoleApplication;


        public LoginController(ILoggerFactory factory,
            ILogger<UsersController> logger,
            ISysUserService userService
            , ILoginApplication loginApplication
            , ITokenService tokenService
            , IMemoryCache memoryCache
            , ISysRoleApplication sysRoleApplication
            )
        {
            this._Factory = factory;
            this._logger = logger;
            this._IUserService = userService;
            this._loginApplication = loginApplication;
            this._tokenService = tokenService;
            this._memoryCache = memoryCache;
            this._sysRoleApplication = sysRoleApplication;
        }
        #endregion
        [HttpPostAttribute]
        public async Task< AjaxResult> LoginSystemManager(LoginInputDto loginInput)
        {
            var ajax = _loginApplication.Login(loginInput);
            if (ajax.success)
            {

                var sysuserdto =  ajax.data as SysUserOutputDto;
                //添加session,sessionId不同，cookie没跨域
                //HttpContext.Session.Set("userInfo",
                //    System.Text.Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(sysuserdto)));

                var curRoles =this._sysRoleApplication.GetUserRoles(sysuserdto.Id);
                if (curRoles != null)
                {
                    sysuserdto.SysRoles = curRoles;
                }

                //var generatedto =
                var generatedto= sysuserdto.MapTo<SysUserOutputDto, GenerateTokenDto>();// sys DataMapping<SysUserOutputDto, Ruanmou04.NetCore.Service.Core.Tokens.Dtos.GenerateTokenDto>.Trans(sysuserdto);
                ajax = await _tokenService.GenerateTokenAsync(generatedto);
                this._memoryCache.Set<SysUserOutputDto>(ajax.data, sysuserdto);
            }


            return ajax;
        }

        [HttpGet]
        public async Task<AjaxResult> TokenConfirm(string token)
        {
            var ajax =await _tokenService.ConfirmVerificationAsync(token);
          
            return ajax;
        }
    }
}