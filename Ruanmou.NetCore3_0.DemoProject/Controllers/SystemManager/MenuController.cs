using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Ruanmou04.Core.Dtos.DtoHelper;
using Ruanmou04.Core.Utility;
using Ruanmou04.Core.Utility.DtoUtilities;
using Ruanmou04.Core.Utility.Extensions;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.EFCore.Dtos.DtoHelper;
using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.AOP.Filter;
using Ruanmou04.NetCore.Dtos.SystemManager.MenuDtos;
using Ruanmou04.NetCore.Dtos.SystemManager.MenuDtos.Input;
using Ruanmou04.NetCore.Dtos.SystemManager.MenuDtos.Output;
using Ruanmou04.NetCore.Interface;
using Ruanmou04.NetCore.Interface.SystemManager.Applications;
using Ruanmou04.NetCore.Interface.SystemManager.Service;
using Ruanmou04.NetCore.Project.Controllers;

namespace Ruanmou.NetCore3_0.DemoProject.Controllers
{
    /// <summary>
    /// 菜单管理
    /// </summary>
    [ServiceFilter(typeof(SysVerifyAttribute))]
    [Route("api/[controller]/[action]"), ApiController]
    public class MenuController : BaseApiController
    {
        private readonly ICurrentUserInfo _currentUserInfo; 
        private readonly ISysMenuApplication _sysMenuApplication;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="currentUserInfo"></param>
        /// <param name="sysMenuApplication"></param>
        public MenuController(ICurrentUserInfo currentUserInfo,
           ISysMenuApplication sysMenuApplication) : base(currentUserInfo)
        {
            _currentUserInfo = currentUserInfo;

            _sysMenuApplication = sysMenuApplication;
        }

        /// <summary>
        /// 获取权限菜单 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public StandardJsonResult<List<SysMenuTreeDto>> GetMenuList()
        {
            return StandardAction(() => _sysMenuApplication.GetAuthorityMenuList(_currentUserInfo.SysCurrentUser.Id));
        }

        /// <summary>
        /// 获取菜单信息
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        [HttpGet]
        public StandardJsonResult GetEditMenuByID(int menuId)
        {
            return StandardAction(() => _sysMenuApplication.GetMenuDetailById(menuId));
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public StandardJsonResult RemoveMenuById(int id)
        {
            return StandardAction(() => _sysMenuApplication.DeleteMenuById(id));
        }

        /// <summary>
        /// 获取所有菜单数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public StandardJsonResult<List<SysMenuDto>> GetRoleMenu()
        {
            return StandardAction(() => _sysMenuApplication.GetMenus());
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public StandardJsonResult<PagedResult<SysMenuListOutputDto>> GetMenus(int page, int limit, string name)
        {
            var param = new SysMenuListInputDto()
            {
                Name = name,
                PageIndex = page,
                PageSize = limit
            };
            return StandardAction(() => _sysMenuApplication.GetPagedResult(param));
        }

        /// <summary>
        /// 新增或修改数据
        /// </summary>
        /// <param name="sysMenuDto"></param>
        /// <returns></returns>
        [HttpPost]
        public StandardJsonResult SaveData([FromBody]SysMenuDto sysMenuDto)
        {
            if (sysMenuDto == null)
            {
                return StandardJsonResult.GetFailureResult("参数有误");
            }
            if (sysMenuDto.Id > 0)
            {
                var sysUserEditInputDto = DataMapping<SysMenuDto, SysMenuEditInputDto>.Trans(sysMenuDto); 
                sysUserEditInputDto.LastModifyTime = DateTime.Now;
                sysUserEditInputDto.LastModifierId = _currentUserInfo.SysCurrentUser.Id;
                return StandardAction(() => _sysMenuApplication.EditMenu(sysUserEditInputDto));
            }
            else
            {
                var sysMenuAddInputDto = DataMapping<SysMenuDto, SysMenuAddInputDto>.Trans(sysMenuDto);
                sysMenuAddInputDto.CreatorId = _currentUserInfo.SysCurrentUser.Id;
                sysMenuAddInputDto.CreateTime = DateTime.Now;
                sysMenuAddInputDto.LastModifierId = sysMenuAddInputDto.CreatorId;
                sysMenuAddInputDto.LastModifyTime = DateTime.Now;
                sysMenuAddInputDto.ParentId = 0;
                sysMenuAddInputDto.MenuType = 1;
                return StandardAction(() => _sysMenuApplication.AddMenu(sysMenuAddInputDto));
            }
        }
    }
}
