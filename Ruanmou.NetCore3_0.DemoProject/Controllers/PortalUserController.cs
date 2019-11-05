using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Ruanmou04.Core.Utility.Extensions;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.Core.Utility.Security;
using Ruanmou04.EFCore.Dtos.DtoHelper;
using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.AOP.Filter;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos.Input;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos.Output;
using Ruanmou04.NetCore.Interface;
using Ruanmou04.NetCore.Interface.SystemManager.Applications;
using Ruanmou04.NetCore.Interface.SystemManager.Service;
using Ruanmou04.NetCore.Project.Controllers;
using System;
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
        private ICurrentUserInfo _currentUserInfo = null;
        private ISysUserApplication _userApplication = null;

        public PortalUserController(ISysUserService userService,
            ICurrentUserInfo currentUser,
            ISysUserApplication userApplication) : base(currentUser)
        {
            this._IUserService = userService;
            this._currentUserInfo = currentUser;
            this._userApplication = userApplication;
        }

        //[HttpGet]
        //public CurrentUser GetUser()
        //{
        //    return base.GetUserInfo();
        //}
        [HttpGet]
        public StandardJsonResult<SysUserDetailOutputDto> GetUserByID()
        {
            int userId = _currentUserInfo.CurrentUser.Id;
            return StandardAction(() => _userApplication.GetUserByUserId(userId));
        }

        [HttpPost]
        public StandardJsonResult SaveUser([FromBody]SysUserEditInputDto sysUserEditInputDto)
        {
            //SysUser user = _IUserService.Find<SysUser>(model.Id);

            //user.Name = model.Name;
            //user.Mobile = model.Mobile;
            //user.QQ = model.QQ;
            //user.WeChat = model.WeChat;
            //if (string.IsNullOrWhiteSpace(model.Sex)) user.Sex = model.Sex;
            //_IUserService.Update(user);
           
            sysUserEditInputDto.LastModifyTime = DateTime.Now;
            sysUserEditInputDto.LastModifyId = _currentUserInfo.CurrentUser.Id;
            return StandardAction(() => _userApplication.EditUser4Memeber(sysUserEditInputDto));
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

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public StandardJsonResult UpdatePwd(UserUpdatePwdInputDto param)
        {
            if (param == null || param.UserOldPwd.IsNullOrWhiteSpace() || param.UserPwd.IsNullOrWhiteSpace())
            {
                return StandardJsonResult.GetFailureResult("参数有误");
            }       

            int userId = _currentUserInfo.CurrentUser.Id;
            var userRestPwdInputDto = new UserUpdatePwdInputDto()
            {
                LastModifyId = userId,
                UserId = userId,
                UserPwd = Encrypt.EncryptionPassword(param.UserPwd),
                UserOldPwd = Encrypt.EncryptionPassword(param.UserOldPwd)
            };
            return _userApplication.UpdateUserPwd(userRestPwdInputDto);
        }


    }
}