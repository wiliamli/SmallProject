using Abp.Application.Services;
using System.Threading.Tasks;
using Ruanmou04.NetCore.Service.Core.Tokens.Dtos;
using Ruanmou04.EFCore.Model.DtoHelper;
using Ruanmou04.NetCore.Interface;

namespace Ruanmou04.NetCore.Service.Core.Authorization.Tokens
{
    /// <summary>
    /// 令牌接口
    /// </summary>
    public interface ITokenConfirmService : IApplication
    {
        /// <summary>
        /// 验证token
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>AjaxResult</returns>
        Task<AjaxResult> ConfirmVerificationAsync(string token);

        

    }
}
