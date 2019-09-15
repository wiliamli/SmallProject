using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RM04.DBEntity;
using Ruanmou.NetCore.Interface;
using Ruanmou04.EFCore.Model.DtoHelper;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Ruanmou04.Core.Utility.Security;
using Ruanmou04.Core.Model.DtoHelper;
using Ruanmou04.NetCore.Project.Utility;
using Ruanmou04.NetCore.Project.Models;
using Newtonsoft.Json;
using Ruanmou04.Core.Utility;
using Microsoft.Extensions.Configuration;
using Ruanmou04.Core.Utility.Extensions;

namespace Ruanmou.NetCore3_0.DemoProject.Controllers
{
    //[CustomAuthorize]
    [Route("api/[controller]/[action]"), ApiController]
    public class UsersController : ControllerBase
    {
        #region MyRegion
        private IConfiguration _configuration = null;
        private ISysUserService _IUserService = null;
        private ICurrentUserInfo _currentUserInfo = null;
        public UsersController(
           IConfiguration configuration,
            ICurrentUserInfo currentUserInfo,
            ISysUserService userService)
        {
            this._configuration = configuration;
            this._IUserService = userService;
            this._currentUserInfo = currentUserInfo;
        }
        #endregion


        // GET api/SysUser/5
        [HttpGet]

        public SysUserOutputDto GetUserByID(int userId)
        {
            return _IUserService.Find<SysUser>(userId).MapTo<SysUser, SysUserOutputDto>();

        }

        /// <summary>
        /// 获取编辑用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]

        public string GetEditUserByID(int userId)
        {
            var user= _IUserService.Find<SysUser>(userId)?.MapTo<SysUser, SysUserOutputDto>();
            return JsonConvert.SerializeObject(new AjaxResult { success = true, data = user });

        }


        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        public string GetUsers(int page,int limit,string name)
        {
            var userData= _IUserService.GetSysUsers(u=>(!name.IsNullOrEmpty() && u.Name.Contains(name)) || name.IsNullOrEmpty());

            PagedResult<SysUserOutputDto> pagedResult = new PagedResult<SysUserOutputDto> { PageIndex=page , PageSize=limit, Rows=userData,Total=userData.Count };

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
            if (sysUserInput != null)
            {
                if (sysUserInput.Id > 0)
                {
                    var user = _IUserService.Find<SysUser>(sysUserInput.Id);
                    user.Mobile = sysUserInput.Mobile;
                    user.Account = sysUserInput.Account;
                    user.Email = sysUserInput.Email;
                    user.Name = sysUserInput.Name;
                    user.Phone = sysUserInput.Phone;
                    user.QQ = sysUserInput.QQ;
                    user.Sex = sysUserInput.Sex;
                    user.Status = sysUserInput.Status;
                    user.WeChat = sysUserInput.WeChat;
                    
                    _IUserService.Update<SysUser>(user);
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
                    _IUserService.Insert<SysUser>(user);
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