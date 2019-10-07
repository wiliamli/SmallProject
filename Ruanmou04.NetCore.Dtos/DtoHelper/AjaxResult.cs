

using System;

namespace Ruanmou04.EFCore.Dtos.DtoHelper
{
    /// <summary>
    /// ajax返回结果
    /// </summary>
    public class AjaxResult
    {
        public AjaxResult(string errorMsg)
        {
            //this.success = false;
            msg = errorMsg;
        }

        public AjaxResult(string successMsg, object result)
        {
            //this.success = true;
            msg = successMsg;
            data = result;
        }
        public AjaxResult(object result)
        {
            data = result;
        }
        public AjaxResult(string errorMsg, bool isSuccess)
        {
            this.success = isSuccess;
            this.msg = errorMsg;
        }
        public AjaxResult()
        {
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object data { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string msg { get; set; }

        public static AjaxResult Success(string msg, object data = null)
        {
            return new AjaxResult()
            {
                success = true,
                msg = msg,
                data = data
            };
        }

        public static AjaxResult Failure(string msg)
        {
            return new AjaxResult()
            {
                success = false,
                msg = msg,
            };
        }

        public void ExecuteAction(DataOperateType dataOperateType, Action action)
        {
            msg = GetOperateName(dataOperateType);
            try
            {
                action();
                success = true;
                msg += "成功";
            }
            catch (Exception exp)
            {
                success = false;
                msg += "失败";
                throw exp;
            }
        }
        private string GetOperateName(DataOperateType dataOperateType)
        {
            string operName = "操作";
            switch (dataOperateType)
            {
                case DataOperateType.Save:
                    operName = "保存";
                    break;
                case DataOperateType.Delete:
                    operName = "删除";
                    break;
                case DataOperateType.Query:
                    operName = "查询";
                    break;
            }
            return operName;
        }
    }
    /// <summary>
    /// 数据操作类型
    /// </summary>
    public enum DataOperateType
    {
        Save = 1,
        Delete = 2,
        Query = 3
    }
}
