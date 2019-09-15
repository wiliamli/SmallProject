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
        /// ��������
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
            services.AddRazorPages();//Լ����AddMvc() ����3.0�����ݲ�ֵĸ�ϸһЩ���ܸ�С������
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
            //////�κ��������ˣ�ֻ�Ƿ��ظ�hello world    �ս�ʽ
            //////��νRun�ս�ʽע�ᣬ��ʵֻ��һ����չ���������ջ����ǵõ���Use������

            ////IApplicationBuilder Ӧ�ó������װ��
            ////RequestDelegate:����һ��HttpContext���첽�����£������أ�Ҳ����һ��������
            //// Use(Func<RequestDelegate, RequestDelegate> middleware) ί�У�����һ��RequestDelegate������һ��RequestDelegate
            ////ApplicationBuilder�����и�����IList<Func<RequestDelegate, RequestDelegate>> _components
            ////Use��ֻ��ȥ����������Ӹ�Ԫ��
            ////���ջ�Build()һ�£� ���û���κ�ע�ᣬ��ֱ��404����һ��
            ///*
            // foreach (var component in _components.Reverse())//��ת����  ÿ��ί���ó���
            //{
            //    app = component.Invoke(app);
            //    //ί��3-- 404��Ϊ�������ã����� ί��3�����ö���--��Ϊ����ȥ����ί��(��Ϊ��ί��2�Ĳ���)--ѭ����ȥ---���յõ�ί��1�����ö���---��������HttpContext---
            //}
            // */
            ////IApplicationBuilder build֮����ʵ����һ��RequestDelegate���ܶ�HttpContext���Դ���
            ////Ĭ������£��ܵ��ǿյģ�����404�����Ը�������������������ִ�У�һ��ȫ���ɿ��������ɶ��ƣ����ֻ���ṩ��һ����װ��ʽ

            ////Func<RequestDelegate, RequestDelegate> middleware = next =>
            ////{
            ////    return new RequestDelegate(async context =>
            ////                    {
            ////                        await context.Response.WriteAsync("<h3>This is Middleware1 start</h3>");
            ////                        await Task.CompletedTask;
            ////                        await next.Invoke(context);//RequestDelegate--��Ҫcontext����Task
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
            //        //await next.Invoke(context);//ע�͵�����ʾ����������
            //        await context.Response.WriteAsync("<h3>This is Middleware3 end</h3>");
            //    });
            //});

            //////1 Run �ս�ʽ  ֻ��ִ�У�û��ȥ����Next  
            //////һ����Ϊ�ս��
            ////app.Run(async (HttpContext context) =>
            ////{
            ////    await context.Response.WriteAsync("Hello World Run");
            ////});
            ////app.Run(async (HttpContext context) =>
            ////{
            ////    await context.Response.WriteAsync("Hello World Run Again");
            ////});

            //////2 Use��ʾע�ᶯ��  �����ս��  
            //////ִ��next���Ϳ���ִ����һ���м��   �����ִ�У��͵���Run
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
            //////UseWhen���Զ�HttpContext�������Ӵ�����;ԭ�������̻�������ִ�е�
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

            ////app.Use(async (context, next) =>//û�е��� next() �Ǿ����ս��  ��Runһ��
            ////{
            ////    await context.Response.WriteAsync("Hello World Use3  Again Again <br/>");
            ////    //await next();
            ////});

            //////Map����������ָ���м��  ָ���ս�㣬û��Next
            //////��ò�Ҫ���м�������ж�����ѡ���֧������һ���м��ֻ��һ���¶�������¶��Ͷ���м��
            ////app.Map("/Test", MapTest);
            ////app.Map("/Eleven", a => a.Run(async context =>
            ////{
            ////    await context.Response.WriteAsync($"This is Advanced Eleven Site");
            ////}));
            ////app.MapWhen(context =>
            ////{
            ////    return context.Request.Query.ContainsKey("Name");
            ////    //�ܾ���chorme�����������  
            ////    //������
            ////    //��ajaxͳһ����
            ////}, MapTest);

            ////Middleware��
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
            });//�ս�㣬������mvc Ҳ�����Ǳ����Ŀ����  signalr


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Ruanmou Web API");

            });
        }
    }
}
