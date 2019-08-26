using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ruanmou.NetCore3_0.DemoProject.Models;

namespace Ruanmou.NetCore3_0.DemoProject.Controllers
{
    public class FirstController : Controller
    {
        public IActionResult Index(int? id)
        {
            base.ViewData["User1"] = new CurrentUser()
            {
                Id = 7,
                Name = "Y",
                Account = " ╰つ Ｈ ♥. 花心胡萝卜",
                Email = "莲花未开时",
                Password = "落单的候鸟",
                LoginTime = DateTime.Now
            };
            base.ViewData["Something"] = 12345;

            base.ViewBag.Name = "Eleven";
            base.ViewBag.Description = "Teacher";
            base.ViewBag.User = new CurrentUser()
            {
                Id = 7,
                Name = "IOC",
                Account = "限量版",
                Email = "莲花未开时",
                Password = "落单的候鸟",
                LoginTime = DateTime.Now
            };

            base.TempData["User"] = Newtonsoft.Json.JsonConvert.SerializeObject(new CurrentUser()
            {
                Id = 7,
                Name = "CSS",
                Account = "季雨林",
                Email = "KOKE",
                Password = "落单的候鸟",
                LoginTime = DateTime.Now
            });//要么就是做成字典 要么就序列化


            if (id == null)
            {
                return this.Redirect("~/First/TempDataPage");
            }
            else
                return View(new CurrentUser()
                {
                    Id = 7,
                    Name = "一点半",
                    Account = "季雨林",
                    Email = "KOKE",
                    Password = "落单的候鸟",
                    LoginTime = DateTime.Now
                });
        }

        public ActionResult TempDataPage()
        {
            base.ViewBag.User = base.TempData["User"];//可以拿到数据
            return View();
        }
    }
}