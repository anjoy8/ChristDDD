using System;
using AutoMapper;
using Christ3D.Application.AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Christ3D.UI.Web.Extensions
{
    /// <summary>
    /// AutoMapper 的启动服务
    /// </summary>
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            //添加服务,如果Profile文件在 api 层，这里，可以直接用 typeof(Startup) 就行
            //但是如果写到了其他层，必须写 config,
            //AutoMapper会从程序集中搜索Profile的子类，然后把这些子类加入到配置中。
            //如果Profile文件是在当前程序中定义，那没有问题，但如果是写在外部类库内，则是搜索不到的
            services.AddAutoMapper(typeof(AutoMapperConfig));
            //启动配置
            AutoMapperConfig.RegisterMappings();
        }
    }
}