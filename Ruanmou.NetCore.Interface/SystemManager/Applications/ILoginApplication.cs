using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.EFCore.Dtos.DtoHelper;
using Ruanmou04.NetCore.Dtos.SystemManager.LoginDtos;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos;
using Ruanmou04.NetCore.Interface;
using Ruanmou04.NetCore.Service.Core.Tokens.Dtos;
using System.Threading.Tasks;

namespace Ruanmou04.NetCore.Interface.SystemManager.Applications
{
    public interface ILoginApplication : IApplication
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginInput"></param>
        /// <returns></returns>

        Task<StandardJsonResult<GenerateTokenDto>> Login(LoginInputDto loginInput);

        /// <summary>
        /// 验证token
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>AjaxResult</returns>
        Task<AjaxResult> ConfirmVerificationAsync(string token);

    }
}
