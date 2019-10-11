using Ruanmou04.EFCore.Dtos.DtoHelper;
using Ruanmou04.NetCore.Dtos.SystemManager.LoginDtos;
using Ruanmou04.NetCore.Interface.SystemManager.Applications;
using Ruanmou04.NetCore.Interface.SystemManager.Service;
using Ruanmou04.NetCore.Interface.Token.Applications;   
using System.Threading.Tasks;

namespace Ruanmou04.NetCore.Application.SystemManager
{
    public class LoginApplication : ILoginApplication
    {

        private readonly ILoginService _loginService;
        private readonly ITokenApplication _tokenService;
        public LoginApplication(ILoginService loginService, ITokenApplication tokenService)
        {
            _loginService = loginService;
            _tokenService = tokenService;
        }
        public LoginApplication()
        {
            _loginService = null;
        }
        public Task<AjaxResult> ConfirmVerificationAsync(string token)
        {
            return _tokenService.ConfirmVerificationAsync(token);
        }

        public AjaxResult Login(LoginInputDto loginInput)
        {
            return _loginService.Login(loginInput);
        }



    }
}
