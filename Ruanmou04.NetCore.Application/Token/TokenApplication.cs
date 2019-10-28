using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Ruanmou04.NetCore.Service.Core.Tokens.Dtos;
using Ruanmou04.EFCore.Model.Token.Dtos;

using System.IdentityModel.Tokens.Jwt;
using Ruanmou.Core.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading;
using Ruanmou04.EFCore.Dtos.DtoHelper;
using Ruanmou04.NetCore.Interface.SystemManager.Service;
using Ruanmou04.NetCore.Interface.Token.Applications;
using Ruanmou04.Core.Utility.Extensions;

namespace Ruanmou04.NetCore.Application.Token
{
    /// <summary>
    /// 令牌实现
    /// </summary>
    public class TokenApplication : ITokenApplication
    {

        ISysUserService _usersRepository;
        //TokenAuthConfiguration _configuration;
        IAuthenticationService _authenticationService;
        IHttpContextAccessor _httpContextAccessor;
        JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="tokenInformationRepository"></param>
        /// <param name="tokenInformationDetailRepository"></param>
        /// <param name="configurationUserSystemsRepository"></param>
        public TokenApplication(
             //TokenAuthConfiguration configuration,
             IAuthenticationService authenticationService,
             IHttpContextAccessor httpContextAccessor,
             JwtSecurityTokenHandler jwtSecurityTokenHandler,
             ISysUserService usersRepository
             )
        {
            _authenticationService = authenticationService;
            _httpContextAccessor = httpContextAccessor;
            _usersRepository = usersRepository;
            _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
        }

        /// <summary>
        /// 生成令牌 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="systemID"></param>
        /// <returns>string</returns>
        public async Task<AjaxResult> GenerateTokenAsync(GenerateTokenDto generateDto)
        {
            AjaxResult result = new AjaxResult();

            TokenInformation tokenInfoMation = null;
            generateDto.TokenExpiration = StaticConstraint.Expiration;
            tokenInfoMation = await CreateTokenDataAsync(generateDto, tokenInfoMation);
            result.data = tokenInfoMation.Token;

            result.success = true;
            return result;
        }

        /// <summary>
        /// 验证token
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>AjaxResult</returns>
        public async Task<AjaxResult> ConfirmVerificationAsync(string token)
        {
            AjaxResult result = new AjaxResult();

            token = token.Replace("Bearer ", string.Empty);
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

        public AjaxResult ConfirmVerification(string token)
        {
            AjaxResult result = new AjaxResult();
            token = token.Replace("Bearer ", string.Empty);
            if (token == "null" || token.IsNullOrWhiteSpace())
            {
                result.msg = "token已失效，请重新登录";
            }
            else
            {
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
            }
            return result;
        }

        public ClaimsPrincipal GetClaims(string token)
        {
            token = token.Replace("Bearer ", string.Empty);
            
            ClaimsPrincipal claimsPrincipal = null;
            if (token != "null")
            {
                var jwtSecurityToken = _jwtSecurityTokenHandler.ReadJwtToken(token);

                if (jwtSecurityToken.Claims.Any())
                {
                    ClaimsIdentity identity = new ClaimsIdentity(Ruanmou.Core.Utility.StaticConstraint.AuthenticationScheme);
                    identity.AddClaims(jwtSecurityToken.Claims);
                    claimsPrincipal = new ClaimsPrincipal(identity);
                }
            }
            return claimsPrincipal;
        }


        /// <summary>
        /// 创建系统与令牌关联数据 TokenInformationDetail
        /// </summary>
        /// <param name="tokenID"></param>
        /// <param name="systemID"></param>
        /// <returns></returns>
        private TokenInformationDetail CreateTokenDetailData(Guid tokenID, string systemID)
        {
            TokenInformationDetail tokenDetail = null;

            tokenDetail = new TokenInformationDetail()
            {
                Id = Guid.NewGuid(),
                SystemID = systemID,
                TokenInformationID = tokenID
            };
            //tokenDetail = tokenInformationDetailRepository.Insert(tokenDetail);

            return tokenDetail;
        }

        /// <summary>
        /// 新建token数据
        /// </summary>
        /// <param name="account"></param>
        /// <param name="systemID"></param>
        /// <returns></returns>
        private async Task<TokenInformation> CreateTokenDataAsync(GenerateTokenDto generateDto, TokenInformation tokenInformation)
        {
            //TokenInformation token = null;

            if (tokenInformation == null)
            {
                tokenInformation = new TokenInformation()
                {
                    Id = Guid.NewGuid(),
                    Account = generateDto.Account,
                    Token = await CreateTokenAsync(generateDto, null),
                    IsEffective = 0,//正常
                    FailureTime = DateTime.Now.Add(generateDto.TokenExpiration)
                };
            }
            else
            {
                await CreateTokenAsync(generateDto, tokenInformation);
            }

            return tokenInformation;
        }

        /// <summary>
        /// 根据账号和userid生成token
        /// </summary>
        /// <param name="account"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        private async Task<string> CreateTokenAsync(GenerateTokenDto generateDto, TokenInformation tokenInformation)
        {
            var accessToken = "";
            long longUserId = 1;

            //generateDto.Id.ToList().ForEach(x => { if (x != 0) longUserId = longUserId * x; });
            ClaimsIdentity identity = new ClaimsIdentity(Ruanmou.Core.Utility.StaticConstraint.AuthenticationScheme);//
            identity.AddClaim(new Claim(ClaimTypes.Name, generateDto.Name.ToString()));
            //失效时间
            identity.AddClaim(new Claim("FaliureTime", DateTime.Now.Add(generateDto.TokenExpiration).ToString()));
            identity.AddClaim(new Claim(ClaimTypes.PrimarySid, generateDto.Id.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Sid, longUserId.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, generateDto.Id.ToString()));

            TimeSpan tokenExpiration = generateDto.TokenExpiration;
            if (tokenInformation == null)
            {
                //tokenExpiration = TimeSpan.FromHours(10);
                accessToken = WriteAccessToken(CreateJwtClaims(identity), tokenExpiration);
            }
            var props = new AuthenticationProperties()
            {
                IssuedUtc = DateTime.Now,
                ExpiresUtc = DateTime.Now.Add(tokenExpiration),
                IsPersistent = true
            };
            var claims = new ClaimsPrincipal(identity);
            _httpContextAccessor.HttpContext.User = claims;

            await _authenticationService.SignInAsync(_httpContextAccessor.HttpContext, CookieAuthenticationDefaults.AuthenticationScheme, claims, props);
            return accessToken;
        }

        /// <summary>
        /// 根据身份写入令牌
        /// </summary>
        /// <param name="account"></param>
        /// <returns>string</returns>
        private string WriteAccessToken(IEnumerable<Claim> claims, TimeSpan expiration)
        {
            var now = DateTime.Now;

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: StaticConstraint.Issuer,
                audience: StaticConstraint.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(expiration),
                signingCredentials: StaticConstraint.SigningCredentials
            );

            return _jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
        }

        /// <summary>
        /// 创建身份
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        private List<Claim> CreateJwtClaims(ClaimsIdentity identity)
        {
            var claims = identity.Claims.ToList();
            var nameIdClaim = claims.First(c => c.Type == ClaimTypes.NameIdentifier);

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, nameIdClaim.Value),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            });
            return claims;
        }

        /// <summary>
        /// 注销token 
        /// 1. 删除数据库token数据  2.删除写入的token数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<AjaxResult> LoginOutTokenAsync(LoginOutTokenDto loginOutTokenDto)
        {
            AjaxResult result = new AjaxResult("");
            
            return result;
        }

    }
}
