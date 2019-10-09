using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Ruanmou04.Core.Dtos.DtoHelper;
using Ruanmou04.NetCore.Dtos.SystemManager.LoginDtos;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos;
using Ruanmou04.NetCore.Interface.SystemManager.Applications;
using Ruanmou04.NetCore.Interface.SystemManager.Service;
using Ruanmou04.NetCore.Interface.Tokens;
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
        private ISysUserService _IUserService = null;
        private ILoginApplication _loginApplication = null;
        private ITokenService _tokenService;


        public LoginController(ILoggerFactory factory,
            ISysUserService userService
            , ILoginApplication loginApplication
            , ITokenService tokenService
            )
        {
            this._Factory = factory;
            this._IUserService = userService;
            this._loginApplication = loginApplication;
            this._tokenService = tokenService;
        }
        #endregion
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginInput"></param>
        /// <returns></returns>
        [HttpPostAttribute]
        public async Task<string> LoginSystemManager(LoginInputDto loginInput)
        {
            var ajax = _loginApplication.Login(loginInput);
            if (ajax.success)
            {
                var sysuserdto =  ajax.data as CurrentUser;
                var generatedto= DataMapping<CurrentUser,GenerateTokenDto>.Trans(sysuserdto);
                ajax = await _tokenService.GenerateTokenAsync(generatedto);
                generatedto.Token = ajax.data.ToString();
                //var curRoles = this._sysRoleApplication.GetUserRoles(sysuserdto.Id);
                //if (curRoles != null)
                //{
                //    sysuserdto.SysRoles = curRoles;
                //}
                ajax.data = generatedto;                
                //this._memoryCache.Set<CurrentUser>($"Bearer {generatedto.Token}", sysuserdto);
            }
            return JsonConvert.SerializeObject( ajax);
        }

        //[HttpGet]
        //public async Task<AjaxResult> TokenConfirm(string token)
        //{
        //    var ajax =await _tokenService.ConfirmVerificationAsync(token);
        //    return ajax;
        //}
    }
}