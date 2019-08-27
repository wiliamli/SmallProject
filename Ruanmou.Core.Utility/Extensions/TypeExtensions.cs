using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace  Ruanmou04.Core.Utility.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// 判断给定的类型是否继承自genericType泛型类型
        /// </summary>
        /// <param name="givenType"></param>
        /// <param name="genericType"></param>
        /// <returns></returns>
        public static bool IsAssignableToGenericType(this Type givenType, Type genericType)
        {
            var interfaceTypes = givenType.GetInterfaces();

            foreach (var it in interfaceTypes)
            {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                    return true;
            }

            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
                return true;

            Type baseType = givenType.BaseType;
            if (baseType == null) return false;

            return IsAssignableToGenericType(baseType, genericType);
        }

        public static T ConvertTo<T>(this object value) where T : struct
        {
            if (value == null)
                return default(T);

            if (value is T)
                return (T)value;

            var destinationType = typeof(T);
            var sourceType = value.GetType();
            if (destinationType == typeof(bool) || destinationType == typeof(bool?))
                value = Convert.ToBoolean(value);

            TypeConverter destinationConverter = TypeDescriptor.GetConverter(destinationType);
            TypeConverter sourceConverter = TypeDescriptor.GetConverter(sourceType);
            if (destinationConverter != null && destinationConverter.CanConvertFrom(value.GetType()))
                return (T)destinationConverter.ConvertFrom(value);
            if (sourceConverter != null && sourceConverter.CanConvertTo(destinationType))
                return (T)sourceConverter.ConvertTo(value, destinationType);
            if (destinationType.IsEnum && value is int)
                return (T)Enum.ToObject(destinationType, (int)value);
            if (!destinationType.IsInstanceOfType(value))
                return (T)Convert.ChangeType(value, destinationType);
            return (T)value;
        }

        public static object ConvertTo(this object value, Type destinationType)
        {
            if (value == null)
                return null;

            var sourceType = value.GetType();
            if (destinationType == typeof(bool) || destinationType == typeof(bool?))
                return Convert.ToBoolean(value);

            TypeConverter destinationConverter = TypeDescriptor.GetConverter(destinationType);
            TypeConverter sourceConverter = TypeDescriptor.GetConverter(sourceType);
            if (destinationConverter != null && destinationConverter.CanConvertFrom(value.GetType()))
                return destinationConverter.ConvertFrom(value);
            if (sourceConverter != null && sourceConverter.CanConvertTo(destinationType))
                return sourceConverter.ConvertTo(value, destinationType);
            if (destinationType.IsEnum && value is int)
                return Enum.ToObject(destinationType, (int)value);
            if (!destinationType.IsInstanceOfType(value))
                return Convert.ChangeType(value, destinationType);

            throw new Exception($"[{value.GetType()}:{value}]转换为目标类型:[{destinationType}]无效!");
        }

        /// <summary>
        /// 安全不会异常的调用GetTypes()
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetTypesSafely(this Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(x => x != null);
            }
        }

        private static Dictionary<Type, List<PropertyInfo>> typePropertyCache = new Dictionary<Type, List<PropertyInfo>>();
        /// <summary>
        /// 将对象属性转换为字典存储
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IDictionary<string, object> ToDictionary(this object obj)
        {
            var dic = new Dictionary<string, object>();

            var type = obj.GetType();
            List<PropertyInfo> props;
            if (!typePropertyCache.TryGetValue(type, out props))
            {
                props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
                typePropertyCache.Add(type, props);
            }
            props.ForEach_(r =>
            {
                if (r != null)
                    dic[r.Name] = r.GetValue(obj);
            });

            return dic;
        }
    }
}
