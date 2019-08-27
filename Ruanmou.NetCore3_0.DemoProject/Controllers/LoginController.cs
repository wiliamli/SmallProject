using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RM04.DBEntity;
using Ruanmou.NetCore.Interface;
using Ruanmou.NetCore3_0.DemoProject.Utility; 
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Ruanmou.NetCore3_0.DemoProject.Controllers
{

    

    [Route("api/[controller]/[action]"), ApiController]
    public class LoginController : ControllerBase
    {
        #region MyRegion
        private ILoggerFactory _Factory = null;
        private ILogger<UsersController> _logger = null;
        private ISysUserService _IUserService = null;
        private List<SysUser> _userList = new List<SysUser>();
        public LoginController(ILoggerFactory factory,
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
        //POST api/SysUser/RegisterNone
        [HttpPost]
        public SysUser RegisterNone()
        {
            return _userList.FirstOrDefault();
        }

        [HttpPost]
        public SysUser RegisterNoKey([FromBody]int id)
        {
            string idParam = base.HttpContext.Request.Form["id"];

            var user = _userList.FirstOrDefault(users => users.Id == id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;
        }

        //POST api/SysUser/register
        //只接受一个参数的需要不给key才能拿到
        [HttpPost]
        public SysUser Register([FromBody]int id)//可以来自FromBody   FromUri
                                               //public SysUser Register(int id)//可以来自url
        {
            string idParam = base.HttpContext.Request.Form["id"];

            var user = _userList.FirstOrDefault(users => users.Id == id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;
        }

        //POST api/SysUser/RegisterUser
        [HttpPost]
        public SysUser RegisterUser(SysUser user)//可以来自FromBody   FromUri
        {
            string idParam = base.HttpContext.Request.Form["Id"];
            string nameParam = base.HttpContext.Request.Form["UserName"];
            string emailParam = base.HttpContext.Request.Form["UserEmail"];

            return user;
        }


        //POST api/SysUser/register
        [HttpPost]
        public string RegisterObject(JObject jData)//可以来自FromBody   FromUri
        {
            string idParam = base.HttpContext.Request.Form["User[Id]"];
            string nameParam = base.HttpContext.Request.Form["User[UserName]"];
            string emailParam = base.HttpContext.Request.Form["User[UserEmail]"];
            string infoParam = base.HttpContext.Request.Form["info"];
            dynamic json = jData;
            JObject jUser = json.User;
            string info = json.Info;
            var user = jUser.ToObject<SysUser>();

            return string.Format("{0}_{1}_{2}_{3}", user.Id, user.Name, user.Email, info);
        }

        [HttpPost]
        public string RegisterObjectDynamic(dynamic dynamicData)//可以来自FromBody   FromUri
        {
            string idParam = base.HttpContext.Request.Form["User[Id]"];
            string nameParam = base.HttpContext.Request.Form["User[UserName]"];
            string emailParam = base.HttpContext.Request.Form["User[UserEmail]"];
            string infoParam = base.HttpContext.Request.Form["info"];
            dynamic json = dynamicData;
            JObject jUser = json.User;
            string info = json.Info;
            var user = jUser.ToObject<SysUser>();

            return string.Format("{0}_{1}_{2}_{3}", user.Id, user.Name, user.Email, info);
        }
        #endregion HttpPost

        #region HttpPut
        //POST api/SysUser/RegisterNonePut
        [HttpPut]
        public SysUser RegisterNonePut()
        {
            return _userList.FirstOrDefault();
        }

        [HttpPut]
        public SysUser RegisterNoKeyPut([FromBody]int id)
        {
            string idParam = base.HttpContext.Request.Form["id"];

            var user = _userList.FirstOrDefault(users => users.Id == id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;
        }

        //POST api/SysUser/registerPut
        //只接受一个参数的需要不给key才能拿到
        [HttpPut]
        public SysUser RegisterPut([FromBody]int id)//可以来自FromBody   FromUri
                                                  //public SysUser Register(int id)//可以来自url
        {
            string idParam = base.HttpContext.Request.Form["id"];

            var user = _userList.FirstOrDefault(users => users.Id == id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;
        }

        //POST api/SysUser/RegisterUserPut
        [HttpPut]
        public SysUser RegisterUserPut(SysUser user)//可以来自FromBody   FromUri
        {
            string idParam = base.HttpContext.Request.Form["Id"];
            string nameParam = base.HttpContext.Request.Form["UserName"];
            string emailParam = base.HttpContext.Request.Form["UserEmail"];

            return user;
        }


        //POST api/SysUser/registerPut
        [HttpPut]
        public string RegisterObjectPut(JObject jData)//可以来自FromBody   FromUri
        {
            string idParam = base.HttpContext.Request.Form["User[Id]"];
            string nameParam = base.HttpContext.Request.Form["User[UserName]"];
            string emailParam = base.HttpContext.Request.Form["User[UserEmail]"];
            string infoParam = base.HttpContext.Request.Form["info"];
            dynamic json = jData;
            JObject jUser = json.User;
            string info = json.Info;
            var user = jUser.ToObject<SysUser>();

            return string.Format("{0}_{1}_{2}_{3}", user.Id, user.Name, user.Email, info);
        }

        [HttpPut]
        public string RegisterObjectDynamicPut(dynamic dynamicData)//可以来自FromBody   FromUri
        {
            string idParam = base.HttpContext.Request.Form["User[Id]"];
            string nameParam = base.HttpContext.Request.Form["User[UserName]"];
            string emailParam = base.HttpContext.Request.Form["User[UserEmail]"];
            string infoParam = base.HttpContext.Request.Form["info"];
            dynamic json = dynamicData;
            JObject jUser = json.User;
            string info = json.Info;
            var user = jUser.ToObject<SysUser>();

            return string.Format("{0}_{1}_{2}_{3}", user.Id, user.Name, user.Email, info);
        }
        #endregion HttpPut

        #region HttpDelete
        //POST api/SysUser/RegisterNoneDelete
        [HttpDelete]
        public SysUser RegisterNoneDelete()
        {
            return _userList.FirstOrDefault();
        }

        [HttpDelete]
        public SysUser RegisterNoKeyDelete([FromBody]int id)
        {
            string idParam = base.HttpContext.Request.Form["id"];

            var user = _userList.FirstOrDefault(users => users.Id == id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;
        }

        //POST api/SysUser/registerDelete
        //只接受一个参数的需要不给key才能拿到
        [HttpDelete]
        public SysUser RegisterDelete([FromBody]int id)//可以来自FromBody   FromUri
                                                     //public SysUser Register(int id)//可以来自url
        {
            string idParam = base.HttpContext.Request.Form["id"];

            var user = _userList.FirstOrDefault(users => users.Id == id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;
        }

        //POST api/SysUser/RegisterUserDelete
        [HttpDelete]
        public SysUser RegisterUserDelete(SysUser user)//可以来自FromBody   FromUri
        {
            string idParam = base.HttpContext.Request.Form["Id"];
            string nameParam = base.HttpContext.Request.Form["UserName"];
            string emailParam = base.HttpContext.Request.Form["UserEmail"];
            return user;
        }


        //POST api/SysUser/registerDelete
        [HttpDelete]
        public string RegisterObjectDelete(JObject jData)//可以来自FromBody   FromUri
        {
            string idParam = base.HttpContext.Request.Form["User[Id]"];
            string nameParam = base.HttpContext.Request.Form["User[UserName]"];
            string emailParam = base.HttpContext.Request.Form["User[UserEmail]"];
            string infoParam = base.HttpContext.Request.Form["info"];
            dynamic json = jData;
            JObject jUser = json.User;
            string info = json.Info;
            var user = jUser.ToObject<SysUser>();

            return string.Format("{0}_{1}_{2}_{3}", user.Id, user.Name, user.Email, info);
        }

        [HttpDelete]
        public string RegisterObjectDynamicDelete(dynamic dynamicData)//可以来自FromBody   FromUri
        {
            string idParam = base.HttpContext.Request.Form["User[Id]"];
            string nameParam = base.HttpContext.Request.Form["User[UserName]"];
            string emailParam = base.HttpContext.Request.Form["User[UserEmail]"];
            string infoParam = base.HttpContext.Request.Form["info"];
            dynamic json = dynamicData;
            JObject jUser = json.User;
            string info = json.Info;
            var user = jUser.ToObject<SysUser>();

            return string.Format("{0}_{1}_{2}_{3}", user.Id, user.Name, user.Email, info);
        }
        #endregion HttpDelete
    }
}