using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou.EFCore3_0.Model.SqlLog
{
    public class CustomEFLoggerFactory : ILoggerFactory
    {
        public void AddProvider(ILoggerProvider provider)
        {
            
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new CustomEFLogger(categoryName);
        }

        public void Dispose()
        {
           
        }
    }
}
