using System.Collections.Generic;
using Ruanmou04.NetCore.Dtos.SystemManager.MenuDtos;
using Ruanmou04.NetCore.Interface.SystemManager.Applications;
using Ruanmou04.NetCore.Interface.SystemManager.Service;
using Ruanmou04.NetCore.Application.Extensions;
using Ruanmou04.Core.Utility.Extensions;
using System.Linq;
using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.Core.Utility.DtoUtilities;
using Ruanmou04.NetCore.Dtos.SystemManager.MenuDtos.Output;
using Ruanmou04.NetCore.Dtos.SystemManager.MenuDtos.Input;

namespace Ruanmou04.NetCore.Application.SystemManager
{
    public class SysMenuApplication : ISysMenuApplication
    {
        private IUserMenuService _userMenuService;
        private ISysRoleMenuMappingService _roleMenuMappingService;
        public SysMenuApplication(IUserMenuService userMenuService, ISysRoleMenuMappingService roleMenuMappingService)
        {
            _userMenuService = userMenuService;
            _roleMenuMappingService = roleMenuMappingService;
        }

        public void AddMenu(SysMenuAddInputDto menuDto)
        {
            var menuEntity = menuDto.ToEntity();
            if (menuEntity==null)
            {
                return;
            }
            _userMenuService.Insert(menuEntity);
        }

        public void EditMenu(SysMenuEditInputDto menuEditDto)
        {
            if (menuEditDto == null || menuEditDto.Id<=0)
            {
                return;
            }
            var sysMenuEntity = _userMenuService.Find<SysMenu>(menuEditDto.Id);
            if (sysMenuEntity == null)
            {
                return;
            }
            sysMenuEntity.Id = menuEditDto.Id;
            sysMenuEntity.Description = menuEditDto.Description;
            sysMenuEntity.MenuIcon = menuEditDto.MenuIcon;
            sysMenuEntity.Text = menuEditDto.Text;
            sysMenuEntity.MenuLevel = menuEditDto.MenuLevel; 
            sysMenuEntity.Sort = menuEditDto.Sort; 
            sysMenuEntity.Status = menuEditDto.Status;
            sysMenuEntity.Url = menuEditDto.Url; 
            sysMenuEntity.LastModifierId = menuEditDto.LastModifierId;
            _userMenuService.Update(sysMenuEntity);
        }


        /// <summary>
        /// 得到该用户的所有权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="menuType"></param>
        /// <returns></returns>
        public List<SysMenuTreeDto> GetAuthorityMenuList(int userId, int menuType = 1)
        {
            var sysMenuList = _userMenuService.GetAuthorityMenuList(userId, menuType);
            var sysMenuDtoList = sysMenuList.ToDtos();
            if (!sysMenuDtoList.HasAny())
            {
                return null;
            }
            return sysMenuDtoList;
        }


        public SysMenuDto GetMenuDetailById(int id)
        {
            var menuDto = _userMenuService.Find<SysMenu>(id).ToMenuDto();// ?.MapTo<SysMenu, SysMenuDto>();
            return menuDto;
        }


        public StandardJsonResult DeleteMenuById(int id)
        {
            if (_roleMenuMappingService.Exists<SysRoleMenuMapping>(rm => rm.SysMenuId == id))
            {
                return StandardJsonResult.GetFailureResult("菜单数据已存在角色授权，请先移除授权再删除");
            }
            _userMenuService.Delete<SysMenu>(id);

            return StandardJsonResult.GetSuccessResult("操作成功");
        }

        /// <summary>
        /// 得到所有的可用菜单
        /// </summary>
        /// <returns></returns>
        public List<SysMenuDto> GetMenus()
        {
            var menus = _userMenuService.
                Query<SysMenu>(u => (u.Status))
                .Select(m => new SysMenuDto
                {
                    Id = m.Id,
                    Text = m.Text
                }).ToList();

            return menus;
        }

        /// <summary>
        /// 获取菜单的上下分页数量
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public PagedResult<SysMenuListOutputDto> GetPagedResult(SysMenuListInputDto param)
        {
            if (param == null)
            {
                return null;
            }
            var name = param.Name;

            PagedResult<SysMenu> pagedResult = _userMenuService.QueryPage<SysMenu, int>((u => u.Status && (!name.IsNullOrEmpty() && u.Text.Contains(name)) || name.IsNullOrEmpty()),
               param.PageSize,
               param.PageIndex, n => n.Id, false);

            return pagedResult.ToPaged();
        }
         
    }
}
