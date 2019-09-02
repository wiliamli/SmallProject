using Abp.AutoMapper;
using RM04.DBEntity;

namespace Aio.Domain.SystemManage.Dtos
{
    public class LoginInputDto:BaseEntity
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
