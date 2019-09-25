

using Microsoft.EntityFrameworkCore;
using RM04.DBEntity;
using Ruanmou.NetCore.Interface;
using Ruanmou04.Core.Dtos.DtoHelper;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Ruanmou.NetCore.Service
{
    public class SysUserService : BaseService, ISysUserService
    {
        public SysUserService(DbContext context) : base(context)
        {
        }

        public void UpdateLastLogin(SysUser user)
        {
            SysUser userDB = base.Find<SysUser>(user.Id);
            //userDB.LastLoginTime = DateTime.Now;
            this.Commit();
        }
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public List<SysUserOutputDto> GetSysUsers(Expression<Func<SysUser,bool>> funcWhere)
        {
            var users = Query<SysUser>(funcWhere);
            
            var userDtolist=users.Select(s => s.MapTo<SysUser, SysUserOutputDto>()
            //new SysUserOutputDto
            //{
            //    Account = s.Account,
            //    Address = s.Address,
            //    Email = s.Email,
            //    Id = s.Id,
            //    Mobile = s.Mobile,
            //    Name = s.Name,
            //    Phone = s.Phone,
            //    QQ = s.QQ,
            //    Sex = s.Sex,
            //    Status = s.Status,
            //    WeChat = s.WeChat
            //}
            ).ToList() ;

            return userDtolist;

        }


    }
}
