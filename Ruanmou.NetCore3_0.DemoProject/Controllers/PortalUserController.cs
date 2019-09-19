using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RM04.DBEntity;
using Ruanmou.NetCore.Interface;
using Ruanmou04.EFCore.Model.DtoHelper;
using Ruanmou04.NetCore.Project.Controllers;
using Ruanmou04.NetCore.Project.Models;
<<<<<<< HEAD
using System.Threading.Tasks;
=======
>>>>>>> d217a75b42dc07b037588035b9f28f71f37e6935
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
        private IMemoryCache _memoryCache = null;
        private ICurrentUserInfo _CurrentUser = null;

        public PortalUserController(ISysUserService userService,
            ICurrentUserInfo currentUser,
            IMemoryCache memoryCache) : base(memoryCache, currentUser)
        {
            this._IUserService = userService;
            this._memoryCache = memoryCache;
            this._CurrentUser = currentUser;
        }

        [HttpGet]
        public SysUserOutputDto GetUser()
        {
            SysUserOutputDto sysUser = base.GetUserInfo();
            var user = _CurrentUser.CurrentUser;
            return sysUser;
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

           // this._memoryCache.Set<SysUserOutputDto>(ajax.data, sysuserdto);

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