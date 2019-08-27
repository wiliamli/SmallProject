using System;

//namespace Sharp.Utilities.Extensions
//{
    public static class YJUtilityDateTimeExtensions
    {
        /// <summary>
        /// 获取本周第一天
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="whichFirst">哪一天的一周的开始</param>
        /// <returns></returns>

        public static DateTime GetWeekFirstDay(this DateTime dt, DayOfWeek whichFirst = DayOfWeek.Monday)
        {
            return dt.AddDays((int)whichFirst - (int)dt.DayOfWeek);
        }


        /// <summary>
        /// 获取本周的最后一天
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="whichFirst">哪一天为一周的开始</param>
        /// <returns></returns>
        public static DateTime GetWeekLastDay(this DateTime dt, DayOfWeek whichFirst = DayOfWeek.Monday)
        {
            return dt.GetWeekFirstDay(whichFirst).AddDays(6);
        }

        /// <summary>
        /// 获取下周的第一天
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="whichFirst">哪一天为一周的开始</param>
        /// <returns></returns>
        public static DateTime GetNextWeekLastDay(this DateTime dt, DayOfWeek whichFirst = DayOfWeek.Monday)
        {
            return dt.GetWeekFirstDay(whichFirst).AddDays(14);
        }

        /// <summary>
        /// 获取本月的第一天
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>

        public static DateTime GetMonthFirstDay(this DateTime dt)
        {
            return dt.AddDays(1 - dt.Day);
        }

        /// <summary>
        /// 获取本月的最后一天
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime GetMonthLastDay(this DateTime dt)
        {
            return dt.GetMonthFirstDay().AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// 获取下一个月的第一天
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime GetNextMonthFirstDay(this DateTime dt)
        {
            return dt.GetMonthFirstDay().AddMonths(1);
        }

        /// <summary>
        /// 获取下一个指定的周几的日期    
        /// 如2016/12/27为周2, 获取下一个周2,2017/1/3,下一个周3就是2016/12/28
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime GetNextSpecificDayOfWork(this DateTime dt, DayOfWeek dayOfWeek)
        {

            var offset = dayOfWeek - dt.DayOfWeek;
            //if (offset == 0)
            //    return dt;
            if (offset > 0)
                return dt.AddDays(offset);
            else
                return dt.AddDays(7 + offset);
        }

        /// <summary>
        /// 获取下一个指定的周几的日期,包含自身  
        /// 如2016/12/27为周2, 获取下一个周2,2016/12/27,下一个周3就是2016/12/28
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime GetNextSpecificDayOfWorkContainSelf(this DateTime dt, DayOfWeek dayOfWeek)
        {
            if (dt.DayOfWeek == dayOfWeek)
                return dt;

            var offset = dayOfWeek - dt.DayOfWeek;
            //if (offset == 0)
            //    return dt;
            if (offset > 0)
                return dt.AddDays(offset);
            else
                return dt.AddDays(7 + offset);
        }

        /// <summary>
        /// 返回下一个指定的号
        /// 如2016/12/27 ,获取下一个20号:2017/1/20
        /// 2017/1/31 获取下一个31号:2017/3/31
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dayOfMonth"></param>
        /// <returns></returns>
        public static DateTime GetNextSpecificDayOfMonth(this DateTime dt, int dayOfMonth)
        {
            if (dayOfMonth < 1 && dayOfMonth > 31)
                throw new ArgumentException("dayofMonth 只能为1~31之前的整数");

            int offset = dayOfMonth - dt.Day;

            var d = dt.AddDays(offset);

            if (offset > 0)
            {
                if (d.Month == dt.Month)
                    return d;

                d = new DateTime(dt.Year, dt.Month, 1).AddMonths(1);
                int i = 0;
                while (true)
                {
                    try
                    {
                        var dd = d.AddDays(dayOfMonth).AddMonths(i++);
                        return dd;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        continue;
                    }
                }
            }
            else
            {
                int i = 1;
                while (true)
                {
                    var next = d.AddMonths(i++);
                    if (next.Day == dayOfMonth)
                        return next;
                }
            }
        }

        /// <summary>
        /// 返回下一个指定的号,包含自身
        /// 如2016/12/27 ,获取下一个20号:2017/1/20
        /// 2017/1/31 获取下一个31号:2017/1/31
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dayOfMonth"></param>
        /// <returns></returns>
        public static DateTime GetNextSpecificDayOfMonthContainSelf(this DateTime dt, int dayOfMonth)
        {
            if (dayOfMonth < 1 && dayOfMonth > 31)
                throw new ArgumentException("dayofMonth 只能为1~31之前的整数");

            if (dt.Day == dayOfMonth)
                return dt;

            int offset = dayOfMonth - dt.Day;

            var d = dt.AddDays(offset);

            if (offset > 0)
            {
                if (d.Month == dt.Month)
                    return d;

                d = new DateTime(dt.Year, dt.Month, 1).AddMonths(1);
                int i = 0;
                while (true)
                {
                    try
                    {
                        var dd = d.AddDays(dayOfMonth).AddMonths(i++);
                        return dd;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        continue;
                    }
                }
            }
            else
            {
                int i = 1;
                while (true)
                {
                    var next = d.AddMonths(i++);
                    if (next.Day == dayOfMonth)
                        return next;
                }
            }
        }

        public static string ToStringyyyyMMddHHmmss(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public static string ToStringyyyyMMddHHmm(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm");
        }

        /// <summary>
        /// 判断两个日期是否是同一天
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static bool IsSameDay(this DateTime d1, DateTime d2)
        {
            return d1.Date == d2.Date;
        }
    }

//}