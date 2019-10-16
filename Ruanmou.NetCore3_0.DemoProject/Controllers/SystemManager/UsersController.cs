using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Ruanmou.Core.Utility;
using Ruanmou04.Core.Dtos.DtoHelper;
using Ruanmou04.Core.Utility.DtoUtilities;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.Core.Utility.Security;
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
    [CustomAuthorize]
    [Route("api/[controller]/[action]"), ApiController]
    public class UsersController : BaseApiController
    {
        #region MyRegion
        private IConfiguration _configuration = null;
        private ICurrentUserInfo _currentUserInfo = null;
        private ISysUserApplication _userApplication = null;

        public UsersController(
           IConfiguration configuration,
            ICurrentUserInfo currentUserInfo,
            ISysUserApplication userApplication) : base(currentUserInfo)

        {
            this._configuration = configuration;
            this._currentUserInfo = currentUserInfo;
            this._userApplication = userApplication;
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
        public StandardJsonResult UpdateStatusByIds(string ids, int status)
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
        public StandardJsonResult GetUsersByType(int userType)
        { 
            return StandardAction(() => _userApplication.GetUsers(u => userType == 0 || u.UserType == userType));
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
            var result = StandardAction(() => _userApplication.GetPagedResult(param));
            return result;
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
                return StandardJsonResult.GetFailureResult("参数有误");
            }

            var existsAaccountUser = _userApplication.GetUsers(u => u.Account == sysUserInput.Account && (sysUserInput.Id == 0 || (sysUserInput.Id > 0 && sysUserInput.Id != u.Id))); //_userService.Find<SysUser>(u => u.Account == sysUserInput.Account && (sysUserInput.Id == 0 || (sysUserInput.Id > 0 && sysUserInput.Id != u.Id)));
            if (existsAaccountUser != null)
            {
                return StandardJsonResult.GetFailureResult("账号重复,请修改账号");
            }

            if (sysUserInput.Id > 0)
            {
                var sysUserEditInputDto = DataMapping<SysUserInputDto, SysUserEditInputDto>.Trans(sysUserInput);
                sysUserEditInputDto.LastModifyTime = DateTime.Now;
                sysUserEditInputDto.LastModifyId = _currentUserInfo.SysCurrentUser.Id;
                return StandardAction(() => _userApplication.EditUser(sysUserEditInputDto));
            }
            else
            {
                var sysUserAddInputDto = DataMapping<SysUserInputDto, SysUserAddInputDto>.Trans(sysUserInput); 
                var defaultPassword = _configuration[StaticConstraint.DefaultPwd];
                sysUserAddInputDto.Password = Encrypt.EncryptionPassword(defaultPassword);
                sysUserAddInputDto.CreateId = _currentUserInfo.SysCurrentUser.Id;
                sysUserAddInputDto.LastModifyTime = DateTime.Now;
                return StandardAction(() => _userApplication.AddUser(sysUserAddInputDto));
            }
        }

        [HttpPost]
        public StandardJsonResult RestUserPwd(int userId)
        {
            if (userId <= 0)
            {
                return StandardJsonResult.GetFailureResult("参数有误");
            }
            var defaultPassword = _configuration[StaticConstraint.DefaultPwd];
            var userRestPwdInputDto = new UserRestPwdInputDto()
            {
                LastModifyId = _currentUserInfo.SysCurrentUser.Id,
                UserId = userId,
                UserPwd = Encrypt.EncryptionPassword(defaultPassword)
            };
            return StandardAction(() => _userApplication.RestUserPwd(userRestPwdInputDto));
        }
    }
}