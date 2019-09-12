using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ruanmou.NetCore3_0.DemoProject.Models;
using Ruanmou04.EFCore.Model.DtoHelper;
using Ruanmou04.NetCore.Interface.SystemManager.Service;
using Ruanmou04.NetCore.Project.Models;
using Ruanmou04.NetCore.Project.Utility;

namespace Ruanmou.NetCore3_0.DemoProject.Controllers
{
    [CustomAuthorize]
    [Route("api/[controller]/[action]"), ApiController]
    public class MenuController : ControllerBase
    {
        private readonly ICurrentUserInfo _currentUserInfo;
        private readonly IUserMenuService _userMenuService;

        public MenuController( ICurrentUserInfo currentUserInfo, IUserMenuService userMenuService)
        {
            _currentUserInfo = currentUserInfo;
            _userMenuService = userMenuService;
        }
        /// <summary>
        /// 获取权限菜单 
        /// </summary>
        /// <param name="menuType">1后台的，2是网站，3是论坛</param>
        /// <returns></returns>
        [HttpPost]
        public AjaxResult GetMenuList()
        {
            var menu = _userMenuService.GetAuthorityMenuList(_currentUserInfo.CurrentUser.Id);
            return new AjaxResult { success=true, data = menu };
        }
        [HttpGet]
        public AjaxResult GetNavigationBarList()
        {

            return new AjaxResult { data = _currentUserInfo.CurrentUser.Name };
        }

    }
}
