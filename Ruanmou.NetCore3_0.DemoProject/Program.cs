using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ruanmou.NetCore3_0.DemoProject
{
    /// <summary>
    /// 1 Asp.NetCore3.0Preview7环境配置，项目迁移
    /// 2 Razor动态编译，TempData序列化，添加区域
    /// 3 中间件源码解读，理解新管道处理模型
    /// 4 autofac新模式
    /// 5 EntityFrameworkCore3.0使用&封装&扩展日志
    /// 6 .NetCore3.0 WebApi开发应用，前后分离
    /// 
    /// 新环境配置：Asp.NetCore3.0 Preview7
    /// 暂时不适合直接上线项目，所以课程主要学习是2.2
    /// 1 只能是VS2019---VS2019下载地址和激活码
    /// 2 .NetCore3.0是需要独立安装运行时环境CLR
    ///   dotnet-hosting-3.0.0-preview7.19365.7-win.exe
    ///   这里还包含IIS需要的Module
    /// 3  SDK软件开发工具包--VS才有对应的模板
    ///   dotnet-sdk-3.0.100-preview7-012821-win-x64-.exe
    ///   工具--选项--环境---预览功能--Preview
    /// 
    /// .NetCore为什么可以跨平台？
    /// 因为微软出了一套可以在Linux运行的CLR
    /// .NetFramework里面CLR更新慢一些，Core的CLR变化很快
    /// 
    /// Core2.2是正式版，现在讲的3.0是预览版，会有蛮多的改动
    /// 计划帮大家完成从2.2到3.0的升级，解决变化，深入一下原理，
    /// 包括EFCore  Core的WebApi  搭建一套3.0的框架
    /// 此外，还有一个福利，
    /// 准备邀请学员基于3.0的框架完成一个真实项目，老师出钱(几千)
    /// 能真实基于Core做个项目，然后我来上线
    /// 
    /// Razor cshtml应该是在访问是会动态编译
    /// 3.0默认是没有动态编译---nuget添加AddRazorRuntimeCompilation--startup configservice添加下--
    /// 
    /// tempdata序列化时，只能是基础类型+int集合+字典，不能是自定义类型
    /// 
    /// 提供AspNetCore-master源码.zip是可以直接查看源代码
    /// 也可以通过https://github.com/aspnet/AspNetCore去查看
    /// 
    /// 区域添加一下：
    /// MVC区分区域是通过命名空间,这里不行
    /// 需要在控制器上面添加[Area("System")] [Route("System/[controller]/[action]")]   每个控制器都需要，所以可以继承个BaseAreaController
    /// 
    /// 把Core2.2的类库 给迁移到Core3.0  修改targetframework，更新引用
    /// 
    /// 管道处理模型--中间件
    /// Startup--Config里面去指定了Http请求管道
    /// 何谓http请求的管道呢？
    /// 就是对Http请求的一连串的处理过程
    /// 就是给你一个HttpContext，然后一步步的处理，最终的得到结果
    /// 
    /// Asp.Net请求管道： 请求最终会由一个具体的HttpHandler处理(page/ashx/mvchttphandler--action)
    ///                   但是还有多个步骤，被封装成事件--可以注册可以扩展--IHttpModule--提供了非常优秀的扩展性
    /// 有一个缺陷：太多管闲事儿了--一个http请求最核心是IHttpHandler--cookie Session  Cache NeginRequest endrequest maprequesthandler 授权----这些不一定非得有---但是写死了---默认认为那些步骤是必须的---跟框架的设计思想有关---.Net入门简单精通难---因为框架大包大揽，全家桶式，随便拖一下控件，写点数据库，一个项目就出来了---所以精通也难----也要付出代价，就是包袱比较重，不能轻装前行---.NetCore是一套全新的平台，已经不再向前兼容---设计更追求组件化，追求高性能---没有全家桶        
    /// 
    /// Asp.NetCore全新的请求管道：
    /// 默认情况，管道只有一个404
    /// 然后你可以增加请求的处理(UseEndPoint)---这就是以前handler，只包含业务处理
    /// 其他的就是中间件middleware
    /// 
    /// 
    /// 1 autofac新模式
    /// 2 EntityFrameworkCore3.0使用&封装&扩展日志
    /// 3 .NetCore3.0 WebApi开发应用，前后分离
    /// 
    /// 替换容器时，升级了
    /// a nuget--可以参考依赖项里面的autofac相关
    /// b UseServiceProviderFactory
    /// c ConfigureContainer(ContainerBuilder containerBuilder)
    /// 
    /// EntityFrameworkCore3.0
    /// 没有edmx，一般是code first  也没有自动创建
    /// 
    /// 先nuget一下efcore+efcoresqlserver
    /// a 从Framework生成实体+context，然后复制粘贴
    /// b JDDbContext构造函数不指定链接
    /// c protected override void OnConfiguring 添加链接
    /// d protected override void OnModelCreating改了个参数类型
    /// 
    /// 关于链接问题，肯定放在配置文件，配置文件怎么读取
    /// 1 内部写死度appsetting--不好 路径可能变化
    /// 2 dbcontext注入IConfiguration---没问题
    /// 3 关于配置文件，如果要用配置项，我们是不是都得注入IConfiguration，
    ///   但是一些静态方法需要使用配置文件
    ///   以前.NetFramework会把配置文件集中管理，做成静态，使用的时候直接拿，
    ///   还是一个StaticConstraint，然后在startup环境传递委托完成初始化
    ///   
    /// .NetCore不再出现静态，需要的话就通过单例模式
    /// 就可以从上到下保持全程依赖注入(改造不小)
    /// 
    /// WebApi:
    /// 直接添加了WebApi控制器,
    /// 这里不需要添加额外的路由，直接靠特性路由
    /// [Route("api/[controller]"), ApiController]  都不能少
    /// 此外，就是具体的action可能需要具体的特性路由
    /// 
    /// btnGet5的时候失败，大家自行研究！   
    /// 谁先搞定的（私聊给我为准，不允许群里发答案），前3名，8.88
    /// 
    /// WebApi的管道和MVC合并了，是一个了
    /// 权限Filter 异常的Filter 是通用的
    /// 
    /// .NetCore其实不难，有问题解决问题，可以搞定的
    /// 差实践！
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
             .UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }
}
