using System.IO;
using Christ3D.Infra.IoC;
using Christ3D.Infrastruct.Identity.Authorization;
using Christ3D.Infrastruct.Identity.Data;
using Christ3D.Infrastruct.Identity.Models;
using Christ3D.UI.Web.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Christ3D.UI.Web
{
    public class Startup
    {
        /*
         一、迁移项目1（一定要切换到 Christ3D.Infrastruct 项目下，使用 Package Manager Console）：
           1、add-migration InitStudentDb -Context StudyContext 
           2、add-migration InitEventStoreDb -Context EventStoreSQLContext -o Migrations/EventStore
           3、update-database -Context StudyContext
           4、update-database -Context EventStoreSQLContext

         二、迁移项目2（一定要切换到 Christ3D.Infrastruct.Identity 项目下，使用 Package Manager Console）：
           1、add-migration InitIdentityDb -Context ApplicationDbContext -o Data/Migrations/ 
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(File.ReadAllText(Configuration.GetConnectionString("DefaultConnection"))));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o => {
                    o.LoginPath = new PathString("/login");
                    o.AccessDeniedPath = new PathString("/home/access-denied");
                })
                .AddFacebook(o =>
                {
                    o.AppId = Configuration["Authentication:Facebook:AppId"];
                    o.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                })
                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                    googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                });


            // Automapper 注入
            services.AddAutoMapperSetup();

            services.AddMvc();


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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
