
using Ruanmou04.EFCore.Dtos.DtoHelper;
using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.Dtos.SystemManager.LoginDtos;
using Ruanmou04.NetCore.Interface;

namespace Ruanmou04.NetCore.Interface.SystemManager.Service
{
    public interface ILoginService : IBaseService
    {
       // AjaxResult Login(LoginInputDto loginInput);

        SysUser Login(string account);
    }
}
