
namespace Ruanmou04.NetCore.Dtos.SystemManager.LoginDtos
{
    public class LoginInputDto: BaseDto
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

    }
}
