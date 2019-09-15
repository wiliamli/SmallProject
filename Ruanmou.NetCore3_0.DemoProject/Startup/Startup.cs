using System.IO;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Ruanmou.Core.Utility;
using Ruanmou.NetCore3_0.DemoProject.Utility;
using Ruanmou.Core.Utility.Middleware;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using YJ.PlatFormCore.Web.Startup;
using Ruanmou04.NetCore.Project.Utility.Middleware;

namespace Ruanmou.NetCore3_0.DemoProject
{
    public class Startup
    {
        private const string _defaultCorsPolicyName = "localhost";
        /// <summary>
        /// 自有妙用
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            _Configuration = configuration;
            StaticConstraint.Init(s => configuration[s]);

        }

        IConfiguration _Configuration { get; }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddRazorRuntimeCompilation();

            services.AddControllersWithViews();
            services.AddRazorPages();//约等于AddMvc() 就是3.0把内容拆分的更细一些，能更小的依赖
            services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();
            services.AddCors(
                options => options.AddPolicy(
                    _defaultCorsPolicyName,
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                )
            );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "1.0.0.0",
                    Title = "Ruanmou",
                    Description = "Ruanmou ASP.NET Core Web API"
                });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Ruanmou.Web.xml");
                c.IncludeXmlComments(xmlPath);
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options => options.Cookie.Path = "/");
            AuthConfigurer.Configure(services, _Configuration);
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule<CustomAutofacModule>();
            //ProjectModule.Init(containerBuilder, _Configuration);
            //var token= provider.GetService(typeof(TokenAuthConfiguration));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region Middleware
            //app.Run(c => c.Response.WriteAsync("Hello World!"));
            //////任何请求来了，只是返回个hello world    终结式
            //////所谓Run终结式注册，其实只是一个扩展方法，最终还不是得调用Use方法，

            ////IApplicationBuilder 应用程序的组装者
            ////RequestDelegate:传递一个HttpContext，异步操作下，不返回；也就是一个处理动作
            //// Use(Func<RequestDelegate, RequestDelegate> middleware) 委托，传入一个RequestDelegate，返回一个RequestDelegate
            ////ApplicationBuilder里面有个容器IList<Func<RequestDelegate, RequestDelegate>> _components
            ////Use就只是去容器里面添加个元素
            ////最终会Build()一下， 如果没有任何注册，就直接404处理一切
            ///*
            // foreach (var component in _components.Reverse())//反转集合  每个委托拿出来
            //{
            //    app = component.Invoke(app);
            //    //委托3-- 404作为参数调用，返回 委托3的内置动作--作为参数去调用委托(成为了委托2的参数)--循环下去---最终得到委托1的内置动作---请求来了HttpContext---
            //}
            // */
            ////IApplicationBuilder build之后其实就是一个RequestDelegate，能对HttpContext加以处理
            ////默认情况下，管道是空的，就是404；可以根据你的诉求，任意的配置执行，一切全部由开发者自由定制，框架只是提供了一个组装方式

            ////Func<RequestDelegate, RequestDelegate> middleware = next =>
            ////{
            ////    return new RequestDelegate(async context =>
            ////                    {
            ////                        await context.Response.WriteAsync("<h3>This is Middleware1 start</h3>");
            ////                        await Task.CompletedTask;
            ////                        await next.Invoke(context);//RequestDelegate--需要context返回Task
            ////                        await context.Response.WriteAsync("<h3>This is Middleware1 end</h3>");
            ////                    });
            ////};
            ////app.Use(middleware);

            //app.Use(next =>
            //{
            //    System.Diagnostics.Debug.WriteLine("this is Middleware1");
            //    return new RequestDelegate(async context =>
            //    {
            //        await context.Response.WriteAsync("<h3>This is Middleware1 start</h3>");
            //        await next.Invoke(context);
            //        await context.Response.WriteAsync("<h3>This is Middleware1 end</h3>");
            //    });
            //});

            //app.Use(next =>
            //{
            //    System.Diagnostics.Debug.WriteLine("this is Middleware2");
            //    return new RequestDelegate(async context =>
            //    {
            //        await context.Response.WriteAsync("<h3>This is Middleware2 start</h3>");
            //        await next.Invoke(context);
            //        await context.Response.WriteAsync("<h3>This is Middleware2 end</h3>");
            //    });
            //});
            //app.Use(next =>
            //{
            //    System.Diagnostics.Debug.WriteLine("this is Middleware3");
            //    return new RequestDelegate(async context =>
            //    {
            //        await context.Response.WriteAsync("<h3>This is Middleware3 start</h3>");
            //        //await next.Invoke(context);//注释掉，表示不再往下走
            //        await context.Response.WriteAsync("<h3>This is Middleware3 end</h3>");
            //    });
            //});

            //////1 Run 终结式  只是执行，没有去调用Next  
            //////一般作为终结点
            ////app.Run(async (HttpContext context) =>
            ////{
            ////    await context.Response.WriteAsync("Hello World Run");
            ////});
            ////app.Run(async (HttpContext context) =>
            ////{
            ////    await context.Response.WriteAsync("Hello World Run Again");
            ////});

            //////2 Use表示注册动作  不是终结点  
            //////执行next，就可以执行下一个中间件   如果不执行，就等于Run
            ////app.Use(async (context, next) =>
            ////{
            ////    await context.Response.WriteAsync("Hello World Use1 <br/>");
            ////    await next();
            ////    await context.Response.WriteAsync("Hello World Use1 End <br/>");
            ////});
            ////app.Use(async (context, next) =>
            ////{
            ////    await context.Response.WriteAsync("Hello World Use2 Again <br/>");
            ////    await next();
            ////});
            //////UseWhen可以对HttpContext检测后，增加处理环节;原来的流程还是正常执行的
            ////app.UseWhen(context =>
            ////{
            ////    return context.Request.Query.ContainsKey("Name");
            ////},
            ////appBuilder =>
            ////{
            ////    appBuilder.Use(async (context, next) =>
            ////    {
            ////        await context.Response.WriteAsync("Hello World Use3 Again Again Again <br/>");
            ////        await next();
            ////    });
            ////});

            ////app.Use(async (context, next) =>//没有调用 next() 那就是终结点  跟Run一样
            ////{
            ////    await context.Response.WriteAsync("Hello World Use3  Again Again <br/>");
            ////    //await next();
            ////});

            //////Map：根据条件指定中间件  指向终结点，没有Next
            //////最好不要在中间件里面判断条件选择分支；而是一个中间件只做一件事儿，多件事儿就多个中间件
            ////app.Map("/Test", MapTest);
            ////app.Map("/Eleven", a => a.Run(async context =>
            ////{
            ////    await context.Response.WriteAsync($"This is Advanced Eleven Site");
            ////}));
            ////app.MapWhen(context =>
            ////{
            ////    return context.Request.Query.ContainsKey("Name");
            ////    //拒绝非chorme浏览器的请求  
            ////    //多语言
            ////    //把ajax统一处理
            ////}, MapTest);

            ////Middleware类
            //app.UseMiddleware<ElevenStopMiddleware>();
            //app.UseMiddleware<ElevenMiddleware>();
            //app.UseMiddleware<ElevenSecondMiddleware>();
            ////app.Use(async (context, next) =>
            ////{
            ////    await context.Response.WriteAsync("Hello World Use3  Again Again <br/>");
            ////});
            #endregion

            //app.UseMiddleware<AuthorizeMiddleware>();
            if (env.IsDevelopment())
            {
                app.UseMiddleware<ExceptionHandlingMiddleware>();

                //app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseCors(_defaultCorsPolicyName);
            app.UseRouting();

            //app.UseAuthorize();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                       name: "areas",
                       areaName: "System",
                       pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });//终结点，可能是mvc 也可能是别的项目类型  signalr


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Ruanmou Web API");

            });
        }
    }
}
