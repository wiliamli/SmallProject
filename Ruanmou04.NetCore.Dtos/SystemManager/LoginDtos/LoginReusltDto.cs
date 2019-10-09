


using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos;

namespace Ruanmou04.NetCore.Dtos.SystemManager.LoginDtos
{ 
    public class LoginReusltDto
    {

        public LoginStatus LoginStatus { get; set; }

        public string Account { get; set; }

        public CurrentUser UsersData { get; set; }
    }


    /// <summary>
    /// 登录状态枚举
    /// </summary>
    public enum LoginStatus
    {
        LoginSuccess = 0, //登录成功
        LoginError = 1, //登录失败
    }
}
