using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ruanmou.NetCore3_0.DemoProject.Models;
using Ruanmou04.EFCore.Model.DtoHelper;
using Ruanmou04.NetCore.Project.Models;
using Ruanmou04.NetCore.Project.Utility;

namespace Ruanmou.NetCore3_0.DemoProject.Controllers
{
    [Route("api/[controller]/[action]"), ApiController]
    public class MenuController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICurrentUserInfo _currentUserInfo;

        public MenuController(ILogger<HomeController> logger, ICurrentUserInfo currentUserInfo)
        {
            _logger = logger;
            _currentUserInfo = currentUserInfo;
        }
        [HttpGet]
        public AjaxResult GetMenuList()
        {

            return new AjaxResult { data= _currentUserInfo.CurrentUser.Name};
        }
[HttpGet]
        public AjaxResult GetNavigationBarList()
        {

            return new AjaxResult { data= _currentUserInfo.CurrentUser.Name};
        }
        
    }
}
