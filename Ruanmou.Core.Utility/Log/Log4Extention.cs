using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou.Core.Utility.Log
{
    public static class Log4Extention
    {
        public static void InitLog4(ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddFilter("System", LogLevel.Warning);
            loggingBuilder.AddFilter("Microsoft", LogLevel.Warning);//过滤掉系统默认的一些日志
            loggingBuilder.AddLog4Net();
        }
    }
}
