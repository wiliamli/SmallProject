
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos;
using Ruanmou04.NetCore.Interface;

namespace Ruanmou04.NetCore.Interface
{
    /// 功能描述    ：CurrentUserInfo  
    /// 创 建 者    ：magic
    /// 创建日期    ：2019/9/7 12:04:33 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2019/9/7 12:04:33 
    public interface ICurrentUserInfo: IApplication
    {
        CurrentUser CurrentUser { get; }

        CurrentUser SysCurrentUser { get; }
    }
}
