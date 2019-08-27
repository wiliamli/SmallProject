using Ruanmou04.Core.Utility.Extensions;

namespace Ruanmou04.Core.Utility
{
    /// <summary>
    /// 分页输入,注意前端页面传过来时,sort和order为空的情况,需要给定sort和order一个默认值,否则无法分页查询
    /// </summary>
    public class PagingInput
    {
        /// <summary>
        /// 第几页,默认值1
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 每页大小,默认值20
        /// </summary>
        public int PageSize { get; set; } = 20;

        /// <summary>
        /// 排序字段 多个排序字段使用逗号分割,默认值"Id",如果实体没有Id字段,且没有输入排序字段将会异常
        /// </summary>
        public string Sort { get; set; } = "Id";

        /// <summary>
        /// 排序方式只能是desc或asc,默认值"asc"
        /// </summary>
        public string Order { get; set; } = "asc";

        /// <summary>
        /// 获取排序字符串如:"name desc,age asc"
        /// </summary>
        /// <returns></returns>
        public string GetSortOrder()
        {
            string sortOrder = "";
            var sortArr = Sort.Split(',');
            var orderArr = Order.Split(',');

            for (int i = 0; i < sortArr.Length; i++)
            {
                var order = (orderArr.Length - 1 >= i && (orderArr[i].EqualsIgnoreCase("desc") || orderArr[i].Equals("asc")) ? orderArr[i] : "asc");

                sortOrder += sortArr[i] + " " + order;
                if (i != sortArr.Length - 1)
                    sortOrder += ",";
            }

            return sortOrder;
        }
    }
}
