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
    public class CoursesController : ControllerBase
    {
        #region MyRegion
        private ILoggerFactory _Factory = null;
        private ILogger<CoursesController> _logger = null;
        private ISysCourseService _courseService = null; //需要注册
        private List<SysCourse> _courseList = new List<SysCourse>();
        public CoursesController(ILoggerFactory factory,
            ILogger<CoursesController> logger,
            ISysCourseService courseService)
        {
            this._Factory = factory;
            this._logger = logger;
            this._courseService = courseService;
        }
        #endregion


        #region HttpGet

        // GET api/Courses/GetCourseByID?id=1
        [HttpGet]

        public SysCourse GetCourseByID(int id)
        {
            return _courseService.Find<SysCourse>(c=>c.Id== id && c.Status);
        }

        //GET api/Courses/GetCoursesByCategoryID/1
        [HttpGet]
        public IEnumerable<SysCourse> GetCoursesByCategoryID(int id)
        {
            return _courseService.Query<SysCourse>(c=>c.CourseCategoryID== id && c.Status).OrderBy(c => c.Sort);
        }

        //GET api/SysCourse/GetHotCourses/3
        [HttpGet]
        public IEnumerable<SysCourse> GetHotCourses(int TopN = 5)
        {
            return _courseService.Query<SysCourse>(c => c.IsHot == 1 && c.Status).OrderBy(c=>c.Sort).Take(TopN);
        }

        #endregion HttpGet

        /*
         还欠缺分页查询，条件：分类 & 排序 & 页码 & 每页大小
        */

        #region 课程分类
        // GET api/Courses/GetSysCourseCategories
        [HttpGet]
        public IEnumerable<SysCourseCategory> GetSysCourseCategories()
        {
            return _courseService.Query<SysCourseCategory>(c => c.Status).OrderBy(c => c.Sort);
        }
        #endregion




    }
}