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

        public SysCourse GetCourse(int id)
        {
            return _courseService.Find<SysCourse>(c=>c.Id== id && c.Status);
        }

        //GET api/Courses/GetCoursesByCategory?id=1
        [HttpGet]
        public IEnumerable<SysCourse> GetCoursesByCategory(int id)
        {
            return _courseService.Query<SysCourse>(c=>c.CourseCategoryID== id && c.Status).OrderBy(c => c.Sort);
        }

        //GET api/Courses/GetHotCourses
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
        //GET api/Courses/GetCourseCategory
        public SysCourseCategory GetCourseCategory(int id)
        {
            return _courseService.Find<SysCourseCategory>(c => c.Id == id && c.Status);
        }

        //GET api/Courses/GetCourseCategories
        [HttpGet]
        public IEnumerable<SysCourseCategory> GetCourseCategories()
        {
            return _courseService.Query<SysCourseCategory>(c => c.Status).OrderBy(c => c.Sort);
        }
        #endregion




    }
}