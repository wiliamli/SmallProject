using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.Dtos.SystemManager.MenuDtos;
using System.Collections.Generic;

namespace Ruanmou04.NetCore.Interface.SystemManager.Service
{
    public interface IUserMenuService : IBaseService
    {
        List<SysMenu> GetAuthorityMenuList(int userID, int menuType = 1);
    }
}
