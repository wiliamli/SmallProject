using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RM04.DBEntity;
using Ruanmou04.NetCore.Project.Controllers;
using Ruanmou04.NetCore.Project.Models;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Ruanmou.NetCore3_0.DemoProject.Controllers
{

    [Route("api/[controller]/[action]")]
    [ServiceFilter(typeof(VerifyAttribute))]
    [ApiController]
    public class PortalUserController : BaseApiController
    {
        public PortalUserController(IMemoryCache memoryCache) : base(memoryCache)
        {

        }

        [HttpGet()]
        public SysUserOutputDto GetUser()
        {
            SysUserOutputDto sysUser = base.GetUserInfo();
            return sysUser;
        }

         

    }
}