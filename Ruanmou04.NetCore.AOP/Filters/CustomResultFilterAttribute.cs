using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruanmou.NetCore2.MVC6.Unitility.Filters
{
    /// <summary>
    /// Result的Filter
    /// </summary>
    public class CustomResultFilterAttribute : Attribute, IResultFilter
    {
        //private Logger logger = Logger.CreateLogger(typeof(CustomResultFilterAttribute));
        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine("OnResultExecuted Executed!");
            //logger.Info("OnResultExecuted Executed!");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine("OnResultExecuting Executing!");
            //logger.Info("OnResultExecuting Executing!");
        }
    }
}