using Autofac;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Ruanmou.Core.Utility;
using Ruanmou04.NetCore.AOP.Filter;
using Ruanmou04.NetCore.AOP.IOC;
using Ruanmou04.NetCore.Interface;
using Ruanmou04.NetCore.Service;
using System.Collections.Generic;
using System.IO;
using YJ.PlatFormCore.Web.Startup;

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
         
            services.AddControllersWithViews();
            services.AddRazorPages();//约等于AddMvc() 就是3.0把内容拆分的更细一些，能更小的依赖
            services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();
            services.AddHttpContextAccessor();
            services.AddSingleton<ICurrentUserInfo, CurrentUserInfo>();
            services.AddMemoryCache();
            services.AddSingleton<VerifyAttribute>();
            services.AddSingleton<CustomExceptionFilterAttribute>();
            //services.AddControllers().AddNewtonsoftJson(options =>
            //{
            //    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            //});  //设置json序列化时使用默认属性，而不是转换为小写，要添加 Microsoft.AspNetCore.Mvc.NewtonsoftJson   
            services.AddMvc(opts=>opts.Filters.Add<CustomExceptionFilterAttribute>())
                    .AddNewtonsoftJson(options => {
                                       options.SerializerSettings.ContractResolver = new DefaultContractResolver(); })  //设置json序列化时使用默认属性，而不是转换为小写，要添加 Microsoft.AspNetCore.Mvc.NewtonsoftJson
                    .AddRazorRuntimeCompilation();

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //权限认证中间件
            //app.UseMiddleware<AuthorizeMiddleware>();
            //异常处理中间件
            //app.UseMiddleware<ExceptionHandlingMiddleware>();
            if (env.IsDevelopment())
            {
                //app.UseMiddleware<ExceptionHandlingMiddleware>();

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

            //app.UseAuthentication();
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

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
           {
               new ApiResource("inventoryapi", "this is inventory api"),
               new ApiResource("orderapi", "this is order api"),
               new ApiResource("productapi", "this is product api")
           };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
           {
               new Client
               {
                   ClientId = "inventory",
                   AllowedGrantTypes = GrantTypes.ClientCredentials,

                   ClientSecrets =
                   {
                       new Secret("inventorysecret".Sha256())
                   },

                   AllowedScopes = { "inventoryapi" }
               },
                new Client
               {
                   ClientId = "order",
                   AllowedGrantTypes = GrantTypes.ClientCredentials,

                   ClientSecrets =
                   {
                       new Secret("ordersecret".Sha256())
                   },

                   AllowedScopes = { "orderapi" }
               },
                new Client
               {
                   ClientId = "product",
                   AllowedGrantTypes = GrantTypes.ClientCredentials,

                   ClientSecrets =
                   {
                       new Secret("productsecret".Sha256())
                   },

                   AllowedScopes = { "productapi" }
               }
           };
        }

    }
}
