using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Ruanmou.NetCore3_0.DemoProject.Areas.System.Controllers
{
   
    [Area("System")]
    [Route("System/[controller]/[action]"), ApiController]
    public class HomeController : ControllerBase
    {
        public string Index()
        {
            return null;
        }
    }
}