using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.Core.Utility.Extensions
{
    /// <summary>
    /// 处理未知JSON或者只对JSON里面部分对象或属性操作时可用
    /// </summary>
    public static class JObjectExtensions
    {
        /// <summary>
        /// 通过KEY和KEYVALUE获取JSON里面的对象数组，可keyvalue可为空，表示找到所有有包含key属性的对象
        /// </summary>
        /// <param name="jsonObj"></param>
        /// <param name="key">需要查找的属性</param>
        /// <param name="keyValue">需要查找属性的值</param>
        /// <param name="notKey">不包含某个KEY</param>
        /// <param name="hasKey">包含某个KEY</param>
        /// <returns></returns>
        public static JArray GetJArrByJsonObj(this JObject jsonObj, string key, string keyValue = null, string notKey = null, string hasKey = null)
        {
            var arr = new JArray();

            foreach (JProperty jProperty in jsonObj.Properties())
            {
                var a = jsonObj[jProperty.Name].GetType();
                if (a == typeof(JValue))
                {
                    if (!notKey.IsNullOrEmpty() && !hasKey.IsNullOrEmpty())
                    {
                        if (jProperty.Name == key && !keyValue.IsNullOrEmpty() && jProperty.Value.Value<string>() == keyValue && jsonObj.SelectToken(notKey) == null && jsonObj.SelectToken(hasKey) != null)
                        {
                            arr.Add(jsonObj.DeepClone());
                        }

                        if (jProperty.Name == key && keyValue.IsNullOrEmpty() && jsonObj.SelectToken(notKey) == null && jsonObj.SelectToken(hasKey) != null)
                        {
                            arr.Add(jsonObj.DeepClone());
                        }
                    }


                    if (!notKey.IsNullOrEmpty() && hasKey.IsNullOrEmpty())
                    {
                        if (jProperty.Name == key && !keyValue.IsNullOrEmpty() && jProperty.Value.Value<string>() == keyValue && jsonObj.SelectToken(notKey) == null)
                        {
                            arr.Add(jsonObj.DeepClone());
                        }

                        if (jProperty.Name == key && keyValue.IsNullOrEmpty() && jsonObj.SelectToken(notKey) == null)
                        {
                            arr.Add(jsonObj.DeepClone());
                        }
                    }


                    if (notKey.IsNullOrEmpty() && !hasKey.IsNullOrEmpty())
                    {
                        if (jProperty.Name == key && !keyValue.IsNullOrEmpty() && jProperty.Value.Value<string>() == keyValue && jsonObj.SelectToken(hasKey) != null)
                        {
                            arr.Add(jsonObj.DeepClone());
                        }

                        if (jProperty.Name == key && keyValue.IsNullOrEmpty() && jsonObj.SelectToken(hasKey) != null)
                        {
                            arr.Add(jsonObj.DeepClone());
                        }
                    }

                    if (notKey.IsNullOrEmpty() && hasKey.IsNullOrEmpty())
                    {
                        if (jProperty.Name == key && !keyValue.IsNullOrEmpty() && jProperty.Value.Value<string>() == keyValue)
                        {
                            arr.Add(jsonObj.DeepClone());
                        }

                        if (jProperty.Name == key && keyValue.IsNullOrEmpty())
                        {
                            arr.Add(jsonObj.DeepClone());
                        }
                    }

                }
                if (a == typeof(JArray))
                {
                    var jarrobjs = jProperty.Value.Value<JArray>();

                    foreach (var jarrobj in jarrobjs)
                    {
                        if (jarrobj.GetType() == typeof(JObject))
                        {
                            foreach (JObject jarrobjrt in ((JObject)jarrobj).GetJArrByJsonObj(key, keyValue))
                            {
                                arr.Add(jarrobjrt);
                            }
                        }


                    }
                }
                if (a == typeof(JObject))
                {
                    var jarrobj = jProperty.Value.Value<JObject>();
                    foreach (JObject jarrobjrt in jarrobj.GetJArrByJsonObj(key, keyValue))
                    {
                        arr.Add(jarrobjrt);
                    }
                }
            }

            return arr;
        }

        /// <summary>
        /// 通过Key和Key的值找到对应的JSON对象，给该对象新增一个List的JProperty
        /// </summary>
        /// <param name="jsonObj"></param>
        /// <param name="key">需要查找的属性</param>
        /// <param name="keyValue">需要查找属性的值</param>
        /// <param name="jPropertys">增加的属性集合</param>
        /// <returns></returns>
        public static bool SetJPropertyToJsonObj(this JObject jsonObj, string key, string keyValue, List<JProperty> jPropertys)
        {
            var returnValue = false;

            if (jsonObj.SelectToken(key) != null && jsonObj.SelectToken(key).Value<string>() == keyValue)
            {
                foreach (var jProperty in jPropertys)
                {
                    if (jsonObj.SelectToken(jProperty.Name) != null)
                    {
                        jsonObj[jProperty.Name] = jProperty.Value;
                    }
                    else
                    {
                        jsonObj.Add(jProperty);
                    }
                }


                returnValue = true;
            }
            if (!returnValue)
            {
                foreach (JProperty jProperty in jsonObj.Properties())
                {
                    var a = jsonObj[jProperty.Name].GetType();
                    if (a == typeof(JArray))
                    {
                        var jarrobjs = jProperty.Value.Value<JArray>();
                        foreach (JObject jarrobj in jarrobjs)
                        {
                            returnValue = jarrobj.SetJPropertyToJsonObj(key, keyValue, jPropertys);
                            if (returnValue) return returnValue;
                        }
                    }
                    if (a == typeof(JObject))
                    {
                        var jarrobj = jProperty.Value.Value<JObject>();
                        returnValue = jarrobj.SetJPropertyToJsonObj(key, keyValue, jPropertys);
                        if (returnValue) return returnValue;
                    }
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 通过key和keyvalue找到JSON对象，并移除一组属性,keyvalue可为空表示移除所有包含key的一组属性
        /// </summary>
        /// <param name="jsonObj"></param>
        /// <param name="key">需要查找的属性</param>
        /// <param name="keys">需要查找属性的值</param>
        /// <param name="keyValue">需要查找的属性的值</param>
        /// <returns></returns>
        public static bool RemoveJPropertyToJsonObj(this JObject jsonObj, string key, List<string> keys, string keyValue = null)
        {
            var returnValue = false;

            if (jsonObj.SelectToken(key) != null)
            {
                if (keyValue.IsNullOrEmpty())
                {
                    foreach (var k in keys)
                    {
                        if (jsonObj.SelectToken(k) != null)
                        {
                            returnValue = jsonObj.Remove(k);

                        }
                    }
                }
                else
                {
                    if (jsonObj.SelectToken(key).Value<string>() == keyValue)
                    {
                        foreach (var k in keys)
                        {
                            if (jsonObj.SelectToken(k) != null)
                            {
                                returnValue = jsonObj.Remove(k);
                            }
                        }
                    }
                }
            }

            foreach (JProperty jProperty in jsonObj.Properties())
            {
                var a = jsonObj[jProperty.Name].GetType();
                if (a == typeof(JArray))
                {
                    var jarrobjs = jProperty.Value.Value<JArray>();
                    foreach (JObject jarrobj in jarrobjs)
                    {
                        returnValue = jarrobj.RemoveJPropertyToJsonObj(key, keys, keyValue) ? true : returnValue;
                    }
                }
                if (a == typeof(JObject))
                {
                    var jarrobj = jProperty.Value.Value<JObject>();
                    returnValue = jarrobj.RemoveJPropertyToJsonObj(key, keys, keyValue) ? true : returnValue;

                }
            }

            return returnValue;
        }

    }
}
