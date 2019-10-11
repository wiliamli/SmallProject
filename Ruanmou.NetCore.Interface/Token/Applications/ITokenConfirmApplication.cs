using System.Threading.Tasks;
using Ruanmou04.EFCore.Dtos.DtoHelper;

namespace Ruanmou04.NetCore.Interface.Token.Applications
{
    /// <summary>
    /// 令牌接口
    /// </summary>
    public interface ITokenConfirmApplication : IApplication
    {
        /// <summary>
        /// 验证token
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>AjaxResult</returns>
        Task<AjaxResult> ConfirmVerificationAsync(string token);

        

    }
}
