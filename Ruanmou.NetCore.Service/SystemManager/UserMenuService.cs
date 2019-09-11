using Microsoft.EntityFrameworkCore;
using RM04.DBEntity;
using Ruanmou.NetCore.Service;
using Ruanmou04.NetCore.Interface.SystemManager.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Service.SystemManager
{
    public class UserMenuService : BaseService, IUserMenuService
    {
        public UserMenuService(DbContext context) : base(context)
        {

        }

        public List<SysMenuTreeDto> GetAuthorityMenuList(int userID)
        {
            throw new NotImplementedException();
        }
        //public List<SysMenuTreeDto> GetAuthorityMenuList(int userID)
        //{
        //    //var menuList=from rm in base
        //    throw new un
        //}
    }
}
