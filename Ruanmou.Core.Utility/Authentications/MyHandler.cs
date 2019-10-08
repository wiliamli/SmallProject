using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.Core.Utility.Authentications
{
    /// <summary>
    /// 自定义的handler
    /// 通常会提供一个统一的认证中心，负责证书的颁发及销毁（登入和登出），而其它服务只用来验证证书，并用不到SingIn/SingOut。
    /// </summary>
    public class MyHandler : IAuthenticationHandler, IAuthenticationSignInHandler, IAuthenticationSignOutHandler
    {
        public AuthenticationScheme Scheme { get; private set; }
        protected HttpContext Context { get; private set; }

        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            Scheme = scheme;
            Context = context;
            return Task.CompletedTask;
        }

        public async Task<AuthenticateResult> AuthenticateAsync()
        {
            var cookie = Context.Request.Cookies["myCookie"];
            if (string.IsNullOrEmpty(cookie))
            {
                return AuthenticateResult.NoResult();
            }
            return AuthenticateResult.Success(this.Deserialize(cookie));
        }

        public Task ChallengeAsync(AuthenticationProperties properties)
        {
            Context.Response.Redirect("/login");
            return Task.CompletedTask;
        }

        public Task ForbidAsync(AuthenticationProperties properties)
        {
            Context.Response.StatusCode = 403;
            return Task.CompletedTask;
        }

        public Task SignInAsync(ClaimsPrincipal user, AuthenticationProperties properties)
        {
            var ticket = new AuthenticationTicket(user, properties, Scheme.Name);
            Context.Response.Cookies.Append("myCookie", this.Serialize(ticket));
            return Task.CompletedTask;
        }

        public Task SignOutAsync(AuthenticationProperties properties)
        {
            Context.Response.Cookies.Delete("myCookie");
            return Task.CompletedTask;
        }
        private AuthenticationTicket Deserialize(string content)
        {
            byte[] byteTicket = System.Text.Encoding.Default.GetBytes(content);
            return TicketSerializer.Default.Deserialize(byteTicket);
        }

        private string Serialize(AuthenticationTicket ticket)
        {
            byte[] byteTicket = TicketSerializer.Default.Serialize(ticket);
            return Encoding.Default.GetString(byteTicket);
        }
    }

    public class TicketDataFormat : SecureDataFormat<AuthenticationTicket>// IDataSerializer<AuthenticationTicket>//
    {
        public TicketDataFormat(IDataProtector protector)
            : base(TicketSerializer.Default, protector)
        {
        }
    }

}
