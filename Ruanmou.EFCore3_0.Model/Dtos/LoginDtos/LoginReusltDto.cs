
using RM04.DBEntity;

namespace YRuanmou04.NetCore.Project.Dtos
{ 
    public class LoginReusltDto
    {

        public LoginStatus LoginStatus { get; set; }

        public string Account { get; set; }

        public UsersListDto UsersData { get; set; }
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
