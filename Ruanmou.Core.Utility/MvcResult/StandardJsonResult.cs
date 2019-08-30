﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou04.Core.Utility.MvcResult
{
    public class StandardJsonResult : IActionResult, IStandardResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public string StatusCode { get; set; }

        public string ContentType { get; set; }

        public StandardJsonResult()
        {
            ContentType = "application/json";
        }

        public void StandardAction(Action action)
        {
            try
            {
                action();
                Success = true;
            }
            catch (Exception ex)
            {
                Success = false;
                throw ex;
            }
        }

        public void ExecuteResult(ActionContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = ContentType;
            response.WriteAsync(JsonConvert.SerializeObject(ToJsonObject()), Encoding.UTF8);
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = ContentType;
            return response.WriteAsync(JsonConvert.SerializeObject(ToJsonObject()), Encoding.UTF8);
        }

        protected virtual IStandardResult ToJsonObject()
        {
            return new StandardResult
            {
                Success = Success,
                Message = Message
            };
        }
    }

    public class StandardJsonResult<T> : StandardJsonResult, IStandardResult<T>
    {
        public T Data { get; set; }

        protected override IStandardResult ToJsonObject()
        {
            return new StandardResult<T>
            {
                Success = Success,
                Message = Message,
                Data = Data
            };
        }
    }
}