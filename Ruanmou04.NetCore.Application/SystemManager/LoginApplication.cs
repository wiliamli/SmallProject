using Ruanmou04.Core.Dtos.DtoHelper;
using Ruanmou04.Core.Utility.Extensions;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.Core.Utility.Security;
using Ruanmou04.EFCore.Dtos.DtoHelper;
using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.Dtos.SystemManager.LoginDtos;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos;
using Ruanmou04.NetCore.Interface.SystemManager.Applications;
using Ruanmou04.NetCore.Interface.SystemManager.Service;
using Ruanmou04.NetCore.Interface.Token.Applications;
using Ruanmou04.NetCore.Service.Core.Tokens.Dtos;
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

        public async Task<StandardJsonResult<GenerateTokenDto>> Login(LoginInputDto loginInput)
        {
            if (loginInput == null || loginInput.Account.IsNullOrWhiteSpace())
            {
                return StandardJsonResult<GenerateTokenDto>.GetFailureResult("请求参数有误,请检查！");
            }

            var user = _loginService.Login(loginInput.Account);
            if (user == null || user.Id <= 0)
            {
                return StandardJsonResult<GenerateTokenDto>.GetFailureResult("用户名或密码不正确,请检查！");
            }
            var password = Encrypt.EncryptionPassword(loginInput.Password);
            if (user.Password != password)
            {
                return StandardJsonResult<GenerateTokenDto>.GetFailureResult("用户名或密码不正确,请检查！");
            }

            var sysUserDto = DataMapping<SysUser, CurrentUser>.Trans(user);

            var generateDto = DataMapping<CurrentUser, GenerateTokenDto>.Trans(sysUserDto);
            var generateResult = await _tokenService.GenerateTokenAsync(generateDto);
            generateDto.Token = generateResult.data.ToString();

            //ajax.data = generatedto;


            return StandardJsonResult<GenerateTokenDto>.GetSuccessResult("登录成功", generateDto);
        }
    }
}
