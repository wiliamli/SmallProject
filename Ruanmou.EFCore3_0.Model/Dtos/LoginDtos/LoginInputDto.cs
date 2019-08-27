using Abp.AutoMapper;
using YJ.PlatFormCore.EntityFrameworkCore.Entities;

namespace Aio.Domain.SystemManage.Dtos
{
    [AutoMap(typeof(Users))]
    public class LoginInputDto
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 系统ID
        /// </summary>
        public string SystemID { get; set; }
    }
}
