using Microsoft.AspNetCore.Mvc;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.NetCore.Dtos.SystemManager.LoginDtos;
using Ruanmou04.NetCore.Interface.SystemManager.Applications;
using Ruanmou04.NetCore.Interface.Token.Applications;
using Ruanmou04.NetCore.Service.Core.Tokens.Dtos;
using System.Threading.Tasks;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Ruanmou.NetCore3_0.DemoProject.Controllers
{
    [Route("api/[controller]/[action]"), ApiController]
    public class LoginController : ControllerBase
    {
        #region MyRegion
        private ILoginApplication _loginApplication = null;
      
        public LoginController(ILoginApplication loginApplication, ITokenApplication tokenApplication)
        {
            this._loginApplication = loginApplication;
        }
        #endregion

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginInput"></param>
        /// <returns></returns>
        [HttpPostAttribute]
        public async Task<StandardJsonResult<GenerateTokenDto>> LoginSystemManager(LoginInputDto loginInput)
        {
            return await _loginApplication.Login(loginInput);
        } 
    }
}