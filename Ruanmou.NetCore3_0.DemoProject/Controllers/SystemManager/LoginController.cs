using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Ruanmou04.Core.Dtos.DtoHelper;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.NetCore.Dtos.SystemManager.LoginDtos;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos;
using Ruanmou04.NetCore.Interface.SystemManager.Applications;
using Ruanmou04.NetCore.Interface.SystemManager.Service;
using Ruanmou04.NetCore.Interface.Token.Applications;
using Ruanmou04.NetCore.Service.Core.Tokens.Dtos;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Ruanmou.NetCore3_0.DemoProject.Controllers
{
    [Route("api/[controller]/[action]"), ApiController]
    public class LoginController : ControllerBase
    {
        #region MyRegion
        private ILoginApplication _loginApplication = null;
       // private ITokenApplication _tokenApplication;


        public LoginController(ILoginApplication loginApplication, ITokenApplication tokenApplication)
        {
            this._loginApplication = loginApplication;
          //  this._tokenApplication = tokenApplication;
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
            //var loginResult = _loginApplication.Login(loginInput);
            //if (loginResult.Success)
            //{
            //    var sysUserDto = loginResult.Data;
            //    var generateDto= DataMapping<CurrentUser,GenerateTokenDto>.Trans(sysUserDto);

            //   var ajax = await _tokenApplication.GenerateTokenAsync(generateDto);

            //    generateDto.Token = ajax.data.ToString();
               
            //    ajax.data = generatedto;                
            //}
            //return JsonConvert.SerializeObject(ajax);
        } 
    }
}