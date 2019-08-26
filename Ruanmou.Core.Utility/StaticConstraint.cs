using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou.Core.Utility
{
    public class StaticConstraint
    {
        public static void Init(Func<string, string> func)
        {
            JDDbConnection = func.Invoke("ConnectionStrings:JDDbConnectionString");
            //循环--反射的方式初始化多个
        }


        /// <summary>
        /// 以前直接读配置文件
        /// ConnectionStrings:JDDbConnectionString
        /// </summary>
        public static string JDDbConnection = null;

    }
}
