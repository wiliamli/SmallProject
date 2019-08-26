

using Microsoft.EntityFrameworkCore;
using Ruanmou.EFCore3_0.Model;
using Ruanmou.NetCore.Interface;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.NetCore.Service
{
    public class UserService : BaseService, IUserService
    {
        public UserService(DbContext context) : base(context)
        {
        }

        public void UpdateLastLogin(User user)
        {
            User userDB = base.Find<User>(user.Id);
            userDB.LastLoginTime = DateTime.Now;
            this.Commit();
        }
        //public void Add()
        //{
        //    throw new NotImplementedException();
        //}

        //public void Delete()
        //{
        //    throw new NotImplementedException();
        //}

        //public void Query()
        //{
        //    throw new NotImplementedException();
        //}

        //public void Update()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
