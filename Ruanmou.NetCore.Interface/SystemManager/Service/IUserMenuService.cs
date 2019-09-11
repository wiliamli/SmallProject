using RM04.DBEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Interface.SystemManager.Service
{
    public interface IUserMenuService
    {
        List<SysMenuTreeDto> GetAuthorityMenuList(int userID, int menuType);
    }
}
