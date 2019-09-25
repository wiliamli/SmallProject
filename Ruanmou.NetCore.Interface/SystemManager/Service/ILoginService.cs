using Aio.Domain.SystemManage.Dtos;
using Ruanmou.NetCore.Interface;
using Ruanmou04.EFCore.Dtos.DtoHelper;

namespace Ruanmou.NetCore.Service
{
    public interface ILoginService : IBaseService
    {

        AjaxResult Login(LoginInputDto loginInput);



    }
}
