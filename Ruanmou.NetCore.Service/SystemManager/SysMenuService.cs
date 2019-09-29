

using Microsoft.EntityFrameworkCore;



using Ruanmou04.NetCore.Dtos.SystemManager.MenuDtos;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos;
using Ruanmou04.NetCore.Interface.SystemManager.Service;
using System.Collections.Generic;

namespace Ruanmou.NetCore.Service
{
    public class SysMenuService : BaseService, ISysMenuService
    {
        public SysMenuService(DbContext context) : base(context)
        {
        }

        
        public List<SysMenuTreeDto> GetMenuTree(CurrentUser currentUser)
        {
            //base.Query<SysMenu>();
            return null;
        }


    }
}
