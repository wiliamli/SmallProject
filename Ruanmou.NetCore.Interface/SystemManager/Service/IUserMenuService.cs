using RM04.DBEntity;
using Ruanmou.NetCore.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Interface.SystemManager.Service
{
    public interface IUserMenuService : IBaseService
    {
        List<SysMenuTreeDto> GetAuthorityMenuList(int userID, int menuType = 1);
    }
}
