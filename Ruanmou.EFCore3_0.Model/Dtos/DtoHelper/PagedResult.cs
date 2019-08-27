
using System.Collections.Generic;

namespace Ruanmou04.EFCore.Model.DtoHelper
{
    /// <summary>
    /// 分页结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResult<T>
    {
        public int Total { get; set; }
        public List<T> Rows { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

}