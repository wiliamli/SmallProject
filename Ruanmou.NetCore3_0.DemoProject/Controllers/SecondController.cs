using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ruanmou.EFCore3_0.Model;
using Ruanmou.NetCore.Interface;
using Ruanmou.NetCore3_0.DemoProject.Utility;
using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;

namespace Ruanmou.NetCore3_0.DemoProject.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SecondController : Controller
    {
        #region MyRegion
        private ILoggerFactory _Factory = null;
        private ILogger<SecondController> _logger = null;
        private ITestServiceA _ITestServiceA = null;
        private ITestServiceB _ITestServiceB = null;
        private ITestServiceC _ITestServiceC = null;
        private ITestServiceD _ITestServiceD = null;
        private IUserService _IUserService = null;
        private IA _IA = null;
        public ITestServiceA ITestServiceA { get; set; }
        public SecondController(ILoggerFactory factory,
            ILogger<SecondController> logger,
            ITestServiceA testServiceA,
            ITestServiceB testServiceB,
            ITestServiceC testServiceC,
            ITestServiceD testServiceD,
            IUserService userService,
            IA a)
        {
            this._Factory = factory;
            this._logger = logger;
            this._ITestServiceA = testServiceA;
            this._ITestServiceB = testServiceB;
            this._ITestServiceC = testServiceC;
            this._ITestServiceD = testServiceD;
            this._IUserService = userService;
            this._IA = a;
        }
        #endregion


        public IActionResult Index()
        {
            this._logger.LogError("这里是ILogger<SecondController> Error");
            this._Factory.CreateLogger<SecondController>().LogError("这里是ILoggerFactory Error");

            this._logger.LogWarning($"_ITestServiceA={this._ITestServiceA.GetHashCode()}");
            this._logger.LogWarning($"_ITestServiceB={this._ITestServiceB.GetHashCode()}");
            this._logger.LogWarning($"_ITestServiceC={this._ITestServiceC.GetHashCode()}");
            this._logger.LogWarning($"_ITestServiceD={this._ITestServiceD.GetHashCode()}");

            this._ITestServiceB.Show();

            this._IA.Show(123, "走自己的路");


            return View();
        }

        public IActionResult Info()
        {
            //using (JDDbContext dbContext = new JDDbContext())
            //{
            //    var list = dbContext.Users.Where(u => u.Id > 10 && u.Id < 50);
            //    base.ViewBag.Users = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            //}
            var list = this._IUserService.Query<User>(u => u.Id > 10 && u.Id < 50);
            base.ViewBag.Users = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            return View();
        }

    }
}