using Ruanmou04.Core.Utility.DtoUtilities;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.NetCore.Dtos.SystemManager.MenuDtos;
using Ruanmou04.NetCore.Dtos.SystemManager.MenuDtos.Input;
using Ruanmou04.NetCore.Dtos.SystemManager.MenuDtos.Output;
using System.Collections.Generic;

namespace Ruanmou04.NetCore.Interface.SystemManager.Applications
{
    public interface ISysMenuApplication : IApplication
    {
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="menuDto"></param>
        void AddMenu(SysMenuAddInputDto menuDto);

        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <param name="menuEditDto"></param>
        void EditMenu(SysMenuEditInputDto menuEditDto);


        List<SysMenuTreeDto> GetAuthorityMenuList(int userID, int menuType = 1);

        /// <summary>
        /// 根据ID获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SysMenuDto GetMenuDetailById(int id);


        /// <summary>
        /// 根据菜单Id删除该菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        StandardJsonResult DeleteMenuById(int id);


        /// <summary>
        /// 得到所有的可用菜单
        /// </summary>
        /// <returns></returns>
        List<SysMenuDto> GetMenus();


        /// <summary>
        /// 获取菜单的上下分页数量
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        PagedResult<SysMenuListOutputDto> GetPagedResult(SysMenuListInputDto param);


    }
}
