using Christ3D.Infra.IoC;
using Christ3D.Infrastruct.Identity.Authorization;
using Christ3D.UI.Web.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Christ3D.UI.Web
{
    public class Startup
    {
        /*
         * mysql和sqlserver的迁移操作步骤一致，不过本项目的迁移文件已经迁移好，在Data文件夹下：
         * msql使用MigrationsMySql文件夹下的迁移记录，卸载另一个Migrations文件夹
         * sqlserver使用Migrations文件夹下的迁移记录，卸载另一个MigrationsMySql文件夹
         * 
         * 当然你也可以都删掉，自己重新做迁移。
         * 
         一、迁移项目1（一定要切换到 Christ3D.Infrastruct 项目下，使用 Package Manager Console）：
           1、add-migration InitStudentDbMysql -Context StudyContext  -o MigrationsMySql
           2、add-migration InitEventStoreDbMysql -Context EventStoreSQLContext -o MigrationsMySql/EventStore
           3、update-database -Context StudyContext
           4、update-database -Context EventStoreSQLContext

         二、迁移项目2【弃用，因为现在是使用IdentityServer4】（一定要切换到 Christ3D.Infrastruct.Identity 项目下，使用 Package Manager Console）：
           1、add-migration InitIdentityDbMysql -Context ApplicationDbContext -o Data/MigrationsMySql/ 
           2、update-database -Context ApplicationDbContext
             
        */

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //nginx 
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
                options.ExcludedHosts.Add("ddd.neters.com");
            });


            services.AddSameSiteCookiePolicy();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            // IdentityServer4 注入
            if (Configuration["Authentication:IdentityServer4:Enabled"].ObjToBool())
            {
                System.Console.WriteLine("当前授权模式是:Ids4");
                services.AddId4OidcSetup(Configuration);
            }
            else
            {
                System.Console.WriteLine("当前授权模式是:Identity");
                services.AddIdentitySetup(Configuration);
            }

            // Automapper 注入
            services.AddAutoMapperSetup();

            services.AddControllersWithViews();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanWriteStudentData", policy => policy.Requirements.Add(new ClaimRequirement("Students", "Write")));
                options.AddPolicy("CanRemoveStudentData", policy => policy.Requirements.Add(new ClaimRequirement("Students", "Remove")));
                options.AddPolicy("CanWriteOrRemoveStudentData", policy => policy.Requirements.Add(new ClaimRequirement("Students", "WriteOrRemove")));
            });

            // Adding MediatR for Domain Events
            // 领域命令、领域事件等注入
            // 引用包 MediatR.Extensions.Microsoft.DependencyInjection
            services.AddMediatR(typeof(Startup));

            // .NET Core 原生依赖注入
            // 单写一层用来添加依赖项，从展示层 Presentation 中隔离
            NativeInjectorBootStrapper.RegisterServices(services);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCookiePolicy();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
