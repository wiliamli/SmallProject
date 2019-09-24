using System.Threading.Tasks;
using Ruanmou04.EFCore.Model.DtoHelper;

namespace Ruanmou04.NetCore.Interface.Tokens
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
