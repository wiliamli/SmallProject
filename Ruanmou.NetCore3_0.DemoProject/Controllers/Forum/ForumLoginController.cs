using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RM04.DBEntity;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.NetCore.Project.Models;

namespace Ruanmou04.NetCore.Project.Controllers.Forum
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ForumLoginController : BaseApiController
    {
        private IMemoryCache memoryCache;
        private ICurrentUserInfo currentUserInfo;

        public ForumLoginController(IMemoryCache memoryCache, ICurrentUserInfo currentUserInfo) : base(memoryCache, currentUserInfo)
        {
        }

        [HttpGet]
        public StandardJsonResult<SysUserOutputDto> GetLoginInfo()
        {
            return StandardAction(()=> base.GetUserInfo());
        }

        [HttpGet]
        public StandardJsonResult Logout()
        {
            return StandardAction(() => base.CleanUserInfo());
        }
    }
}
