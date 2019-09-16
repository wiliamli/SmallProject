﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RM04.DBEntity;
using Ruanmou04.Core.Model.DtoHelper;
using Ruanmou04.Core.Utility;
using Ruanmou04.Core.Utility.Extensions;
using Ruanmou04.EFCore.Model.DtoHelper;
using Ruanmou04.NetCore.Interface.SystemManager.Service;
using Ruanmou04.NetCore.Project.Models;
using Ruanmou04.NetCore.Project.Utility;

namespace Ruanmou.NetCore3_0.DemoProject.Controllers
{
    //[CustomAuthorize]
    [Route("api/[controller]/[action]"), ApiController]
    public class ResouceController : ControllerBase
    {
        private readonly ICurrentUserInfo _currentUserInfo;
        private readonly IUserMenuService _userMenuService;

        public ResouceController(ICurrentUserInfo currentUserInfo, IUserMenuService userMenuService)
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
            return new AjaxResult { success = true, data = menu };
        }

        /// <summary>
        /// 获取编辑用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]

        public string GetEditMenuByID(int userId)
        {
            var user = _userMenuService.Find<SysMenu>(userId)?.MapTo<SysMenu, SysMenuDto>();
            return JsonConvert.SerializeObject(new AjaxResult { success = true, data = user });

        }


        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        public string GetMenus(int page, int limit, string name)
        {
            var userData = _userMenuService.
                Query<SysMenu>(u => (!name.IsNullOrEmpty() && u.Text.Contains(name)) || name.IsNullOrEmpty())
                .Select(m => new SysMenuDto
                {
                    Id = m.Id,
                    Description = m.Description,
                    MenuIcon = m.MenuIcon,
                    Text = m.Text,
                    MenuLevel = m.MenuLevel,
                    MenuType = m.MenuType,
                    Sort = m.Sort,
                    ParentId = m.ParentId,
                    Status = m.Status,
                    Url = m.Url
                }).ToList() ;

            PagedResult<SysMenuDto> pagedResult = new PagedResult<SysMenuDto> { PageIndex = page, PageSize = limit, Rows = userData, Total = userData.Count };

            return JsonConvert.SerializeObject(pagedResult);


        }

        /// <summary>
        /// 新增或修改数据
        /// </summary>
        /// <param name="sysMenuDto"></param>
        /// <returns></returns>
        [HttpPost]
        public AjaxResult SaveData([FromBody]SysMenuDto sysMenuDto)
        {

            AjaxResult ajaxResult = new AjaxResult { success = false };
            if (sysMenuDto != null)
            {
                if (sysMenuDto.Id > 0)
                {
                    var menu = _userMenuService.Find<SysMenu>(sysMenuDto.Id);
                    menu.Id = sysMenuDto.Id;
                    menu.Description = sysMenuDto.Description;
                    menu.MenuIcon = sysMenuDto.MenuIcon;
                    menu.Text = sysMenuDto.Text;
                    menu.MenuLevel = sysMenuDto.MenuLevel;
                    //menu.MenuType = sysMenuDto.MenuType;
                    menu.Sort = sysMenuDto.Sort;
                    //menu.ParentId = sysMenuDto.ParentId;
                    menu.Status = sysMenuDto.Status;
                    menu.Url = sysMenuDto.Url;

                    _userMenuService.Update<SysMenu>(menu);
                }
                else
                {
                    var menu = sysMenuDto.MapTo<SysMenuDto, SysMenu>();
                    _userMenuService.Insert<SysMenu>(menu);
                }
                ajaxResult.msg = "保存成功";
                ajaxResult.success = true;
            }
            else
                ajaxResult.msg = "保存失败";
            return ajaxResult;
        }
    }
}