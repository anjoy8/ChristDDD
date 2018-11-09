using Christ3D.Application.Interfaces;
using Christ3D.Application.Services;
using Christ3D.Domain.Interfaces;
using Christ3D.Infra.Data.Context;
using Christ3D.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Christ3D.Infra.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {

            // 注入 Application
            services.AddScoped<IStudentAppService, StudentAppService>();
          

            // 注入 Infra - Data
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<StudyContext>();

        }
    }
}