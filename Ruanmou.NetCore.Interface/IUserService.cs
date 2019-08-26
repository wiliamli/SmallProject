
using Ruanmou.EFCore3_0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.NetCore.Interface
{
    public interface IUserService : IBaseService
    {
        //void Query();
        //void Update();
        //void Delete();
        //void Add();

        void UpdateLastLogin(User user);
    }
}
