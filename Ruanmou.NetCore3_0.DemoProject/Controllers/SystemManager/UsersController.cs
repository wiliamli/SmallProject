using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Ruanmou.Core.Utility;
using Ruanmou04.Core.Dtos.DtoHelper;
using Ruanmou04.Core.Utility.DtoUtilities;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.Core.Utility.Security;
using Ruanmou04.EFCore.Dtos.DtoHelper;
using Ruanmou04.NetCore.AOP.Filter;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos.Input;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos.Output;
using Ruanmou04.NetCore.Interface;
using Ruanmou04.NetCore.Interface.SystemManager.Applications;
using Ruanmou04.NetCore.Project.Controllers;
using System;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Ruanmou.NetCore3_0.DemoProject.Controllers
{
    //[TypeFilter(typeof( CustomExceptionFilterAttribute))]
    [CustomAuthorize]
    [Route("api/[controller]/[action]"), ApiController]
    public class UsersController : BaseApiController
    {
        #region MyRegion
        private IConfiguration _configuration = null;
        private ISysUserApplication _userApplication = null;        
        private ICurrentUserInfo _currentUserInfo = null;
        public UsersController(
            IConfiguration configuration,
            ISysUserApplication userApplication,
            ICurrentUserInfo currentUserInfo,
            IMemoryCache memoryCache) : base(memoryCache, currentUserInfo)
        {
            this._configuration = configuration;
            this._userApplication = userApplication;
            this._currentUserInfo = currentUserInfo;
        }
        #endregion

        // GET api/SysUser/5
        [HttpGet]
        public StandardJsonResult<SysUserDetailOutputDto> GetUserByID(int userId)
        {
            return StandardAction(() => _userApplication.GetUserByUserId(userId));
        }

        /// <summary>
        /// 获取编辑用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public StandardJsonResult<SysUserDetailOutputDto> GetEditUserByID(int userId)
        {
            return StandardAction(() => _userApplication.GetUserByUserId(userId));
        }

        [HttpGet]
        public StandardJsonResult UpdateStatusByIds(string ids,int status)
        {
            return StandardAction(() => _userApplication.UpdateUserStatus(ids, status));
        }

        /// <summary>
        /// 删除数据通过Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public StandardJsonResult DeleteById(int id)
        {   
            return StandardAction(() => _userApplication.DeleteByUserId(id));
        }

        /// <summary>
        /// 批量删除数据通过Id
        /// </summary>
        /// <param name="ids">id列表</param>
        /// <returns></returns>
        [HttpGet]
        public StandardJsonResult DeleteBatchById(string ids)
        {
            return StandardAction(() => _userApplication.DeleteBatchByUserId(ids));
        }

        /// <summary>
        /// 根据用户类型获取用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public AjaxResult GetUsersByType(int userType)
        {
            return null;
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public StandardJsonResult<PagedResult<SysUserListOutputDto>> GetUsers(int page, int limit, int userType, string name)
        {
            var param = new SysUserListInputDto()
            {
                PageSize = page,
                PageIndex = limit,
                Name = name,
                UserType = userType
            };
            return StandardAction(() => _userApplication.GetPagedResult(param));
        }

        /// <summary>
        /// 新增或修改用户 DefaultPassword
        /// </summary>
        /// <param name="sysUserInput"></param>
        /// <returns></returns>
        [HttpPost]
        public StandardJsonResult SaveUser([FromBody]SysUserInputDto sysUserInput)
        {
            if (sysUserInput == null)
            {
                return StandardJsonResult.GetFailure("参数有误");
            }

            var existsAaccountUser = _userApplication.GetUsers(u => u.Account == sysUserInput.Account && (sysUserInput.Id == 0 || (sysUserInput.Id > 0 && sysUserInput.Id != u.Id))); //_userService.Find<SysUser>(u => u.Account == sysUserInput.Account && (sysUserInput.Id == 0 || (sysUserInput.Id > 0 && sysUserInput.Id != u.Id)));
            if (existsAaccountUser != null)
            {
                return StandardJsonResult.GetFailure("账号重复,请修改账号");
            }

            if (sysUserInput.Id > 0)
            {
                var sysUserEditInputDto = DataMapping<SysUserInputDto, SysUserEditInputDto>.Trans(sysUserInput);//sysUserInput.MapTo<SysUserInputDto, SysUserEditInputDto>();
                sysUserEditInputDto.LastModifyTime = DateTime.Now;
                sysUserEditInputDto.LastModifyId = _currentUserInfo.CurrentUser.Id;
                return StandardAction(() => _userApplication.EditUser(sysUserEditInputDto));
            }
            else
            {
                var sysUserAddInputDto = DataMapping<SysUserInputDto, SysUserAddInputDto>.Trans(sysUserInput);  //DataMapping<SysUserInputDto, SysUserAddInputDto>.Trans(sysUserInput);
                var defaultPassword = _configuration[StaticConstraint.DefaultPwd];
                sysUserAddInputDto.Password = Encrypt.EncryptionPassword(defaultPassword);
                sysUserAddInputDto.CreateId = _currentUserInfo.CurrentUser.Id;
               
                return StandardAction(() => _userApplication.AddUser(sysUserAddInputDto));
            }
        }

        ///// <summary>
        ///// 新增或修改用户 DefaultPassword
        ///// </summary>
        ///// <param name="sysUserInput"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public AjaxResult SaveUser([FromBody]SysUserInputDto sysUserInput)
        //{
        //    if (sysUserInput == null)
        //    {
        //        return AjaxResult.Failure("参数有误");
        //    }

        //    //AjaxResult ajaxResult = new AjaxResult { success = false };
        //    var existsAaccountUser = _userService.Find<SysUser>(u => u.Account == sysUserInput.Account && (sysUserInput.Id == 0 || (sysUserInput.Id > 0 && sysUserInput.Id != u.Id)));
        //    if (existsAaccountUser != null)
        //    {
        //        //ajaxResult.msg = "账号重复,请修改账号";
        //        //return ajaxResult;
        //        return AjaxResult.Failure("账号重复,请修改账号");
        //    }

        //    //if (sysUserInput != null)
        //    //{
        //    if (sysUserInput.Id > 0)
        //    {
        //        var user = _userService.Find<SysUser>(sysUserInput.Id);
        //        //user = DataMapping<SysUserInputDto, SysUser>.Trans(sysUserInput);
        //        user.Mobile = sysUserInput.Mobile;
        //        user.Account = sysUserInput.Account;
        //        user.Email = sysUserInput.Email;
        //        user.Name = sysUserInput.Name;
        //        user.Phone = sysUserInput.Phone;
        //        user.QQ = sysUserInput.QQ;
        //        user.Sex = sysUserInput.Sex;
        //        user.Status = sysUserInput.Status;
        //        user.WeChat = sysUserInput.WeChat;
        //        user.UserType = sysUserInput.UserType;
        //        user.Address = sysUserInput.Address;
        //        user.LastModifyTime = DateTime.Now;
        //        user.LastModifyId = _currentUserInfo.CurrentUser.Id;

        //        _userService.Update<SysUser>(user);
        //    }
        //    else
        //    {
        //        var user = sysUserInput.MapTo<SysUserInputDto, SysUser>();
        //        var defaultPassword = _configuration[StaticConstraint.DefaultPwd];
        //        if (defaultPassword.IsValid())
        //        {
        //            user.Password = Encrypt.EncryptionPassword(defaultPassword);
        //        }
        //        else
        //        {
        //            user.Password = Encrypt.EncryptionPassword("123");
        //        }
        //        user.CreateTime = DateTime.Now;
        //        user.CreateId = _currentUserInfo.CurrentUser.Id;
        //        _userService.Insert<SysUser>(user);
        //    }
        //    return AjaxResult.Success("保存成功");
        //    //ajaxResult.msg = "保存成功";
        //    //ajaxResult.success = true;
        //    // }
        //    //else
        //    //    ajaxResult.msg = "保存失败";
        //    //  return ajaxResult;
        //}


    }
}