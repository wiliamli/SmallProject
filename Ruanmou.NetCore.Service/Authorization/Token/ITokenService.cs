using Abp.Application.Services;
using System.Threading.Tasks;
using Ruanmou04.NetCore.Service.Core.DtoHelper;
using Ruanmou04.NetCore.Service.Core.Tokens.Dtos;
using Ruanmou04.NetCore.Service.EntityFrameworkCore.Entities;

namespace Ruanmou04.NetCore.Service.Core.Authorization.Tokens
{
    /// <summary>
    /// 令牌接口
    /// </summary>
    public interface ITokenService : IApplicationService
    {
        /// <summary>
        /// 验证token
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>AjaxResult</returns>
        Task<AjaxResult> ConfirmVerificationAsync(VerificationTokenDto dto);

        /// <summary>
        /// 生成令牌 并保存到数据库
        /// </summary>
        /// <param name="account"></param>
        /// <param name="systemID"></param>
        /// <returns>string</returns>
        Task<AjaxResult> GenerateTokenAsync(GenerateTokenDto generateDto);

        /// <summary>
        /// 注销登录 + 删除数据库token
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<AjaxResult> LoginOutTokenAsync(LoginOutTokenDto loginOutTokenDto);

        /// <summary>
        /// 根据token获取TokenInformation实体
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<TokenInformation> GetTokenInformationByTokenAsync(string token);
    }
}
