
using Ruanmou.EFCore3_0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.NetCore.Interface
{
    public interface IUserCompanyService : IBaseService
    {
        //void Query();
        //void Update();
        //void Delete();
        //void Add();

        //void UpdateLastLogin(User user);
        void UpdateLastLogin(User user);

        bool RemoveCompanyAndUser(Company company);
    }
}
