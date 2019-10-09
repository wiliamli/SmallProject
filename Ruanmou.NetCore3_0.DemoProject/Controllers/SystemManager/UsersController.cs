using System;
using Microsoft.AspNetCore.Mvc;
 
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Ruanmou04.Core.Utility.Security;       
using Microsoft.Extensions.Configuration;
using Ruanmou04.Core.Utility.Extensions;
using Ruanmou04.NetCore.AOP.Filter;
using Ruanmou04.EFCore.Dtos.DtoHelper;
using Ruanmou04.Core.Dtos.DtoHelper;
using Ruanmou04.NetCore.Interface;
using Ruanmou04.NetCore.Interface.SystemManager.Service;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos;
using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.Core.Utility.DtoUtilities;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos.Input;

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
        public CurrentUser GetUserByID(int userId)
        {
            return _userService.Find<SysUser>(userId).MapTo<SysUser, CurrentUser>();

        }

        /// <summary>
        /// 获取编辑用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public AjaxResult GetEditUserByID(int userId)
        {
            var user = _userService.Find<SysUser>(userId)?.MapTo<SysUser, CurrentUser>();
            return AjaxResult.Success("操作成功", user);
            // return JsonConvert.SerializeObject(new AjaxResult { success = true, data = user });

        }

        /// <summary>
        /// 删除数据通过Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public AjaxResult DeleteById(int id)
        {
            _userService.Delete<SysUser>(id);
            return AjaxResult.Success("删除成功");
            // return JsonConvert.SerializeObject(new AjaxResult { success = true, msg = "删除成功" });
        }

        /// <summary>
        /// 批量删除数据通过Id
        /// </summary>
        /// <param name="ids">id列表</param>
        /// <returns></returns>
        [HttpGet]
        public AjaxResult DeleteBatchById(string ids)
        {
            _userService.Delete<SysUser>(u => ids.Contains(u.Id.ToString()));
            return AjaxResult.Success("删除成功");
            //return JsonConvert.SerializeObject(new AjaxResult { success = true, msg = "删除成功" });
        }

        /// <summary>
        /// 根据用户类型获取用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public AjaxResult GetUsersByType(int userType)
        {
            var userData = _userService.GetSysUsers(u => userType == 0 || u.UserType == userType);
            return AjaxResult.Success("操作成功", userData);
            //return JsonConvert.SerializeObject(new AjaxResult { success = true, data= userData } );
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetUsers(int page, int limit, int userType, string name)
        {
            var userData = _userService.GetSysUsers(u => ((!name.IsNullOrEmpty() && u.Name.Contains(name)) || name.IsNullOrEmpty()) && (userType == 0 || u.UserType == userType));

            PagedResult<CurrentUser> pagedResult = new PagedResult<CurrentUser> { PageIndex = page, PageSize = limit, Rows = userData, Total = userData.Count };
            return Ok(pagedResult);
            // return JsonConvert.SerializeObject(pagedResult);
        }

        /// <summary>
        /// 新增或修改用户 DefaultPassword
        /// </summary>
        /// <param name="sysUserInput"></param>
        /// <returns></returns>
        [HttpPost]
        public AjaxResult SaveUser([FromBody]SysUserInputDto sysUserInput)
        {
            if (sysUserInput == null)
            {
                return AjaxResult.Failure("参数有误");
            }

            //AjaxResult ajaxResult = new AjaxResult { success = false };
            var existsAaccountUser = _userService.Find<SysUser>(u => u.Account == sysUserInput.Account && (sysUserInput.Id == 0 || (sysUserInput.Id > 0 && sysUserInput.Id != u.Id)));
            if (existsAaccountUser != null)
            {
                //ajaxResult.msg = "账号重复,请修改账号";
                //return ajaxResult;
                return AjaxResult.Failure("账号重复,请修改账号");
            }

            //if (sysUserInput != null)
            //{
            if (sysUserInput.Id > 0)
            {
                var user = _userService.Find<SysUser>(sysUserInput.Id);
                //user = DataMapping<SysUserInputDto, SysUser>.Trans(sysUserInput);
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
                user.Address = sysUserInput.Address;  
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
            return AjaxResult.Success("保存成功");
            //ajaxResult.msg = "保存成功";
            //ajaxResult.success = true;
            // }
            //else
            //    ajaxResult.msg = "保存失败";
            //  return ajaxResult;
        }
    }
}