using RM04.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ruanmou04.Core.Model.DtoHelper
{
    /// <summary>
    /// 生成表达式目录树  泛型缓存
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    public class DataMapping<TIn, TOut>
    {
        private static Func<TIn, TOut> _FUNC = null;
        static DataMapping()
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TIn), "p");
            List<MemberBinding> memberBindingList = new List<MemberBinding>();
            foreach (var item in typeof(TOut).GetProperties())
            {
                if (typeof(TIn).GetProperty(item.Name) == null)
                    continue;
                MemberExpression property = Expression.Property(parameterExpression, typeof(TIn).GetProperty(item.Name));
                MemberBinding memberBinding = Expression.Bind(item, property);
                memberBindingList.Add(memberBinding);
            }
            foreach (var item in typeof(TOut).GetFields())
            {
                if (typeof(TIn).GetField(item.Name) == null)
                    continue;
                MemberExpression property = Expression.Field(parameterExpression, typeof(TIn).GetField(item.Name));
                MemberBinding memberBinding = Expression.Bind(item, property);
                memberBindingList.Add(memberBinding);
            }
            MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TOut)), memberBindingList.ToArray());
            Expression<Func<TIn, TOut>> lambda = Expression.Lambda<Func<TIn, TOut>>(memberInitExpression, new ParameterExpression[]
            {
                    parameterExpression
            });
            _FUNC = lambda.Compile();//拼装是一次性的
        }
        public static TOut Trans(TIn t)
        {
            return _FUNC(t);
        }
    }
    /// <summary>
    /// 生成表达式目录树  泛型缓存
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    public static class DataMappingExtend
    {
        //private Func<TOut> FUNC<TOut>(this object source)= null;

        public static TOut MapTo<TIn,TOut>(this TIn source) where TIn :BaseEntity
        {
            Type tin= source.GetType();
            ParameterExpression parameterExpression = Expression.Parameter(tin, "p");
            List<MemberBinding> memberBindingList = new List<MemberBinding>();
            foreach (var item in typeof(TOut).GetProperties())
            {
                if (tin.GetProperty(item.Name) == null)
                    continue;
                MemberExpression property = Expression.Property(parameterExpression, typeof(TIn).GetProperty(item.Name));
                MemberBinding memberBinding = Expression.Bind(item, property);
                memberBindingList.Add(memberBinding);
            }
            foreach (var item in typeof(TOut).GetFields())
            {
                if (tin.GetField(item.Name) == null)
                    continue;
                MemberExpression property = Expression.Field(parameterExpression, typeof(TIn).GetField(item.Name));
                MemberBinding memberBinding = Expression.Bind(item, property);
                memberBindingList.Add(memberBinding);
            }
            MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TOut)), memberBindingList.ToArray());
            Expression<Func<TIn, TOut>> lambda = Expression.Lambda<Func<TIn, TOut>>(memberInitExpression, new ParameterExpression[]
            {
                    parameterExpression
            });
            var func= lambda.Compile();//拼装是一次性的
            return func.Invoke(source);
        }


    }
}
