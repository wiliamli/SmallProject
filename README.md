# ruanmouProject
软媒项目

#### 日记:
##### 1.2019-08-26  william
  * a.  添加了代码框架；论坛的数据库PD设计文档
  * b.加入swagger.
    如果使用当前swagger.core4.0.1，将提出异常MissingMethodException: Method not found: 'Void     Microsoft.AspNetCore.StaticFiles.StaticFileMiddleware..ctor(Microsoft.AspNetCore.Http.RequestDelegate
    于是在Nuget官网上找到swagger.core当前的最新版本5.0.0-rc2，相关组件同样升级到5.0.0-rc2，则该异常处理掉。
  * c.attribute问题，在.net core3.0下使用swagger，所有的http请求，都需要【显示】添加attribute
  * d.路由问题，不能使用area特性路由，否则无法生存swagger.json文档，则显示swagger错误
  
