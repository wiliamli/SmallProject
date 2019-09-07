using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Service
{

    /// <summary>
    /// 模块父类,执行顺序:PreInit->系统注册程序集->Inited->ConfigAutoMap
    /// </summary>
    public abstract class RuanMouModule
    {
        /// <summary>
        /// 初始化之前,
        /// 在系统注册当前程序集之前
        /// </summary>
        /// <param name="iocManager"></param>
        public virtual void OnPreInit(IContainer container)
        {

        }

        /// <summary>
        /// 配置automap映射,AutoMap特性只能满足简单的配置需求,一些复杂的配置需要通过该方法统一配置
        /// </summary>
        /// <param name="configuration"></param>
        //public virtual void ConfigAutoMap(IMapperConfigurationExpression configuration)
        //{

        //}

        /// <summary>
        /// 系统初始化之后,可用于重新注册ioc,用于替换已注册的类或接口,该方法的调用在PreInit之后以及系统初始化,比如需要替换平台的绩效实现
        /// </summary>
        /// <param name="iocManager"></param>
        public virtual void OnInited(IContainer container)
        {

        }
    }
}
