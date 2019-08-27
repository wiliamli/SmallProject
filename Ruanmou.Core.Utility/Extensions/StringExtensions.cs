using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace  Ruanmou04.Core.Utility.Extensions
{
    /// <summary>
    /// 字符串扩展类
    /// </summary>
    public static class MyExtensions
    {
        /// <summary>
        /// 是否为null或者空字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
        /// <summary>
        /// 是否Guid
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsGuid(this string str)
        {
            Guid guid;
            if (Guid.TryParse(str,out guid))
                return true;
            return false;
        }

        /// <summary>
        /// 字符串转Guid
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string str)
        {
            Guid guid;
            if (Guid.TryParse(str, out guid))
                return guid;
            return Guid.Empty;
        }
        /// <summary>
        /// 字符串比较 忽略大小写
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool EqualsIgnoreCase(this string source, string target)
        {
            return source.Equals(target, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 字符串分割为数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <param name="separators"></param>
        /// <returns></returns>
        public static List<T> Split<T>(this string str, params string[] separators) where T : struct
        {
            if (string.IsNullOrWhiteSpace(str))
                return new List<T>();
            return
                str
                .Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .Select(TypeExtensions.ConvertTo<T>).ToList();
        }

        /// <summary>
        /// 字符串分割为数组,跳过不能转换的异常数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <param name="separators"></param>
        /// <returns></returns>
        public static List<T> SplitSkipError<T>(this string str, params string[] separators) where T : struct
        {
            if (string.IsNullOrWhiteSpace(str))
                return new List<T>();
            var query =
                str
                .Split(separators, StringSplitOptions.RemoveEmptyEntries);

            List<T> result = new List<T>();
            foreach (var item in query)
            {
                try
                {
                    result.Add(item.ConvertTo<T>());
                }
                catch { }
            }
            return result;
        }

        /// <summary>
        /// 将相对路径转换为绝对路径
        /// 如: "~/bin/"  to "c:\web\bin\"
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        //public static string MapPath(this string relativePath)
        //{
        //    if (HostingEnvironment.IsHosted)
        //    {
        //        return HostingEnvironment.MapPath(relativePath);
        //    }

        //    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        //    relativePath = relativePath.Replace("~/", "").TrimStart('/').Replace('/', '\\');
        //    return Path.Combine(baseDirectory, relativePath);
        //}

        public static bool Contains(this string source, string value, StringComparison stringComparison)
        {
            if (source == null || value == null) { return false; }
            if (value == "") { return true; }
            return (source.IndexOf(value, stringComparison) >= 0);
        }

        /// <summary>
        /// 字符串截取,null时,返回"",不足长度时,返回字符串本身
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Substring(this string str, int length)
        {
            if (string.IsNullOrWhiteSpace(str))
                return "";

            if (str.Trim().Length <= length)
                return str.Trim();

            return str.Substring(0, length) + "...";
        }

        /// <summary>
        /// 字符串替换,忽略大小写,使用的是正则,注意正则中的特殊字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="pattern"></param>
        /// <param name="replaceString"></param>
        /// <returns></returns>
        public static string ReplaceIgnoreCase(this string str, string pattern, string replaceString)
        {
            return Regex.Replace(str, pattern, replaceString, RegexOptions.IgnoreCase);
        }
    }

}