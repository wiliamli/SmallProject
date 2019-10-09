using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;    
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos;
using Ruanmou04.NetCore.Interface;

namespace Ruanmou04.NetCore.Project.Controllers.Forum
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ForumLoginController : BaseApiController
    {
        private ICurrentUserInfo currentUserInfo;

        public ForumLoginController(ICurrentUserInfo currentUserInfo) : base(currentUserInfo)
        {
        }

        [HttpGet]
        public StandardJsonResult<CurrentUser> GetLoginInfo()
        {
            return StandardAction(()=> base.GetUserInfo());
        }

        //[HttpGet]
        //public StandardJsonResult Logout()
        //{
        //    return StandardAction(() => base.CleanUserInfo());
        //}
    }
}
