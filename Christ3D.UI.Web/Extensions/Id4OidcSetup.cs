using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using AutoMapper;
using Christ3D.Application.AutoMapper;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;

namespace Christ3D.UI.Web.Extensions
{
    /// <summary>
    /// AutoMapper 的启动服务
    /// </summary>
    public static class Id4OidcSetup
    {
        public static void AddId4OidcSetup(this IServiceCollection services, IConfiguration Configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));


            //关闭默认映射，否则它可能修改从授权服务返回的各种claim属性
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            //添加认证服务，并设置其有关选项
            services.AddAuthentication(options =>
            {
                // 客户端应用设置使用"Cookies"进行认证
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                // identityserver4设置使用"oidc"进行认证
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;

            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                 // 对使用的OpenIdConnect进行设置，此设置与Identityserver的config.cs中相应client配置一致才可能登录授权成功
                 .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
                 {
                     options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                     options.Authority = Configuration["Authentication:IdentityServer4:AuthorizationUrl"];
                     options.RequireHttpsMetadata = false;//必须https协议
                     options.ClientId = "chrisdddmvc";//idp项目中配置的client
                     options.ClientSecret = "secret";
                     options.SaveTokens = true;
                     options.ResponseType = "code";//响应类型

                     // 下边是所有的scope,必须要和idp项目中一致,至少是一部分
                     options.Scope.Clear();
                     options.Scope.Add("roles");//"roles"
                     options.Scope.Add("rolename");//"rolename"
                     options.Scope.Add(OidcConstants.StandardScopes.OpenId);//"openid"
                     options.Scope.Add(OidcConstants.StandardScopes.Profile);//"profile"
                     options.Scope.Add(OidcConstants.StandardScopes.Email);//"email"

                 });

        }


    }
}