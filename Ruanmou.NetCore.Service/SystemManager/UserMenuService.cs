using Microsoft.EntityFrameworkCore;


using Ruanmou.NetCore.Service;
using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.Dtos.SystemManager.MenuDtos;
using Ruanmou04.NetCore.Interface.SystemManager.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ruanmou04.NetCore.Service.SystemManager
{
    public class UserMenuService : BaseService, IUserMenuService
    {
        public UserMenuService(DbContext context) : base(context)
        {

        }
        /// <summary>
        /// 获取有权限的菜单
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        //public List<SysMenuTreeDto> GetAuthorityMenuList(int userID, int menuType)
        //{
        //    var menuTree = from m in Query<SysMenu>(m => m.MenuType == menuType && m.Status)
        //                   join r in Query<SysRoleMenuMapping>() on m.Id equals r.SysMenuId
        //                   join ur in Query<SysUserRoleMapping>(u => u.SysUserId == userID) on r.SysRoleId equals ur.SysRoleId
        //                   select new SysMenuTreeDto
        //                   {
        //                       Id = m.Id,
        //                       Text = m.Text,
        //                       Url = m.Url,
        //                       MenuLevel = m.MenuLevel,
        //                       SourcePath = m.SourcePath,
        //                       MenuIcon = m.MenuIcon,
        //                       Description = m.Description,
        //                       Sort = m.Sort
        //                   };

        //    return menuTree.Distinct().OrderBy(m => m.Sort).ToList();
        //}

        public List<SysMenu> GetAuthorityMenuList(int userID, int menuType)
        {
            var menuTree = from m in Query<SysMenu>(m => m.MenuType == menuType && m.Status)
                           join r in Query<SysRoleMenuMapping>() on m.Id equals r.SysMenuId
                           join ur in Query<SysUserRoleMapping>(u => u.SysUserId == userID) on r.SysRoleId equals ur.SysRoleId
                           select m;
            return menuTree.Distinct().OrderBy(m=>m.Sort).ToList();
        }

    }
}
