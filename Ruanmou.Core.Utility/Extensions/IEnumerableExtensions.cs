using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Ruanmou04.Core.Utility.Extensions
{
    /// <summary>
    /// IEnumerable扩展
    /// </summary>
    public static class IEnumerableExtensions
    {
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            HashSet<T> hs = new HashSet<T>();
            source.ForEach_(r => hs.Add(r));
            return hs;
        }
        public static string Join<T>(this IEnumerable<T> source, string separator)
        {
            return string.Join<T>(separator, source);
        }

        public static void ForEach_<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var item in list)
            {
                action(item);
            }
        }

        public static IReadOnlyList<T> ToReadonlyList<T>(this IEnumerable<T> list)
        {
            return list.ToImmutableList();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return list == null || !list.Any();
        }

        public static bool HasRepeat<T>(this IEnumerable<T> list)
        {
            return list.GroupBy(r => r).Any(g => g.Count() > 1);
        }

        public static bool HasRepeat<T>(this IEnumerable<T> list, Func<T, object> selectProperty)
        {
            return list.GroupBy(selectProperty).Any(g => g.Count() > 1);
        }

        /// <summary>
        /// 是否有效，即至少有一个元素
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source">源对象集合</param>
        /// <returns>成功或失败</returns>
        public static bool HasAny<TSource>(this IEnumerable<TSource> source)
        {
            return (source != null && source.Any());
        }
    }
}