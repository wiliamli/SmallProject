using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Ruanmou.Core.Utility;
using System.Threading;
using Ruanmou04.NetCore.Interface.Tokens;
using Ruanmou04.EFCore.Dtos.DtoHelper;

namespace Ruanmou04.NetCore.Service.Core.Authorization.Tokens
{
    /// <summary>
    /// 令牌实现
    /// </summary>
    public class TokenConfirmService : ITokenConfirmService
    {
        JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="tokenInformationRepository"></param>
        /// <param name="tokenInformationDetailRepository"></param>
        /// <param name="configurationUserSystemsRepository"></param>
        public TokenConfirmService(
             JwtSecurityTokenHandler jwtSecurityTokenHandler
             )
        {
            _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
        }


        /// <summary>
        /// 验证token
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>AjaxResult</returns>
        public async Task<AjaxResult> ConfirmVerificationAsync(string token)
        {
            AjaxResult result = new AjaxResult("");

            token = token.Replace("Bearer ", "");
            var jwtSecurityToken = _jwtSecurityTokenHandler.ReadJwtToken(token);

            if (jwtSecurityToken.Claims.Any())
            {
                ClaimsIdentity identity = new ClaimsIdentity(Ruanmou.Core.Utility.StaticConstraint.AuthenticationScheme);
                identity.AddClaims(jwtSecurityToken.Claims);
                Thread.CurrentPrincipal = new ClaimsPrincipal(identity);
            }

            if (jwtSecurityToken == null) //没有令牌 
            {
                result.msg = "token已失效，请重新登录";
            }
            else
            {
                if (jwtSecurityToken.ValidTo.Add(StaticConstraint.Expiration) < DateTime.Now) //已过期 
                {
                    result.msg = "token已过期，请重新登录";
                }
                else
                {
                    result.msg = "token验证成功";
                    result.success = true;
                }
            }

            return result;
        }
    }
}
