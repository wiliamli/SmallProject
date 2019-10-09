using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;  
using Ruanmou04.EFCore.Dtos.DtoHelper;
using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.AOP.Filter;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos.Output;
using Ruanmou04.NetCore.Interface;
using Ruanmou04.NetCore.Interface.SystemManager.Service;
using Ruanmou04.NetCore.Project.Controllers; 
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Ruanmou.NetCore3_0.DemoProject.Controllers
{

    [Route("api/[controller]/[action]")]
    [ServiceFilter(typeof(VerifyAttribute))]
    [ApiController]
    public class PortalUserController : BaseApiController
    {
        private ISysUserService _IUserService = null;
        private ICurrentUserInfo _CurrentUser = null;

        public PortalUserController(ISysUserService userService,
            ICurrentUserInfo currentUser) : base(currentUser)
        {
            this._IUserService = userService;
            this._CurrentUser = currentUser;
        }

        [HttpGet]
        public CurrentUser GetUser()
        {
            return base.GetUserInfo();
        }

        [HttpPost]
        public AjaxResult SaveUser([FromBody]SysUserOutputDto model)
        { 
            SysUser user = _IUserService.Find<SysUser>(model.Id);
           
            user.Name = model.Name;
            user.Mobile = model.Mobile;
            user.QQ = model.QQ;
            user.WeChat = model.WeChat;
            if(string.IsNullOrWhiteSpace(model.Sex)) user.Sex = model.Sex; 

            _IUserService.Update(user);

            return new AjaxResult()
            {
                success = true,
                msg = "ok",
            };
        }

        [HttpPost]
        public AjaxResult SaveUserV2(SysUserOutputDto model)
        {
            SysUser user = _IUserService.Find<SysUser>(model.Id);

            //user.Name = model.Name;
            //user.Mobile = model.Mobile;
            //user.QQ = model.QQ;
            //user.WeChat = model.WeChat;
            //if (string.IsNullOrWhiteSpace(model.Sex)) user.Sex = model.Sex;

            //_IUserService.Update(user);
            return new AjaxResult()
            {
                success = true,
                msg = "ok",
            };
        }

    }
}