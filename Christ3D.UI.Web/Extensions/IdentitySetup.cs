using System;
using Christ3D.Infrastruct;
using Christ3D.Infrastruct.Identity.Data;
using Christ3D.Infrastruct.Identity.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Christ3D.UI.Web.Extensions
{
    /// <summary>
    /// AutoMapper 的启动服务
    /// </summary>
    public static class IdentitySetup
    {

        public static void AddIdentitySetup(this IServiceCollection services, IConfiguration Configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));


            services.AddDbContext<ApplicationDbContext>(options =>
            {
                if (Configuration.GetConnectionString("IsMysql").ObjToBool())
                {
                    options.UseMySql(DbConfig.InitConn(Configuration.GetConnectionString("DefaultConnection_file"), Configuration.GetConnectionString("DefaultConnection")));
                }
                else
                {
                    options.UseSqlServer(DbConfig.InitConn(Configuration.GetConnectionString("DefaultConnection_file"), Configuration.GetConnectionString("DefaultConnection")));
                }
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/oauth2/authorize";
                options.AccessDeniedPath = "/account/access-denied";
                options.SlidingExpiration = true;
            });
        }


    }
}