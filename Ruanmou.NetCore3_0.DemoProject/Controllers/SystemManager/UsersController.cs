using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
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

namespace Ruanmou.NetCore3_0.DemoProject.Controllers
{



    [Route("api/[controller]/[action]"), ApiController]
    public class UsersController : ControllerBase
    {
        #region MyRegion
        private ILoggerFactory _Factory = null;
        private ILogger<UsersController> _logger = null;
        private ISysUserService _IUserService = null;
        public UsersController(ILoggerFactory factory,
            ILogger<UsersController> logger,

            ISysUserService userService)
        {
            this._Factory = factory;
            this._logger = logger;
            this._IUserService = userService;
        }
        #endregion


        #region HttpGet

        // GET api/SysUser/5
        [HttpGet]

        public SysUser GetUserByID(int userId)
        {
            return _IUserService.Find<SysUser>(userId);

        }

        //GET api/SysUser/?username=xx
        [HttpGet]
        //[CustomBasicAuthorizeAttribute]
        //[CustomExceptionFilterAttribute]
        public IEnumerable<SysUser> GetUserByName(string userName)
        {

            return _IUserService.Query<SysUser>(u=>u.Name==userName);
        }

        //GET api/SysUser/?username=xx&id=1
        [HttpGet]

        public IEnumerable<SysUser> GetUserByNameId(string userName, int id)
        {


            return _IUserService.Query<SysUser>(p => string.Equals(p.Name, userName, StringComparison.OrdinalIgnoreCase));
        }

        #endregion HttpGet

        #region HttpPost


        //POST api/SysUser/register
        //只接受一个参数的需要不给key才能拿到
        [HttpPost]
        public SysUser Register([FromBody]int id)//可以来自FromBody   FromUri
                                               //public SysUser Register(int id)//可以来自url
        {
            string idParam = base.HttpContext.Request.Form["id"];

            
            return null;
        }

        //POST api/SysUser/RegisterUser
        [HttpPost]
        public AjaxResult RegisterUser(SysUserInputDto user)//可以来自FromBody   FromUri
        {
            string idParam = base.HttpContext.Request.Form["Id"];
            string nameParam = base.HttpContext.Request.Form["UserName"];
            string emailParam = base.HttpContext.Request.Form["UserEmail"];

            return null;
        }


       
        #endregion HttpPost

        #region HttpPut
        [HttpPut]
        public AjaxResult RegisterUserPut(SysUserInputDto userInput)//可以来自FromBody   FromUri
        {
            AjaxResult ajaxResult = new AjaxResult { success = false };
            //string idParam = base.HttpContext.Request.Form["Id"];
            //string nameParam = base.HttpContext.Request.Form["UserName"];
            //string emailParam = base.HttpContext.Request.Form["UserEmail"];
            //var user= DataMapping<SysUserInputDto, SysUser>.Trans(userInput);
            var user= userInput.MapTo<SysUserInputDto, SysUser>();
            user.Password = Encrypt.EncryptionPassword(user.Password);
            //user.CreateId= user.LastModifyId = 1;
            //user.CreateTime=user.LastLoginTime=user.LastModifyTime = DateTime.Now;

            user = _IUserService.Insert<SysUser>(user);
            
            if (user!=null)
            {
                ajaxResult.success = true;
            }
            else
            {
                ajaxResult.msg = "添加失败";
            }
            return ajaxResult;
        }

[HttpPut]
        public AjaxResult Registertest(SysRoleDto userInput)//可以来自FromBody   FromUri
        {
            AjaxResult ajaxResult = new AjaxResult { success = false };
            //string idParam = base.HttpContext.Request.Form["Id"];
            //string nameParam = base.HttpContext.Request.Form["UserName"];
            //string emailParam = base.HttpContext.Request.Form["UserEmail"];
            //var user= DataMapping<SysUserInputDto, SysUser>.Trans(userInput);
            //var user= userInput.MapTo<SysUserInputDto, SysUser>();
            //user.Password = Encrypt.EncryptionPassword(user.Password);
            var role = userInput.MapTo<SysRoleDto, SysRole>();
            _IUserService.Insert<SysRole>(role);
            
            return ajaxResult;
        }


        
        #endregion HttpPut

        #region HttpDelete
        //POST api/SysUser/RegisterNoneDelete

       

        //POST api/SysUser/RegisterUserDelete
        [HttpDelete]
        public SysUser RegisterUserDelete(SysUser user)//可以来自FromBody   FromUri
        {
            string idParam = base.HttpContext.Request.Form["Id"];
            string nameParam = base.HttpContext.Request.Form["UserName"];
            string emailParam = base.HttpContext.Request.Form["UserEmail"];
            return user;
        }


       
        #endregion HttpDelete
    }
}