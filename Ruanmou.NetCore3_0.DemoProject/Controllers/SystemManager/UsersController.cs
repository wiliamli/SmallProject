using System;
using Microsoft.AspNetCore.Mvc;
using RM04.DBEntity;
using Ruanmou.NetCore.Interface;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Ruanmou04.Core.Utility.Security;
using Ruanmou04.NetCore.Project.Models;
using Newtonsoft.Json;
using Ruanmou04.Core.Utility;
using Microsoft.Extensions.Configuration;
using Ruanmou04.Core.Utility.Extensions;
using Ruanmou04.NetCore.AOP.Filter;
using Ruanmou04.EFCore.Dtos.DtoHelper;
using Ruanmou04.Core.Dtos.DtoHelper;

namespace Ruanmou.NetCore3_0.DemoProject.Controllers
{
    //[TypeFilter(typeof( CustomExceptionFilterAttribute))]
    [CustomAuthorize]
    [Route("api/[controller]/[action]"), ApiController]
    public class UsersController : ControllerBase
    {
        #region MyRegion
        private IConfiguration _configuration = null;
        private ISysUserService _userService = null;
        private ICurrentUserInfo _currentUserInfo = null;
        public UsersController(
           IConfiguration configuration,
            ICurrentUserInfo currentUserInfo,
            ISysUserService userService)
        {
            this._configuration = configuration;
            this._userService = userService;
            this._currentUserInfo = currentUserInfo;
        }
        #endregion


        // GET api/SysUser/5
        [HttpGet]

        public SysUserOutputDto GetUserByID(int userId)
        {
            return _userService.Find<SysUser>(userId).MapTo<SysUser, SysUserOutputDto>();

        }

        /// <summary>
        /// 获取编辑用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]

        public string GetEditUserByID(int userId)
        {
            var user = _userService.Find<SysUser>(userId)?.MapTo<SysUser, SysUserOutputDto>();
            return JsonConvert.SerializeObject(new AjaxResult { success = true, data = user });

        }
        /// <summary>
        /// 删除数据通过Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]

        public string DeleteById(int id)
        {
            _userService.Delete<SysUser>(id);
            return JsonConvert.SerializeObject(new AjaxResult { success = true, msg = "删除成功" });
        }
        /// <summary>
        /// 批量删除数据通过Id
        /// </summary>
        /// <param name="ids">id列表</param>
        /// <returns></returns>
        [HttpGet]

        public string DeleteBatchById(string ids)
        {
            _userService.Delete<SysUser>(u => ids.Contains(u.Id.ToString()));
            return JsonConvert.SerializeObject(new AjaxResult { success = true, msg = "删除成功" });
        }
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        public string GetUsers(int page, int limit, int userType, string name)
        {

            var userData = _userService.GetSysUsers(u => ((!name.IsNullOrEmpty() && u.Name.Contains(name)) || name.IsNullOrEmpty()) && (userType==0 || u.UserType == userType));

            PagedResult<SysUserOutputDto> pagedResult = new PagedResult<SysUserOutputDto> { PageIndex = page, PageSize = limit, Rows = userData, Total = userData.Count };
            return JsonConvert.SerializeObject(pagedResult);


        }

        /// <summary>
        /// 新增或修改用户 DefaultPassword
        /// </summary>
        /// <param name="sysUserInput"></param>
        /// <returns></returns>
        [HttpPost]
        public AjaxResult SaveUser([FromBody]SysUserInputDto sysUserInput)
        {

            AjaxResult ajaxResult = new AjaxResult { success = false };
            var existsAaccountUser = _userService.Find<SysUser>(u => u.Account == sysUserInput.Account && (sysUserInput.Id == 0 ||(sysUserInput.Id>0 && sysUserInput.Id!=u.Id)));
            if (existsAaccountUser != null)
            {
                ajaxResult.msg = "账号重复,请修改账号";
                return ajaxResult;
            }
            if (sysUserInput != null)
            {
                if (sysUserInput.Id > 0)
                {
                    var user = _userService.Find<SysUser>(sysUserInput.Id);
                    user.Mobile = sysUserInput.Mobile;
                    user.Account = sysUserInput.Account;
                    user.Email = sysUserInput.Email;
                    user.Name = sysUserInput.Name;
                    user.Phone = sysUserInput.Phone;
                    user.QQ = sysUserInput.QQ;
                    user.Sex = sysUserInput.Sex;
                    user.Status = sysUserInput.Status;
                    user.WeChat = sysUserInput.WeChat;
                    user.UserType = sysUserInput.UserType;
                    user.LastModifyTime = DateTime.Now;
                    user.LastModifyId = _currentUserInfo.CurrentUser.Id;
                    _userService.Update<SysUser>(user);
                }
                else
                {
                    var user = sysUserInput.MapTo<SysUserInputDto, SysUser>();
                    var defaultPassword = _configuration["DefaultPassword"];
                    if (defaultPassword.IsNullOrWhiteSpace())
                    {
                        user.Password = Encrypt.EncryptionPassword(defaultPassword);
                    }
                    else
                    {
                        user.Password = Encrypt.EncryptionPassword("123");
                    }
                    user.CreateTime = DateTime.Now;
                    user.CreateId = _currentUserInfo.CurrentUser.Id;
                    _userService.Insert<SysUser>(user);
                }
                ajaxResult.msg = "保存成功";
                ajaxResult.success = true;
            }
            else
                ajaxResult.msg = "保存失败";
            return ajaxResult;
        }


    }
}