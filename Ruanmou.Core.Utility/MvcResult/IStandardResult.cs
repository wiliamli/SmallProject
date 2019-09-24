using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.Core.Utility.MvcResult
{
    public interface IStandardResult
    {
        bool Success { get; set; }

        string Message { get; set; }
    }

    public interface IStandardResult<T> : IStandardResult
    {
        T Data { get; set; }
    }
}
