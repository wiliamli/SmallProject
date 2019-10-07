

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
            Message = errorMsg;
        }

        public AjaxResult(string successMsg, object result)
        {
            //this.success = true;
            Message = successMsg;
            data = result;
        }
        public AjaxResult(object result)
        {
            data = result;
        }
        public AjaxResult(string errorMsg, bool isSuccess)
        {
            this.Success = isSuccess;
            this.Message = errorMsg;
        }
        public AjaxResult()
        {
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object data { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }

        public static AjaxResult SuccessResult(string msg, object data = null)
        {
            return new AjaxResult()
            {
                Success = true,
                Message = msg,
                data = data
            };
        }

        public static AjaxResult Failure(string msg)
        {
            return new AjaxResult()
            {
                Success = false,
                Message = msg,
            };
        }

        public void ExecuteAction(DataOperateType dataOperateType, Action action)
        {
            Message = GetOperateName(dataOperateType);
            try
            {
                action();
                Success = true;
                Message += "成功";
            }
            catch (Exception exp)
            {
                Success = false;
                Message += "失败";
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
