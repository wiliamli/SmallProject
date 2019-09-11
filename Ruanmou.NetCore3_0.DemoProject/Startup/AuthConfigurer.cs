using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Abp.Runtime.Security;
using Ruanmou.Core.Utility;

namespace YJ.PlatFormCore.Web.Startup
{
    public static class AuthConfigurer
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            if (bool.Parse(configuration["Authentication:JwtBearer:IsEnabled"]))
            {
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "JwtBearer";
                    options.DefaultChallengeScheme = "JwtBearer";
                }).AddJwtBearer("JwtBearer", options =>
                {
                    options.Audience = configuration["Authentication:JwtBearer:Audience"];

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // The signing key must match!
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Authentication:JwtBearer:SecurityKey"])),

                        // Validate the JWT Issuer (iss) claim
                        ValidateIssuer = true,
                        ValidIssuer = configuration["Authentication:JwtBearer:Issuer"],

                        // Validate the JWT Audience (aud) claim
                        ValidateAudience = true,
                        ValidAudience = configuration["Authentication:JwtBearer:Audience"],

                        // Validate the token expiry
                        ValidateLifetime = true,

                        // If you want to allow a certain amount of clock drift, set that here
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = OnTokenValidate,
                        OnAuthenticationFailed = OnAuthenticationFaile,
                        OnMessageReceived = QueryStringTokenResolver,
                        OnChallenge = OnChallenge
                    };
                });
            }
        }

        private static Task OnChallenge(JwtBearerChallengeContext arg)
        {
            return Task.CompletedTask;

        }

        private static Task OnAuthenticationFaile(AuthenticationFailedContext arg)
        {
            return Task.CompletedTask;
        }

        private static Task OnTokenValidate(TokenValidatedContext arg)
        {
            //if (CurrentUserInfo.CurrentUser == null)
            //{
            //    var claims = arg.Principal.Claims;

            //    CurrentUserInfo.CurrentUser = new Users();

            //    foreach (var item in claims)
            //    {
            //        switch (item.Type)
            //        {
            //            case ClaimTypes.Name:
            //                CurrentUserInfo.CurrentUser.Name = item.Value;
            //                break;
            //            case ClaimTypes.PrimarySid:
            //                CurrentUserInfo.CurrentUser.Id = item.Value.ToGuid();
            //                break;
            //            case ClaimTypes.System:
            //                CurrentUserInfo.CurrentUser.TenantId = item.Value.ToGuid();
            //                break;
            //            case ClaimTypes.Role:
            //                CurrentUserInfo.CurrentUser.UserType = item.Value.ToGuid();
            //                break;
            //        }
            //    }
                //CurrentUserInfo.CurrentUser =new Users() { Id= Guid.Parse(claims.[ClaimTypes.PrimarySid]), Account= };
            //}
            return Task.CompletedTask;
        }


        /* This method is needed to authorize SignalR javascript client.
         * SignalR can not send authorization header. So, we are getting it from query string as an encrypted text. */
        private static Task QueryStringTokenResolver(MessageReceivedContext context)
        {
            if (!context.HttpContext.Request.Path.HasValue ||
                !context.HttpContext.Request.Path.Value.StartsWith("/signalr"))
            {
                // We are just looking for signalr clients
                return Task.CompletedTask;
            }

            var qsAuthToken = context.HttpContext.Request.Query["enc_auth_token"].FirstOrDefault();
            if (qsAuthToken == null)
            {
                // Cookie value does not matches to querystring value
                return Task.CompletedTask;
            }

            // Set auth token from cookie
            context.Token = SimpleStringCipher.Instance.Decrypt(qsAuthToken, StaticConstraint.DefaultPassPhrase);
            return Task.CompletedTask;
        }
    }
}
