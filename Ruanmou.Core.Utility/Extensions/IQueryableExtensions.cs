using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ruanmou04.Core.Utility.Extensions
{
    /// <summary>
    /// IQueryable扩展
    /// </summary>
    public static class IQueryableExtensions
    {
        ///// <summary>
        ///// where if
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="query"></param>
        ///// <param name="where"></param>
        ///// <param name="predicate"></param>
        ///// <returns></returns>
        //public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        //{
        //    if (condition)
        //        return query.Where(predicate);
        //    return query;
        //}

        public static IQueryable<T> Paging<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            return
                query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize);
        }
        public static IQueryable Paging(this IQueryable query, int pageIndex, int pageSize)
        {
            return
                query.Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize);
        }

        public static object First_(this IQueryable source)
        {
            if (source == null) throw new ArgumentNullException("source");
            return source.Provider.Execute(
                Expression.Call(
                    typeof(Queryable), "First",
                    new Type[] { source.ElementType },
                    source.Expression));
        }

        public static object FirstOrDefault_(this IQueryable source)
        {
            if (source == null) throw new ArgumentNullException("source");
            return source.Provider.Execute(
                Expression.Call(
                    typeof(Queryable), "FirstOrDefault",
                    new Type[] { source.ElementType },
                    source.Expression));
        }

        public static List<object> ToList_(this IQueryable<object> source)
        {
            return source.ToList();
        }

        public static object Single_(this IQueryable source)
        {
            if (source == null) throw new ArgumentNullException("source");
            return source.Provider.Execute(
                Expression.Call(
                    typeof(Queryable), "Single",
                    new Type[] { source.ElementType },
                    source.Expression));
        }

        public static object SingleOrDefault_(this IQueryable source)
        {
            if (source == null) throw new ArgumentNullException("source");
            return source.Provider.Execute(
                Expression.Call(
                    typeof(Queryable), "SingleOrDefault",
                    new Type[] { source.ElementType },
                    source.Expression));
        }

        /// <summary>
        /// 分页（带select查询转换函数）
        /// </summary>
        /// <typeparam name="TModel">模型类型</typeparam>
        /// <typeparam name="TDto">Dto类型</typeparam>
        /// <param name="query"></param>
        /// <param name="input">分页数据</param>
        /// <param name="selectFunc">查询转换函数</param>
        /// <returns></returns>
        public static PagedResult<TDto> Paging<TModel, TDto>(this IQueryable<TModel> query, PagingInput input, Func<TModel, TDto> selectFunc)
        {
            int count = query.Count();
            string sort = input.GetSortOrder();

            if (sort.IsNullOrEmpty())
                throw new ArgumentNullException("分页数据必须有排序列:请检查sort与order参数");
            var rows = query.OrderBy(sort)
                 .Skip((input.PageIndex - 1) * input.PageSize)
                 .Take(input.PageSize)
                 .Select(selectFunc)
                 .ToList();
            return new PagedResult<TDto>
            {
                Total = count,
                Rows = rows
            };
        }

        /// <summary>
        /// 分页异步（带select查询转换函数）
        /// </summary>
        /// <typeparam name="TModel">模型类型</typeparam>
        /// <typeparam name="TDto">Dto类型</typeparam>
        /// <param name="query"></param>
        /// <param name="input">分页数据</param>
        /// <param name="selectFunc">查询转换函数</param>
        /// <returns></returns>
        public static async Task<PagedResult<TDto>> PagingAsync<TModel, TDto>(this IQueryable<TModel> query, PagingInput input, Func<TModel, TDto> selectFunc)
        {
            int count = query.Count();
            string sort = input.GetSortOrder();

            if (sort.IsNullOrEmpty())
                throw new ArgumentNullException("分页数据必须有排序列:请检查sort与order参数");
            var rows = await query.OrderBy(sort)
                 .Skip((input.PageIndex - 1) * input.PageSize)
                 .Take(input.PageSize)
                 .Select(selectFunc)
                 .AsQueryable()
                 .ToListAsync();
            return new PagedResult<TDto>
            {
                Total = count,
                Rows = rows
            };
        }

        /// <summary>
        /// 分页（不带select查询转换函数）
        /// </summary>
        /// <typeparam name="TModel">模型类型</typeparam>
        /// <typeparam name="TDto">Dto类型</typeparam>
        /// <param name="query"></param>
        /// <param name="input">分页数据</param>
        /// <param name="selectFunc">查询转换函数</param> 
        /// <returns></returns>
        public static PagedResult<TModel> Paging<TModel>(this IQueryable<TModel> query, PagingInput input)
        {
            int count = query.Count();

            string sort = input.GetSortOrder();

            if (sort.IsNullOrEmpty())
                throw new ArgumentNullException("分页数据必须有排序列:请检查sort与order参数");
            var rows = query.OrderBy(sort)
                 .Skip((input.PageIndex - 1) * input.PageSize)
                 .Take(input.PageSize)
                 .ToList();
            return new PagedResult<TModel>
            {
                Total = count,
                Rows = rows
            };
        }

        /// <summary>
        /// 分页异步（不带select查询转换函数）
        /// </summary>
        /// <typeparam name="TModel">模型类型</typeparam>
        /// <typeparam name="TDto">Dto类型</typeparam>
        /// <param name="query"></param>
        /// <param name="input">分页数据</param>
        /// <param name="selectFunc">查询转换函数</param> 
        /// <returns></returns>
        public static async Task<PagedResult<TModel>> PagingAsync<TModel>(this IQueryable<TModel> query, PagingInput input)
        {
            int count = query.Count();

            string sort = input.GetSortOrder();

            if (sort.IsNullOrEmpty())
                throw new ArgumentNullException("分页数据必须有排序列:请检查sort与order参数");
            var rows = await query.OrderBy(sort)
                 .Skip((input.PageIndex - 1) * input.PageSize)
                 .Take(input.PageSize)
                 .ToListAsync();
            return new PagedResult<TModel>
            {
                Total = count,
                Rows = rows
            };
        }

        /// <summary>
        /// 分页（不带select查询转换函数,不执行ToList）
        /// </summary>
        /// <typeparam name="TModel">模型类型</typeparam>
        /// <typeparam name="TDto">Dto类型</typeparam>
        /// <param name="query"></param>
        /// <param name="input">分页数据</param>
        /// <param name="selectFunc">查询转换函数</param> 
        /// <returns>IQueryable<TModel></returns>
        public static IQueryable<TModel> PagingNonQuery<TModel>(this IQueryable<TModel> query, PagingInput input)
        {
            string sort = input.GetSortOrder();

            if (sort.IsNullOrEmpty())
                throw new ArgumentNullException("分页数据必须有排序列:请检查sort与order参数");

            return query.OrderBy(sort)
                     .Skip((input.PageIndex - 1) * input.PageSize)
                     .Take(input.PageSize);
        }
    }
}
