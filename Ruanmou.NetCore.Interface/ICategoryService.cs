
using Ruanmou.EFCore3_0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.NetCore.Interface
{
    /// <summary>
    /// Category增删改查
    /// </summary>
    public interface ICategoryService : IBaseService
    {
        void Show();
        #region Query
        /// <summary>
        /// 用code获取当前类及其全部子孙类别的id
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        IEnumerable<int> GetDescendantsIdList(string code);

        /// <summary>
        /// 根据类别编码找子类别集合  找一级类用默认code  root
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        IEnumerable<Category> GetChildList(string code = "root");

        /// <summary>
        /// 查询并缓存全部的类别数据
        /// 类别数据一般是不变化的
        /// </summary>
        /// <returns></returns>
        List<Category> CacheAllCategory();
        #endregion Query

    }
}
