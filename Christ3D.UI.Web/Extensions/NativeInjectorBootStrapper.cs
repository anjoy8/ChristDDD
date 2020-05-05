using Christ3D.Application.EventSourcing;
using Christ3D.Application.Interfaces;
using Christ3D.Application.Services;
using Christ3D.Domain.CommandHandlers;
using Christ3D.Domain.Commands;
using Christ3D.Domain.Core.Bus;
using Christ3D.Domain.Core.Events;
using Christ3D.Domain.Core.Notifications;
using Christ3D.Domain.EventHandlers;
using Christ3D.Domain.Events;
using Christ3D.Domain.Interfaces;
using Christ3D.Infra.Bus;
using Christ3D.Infra.Data.Context;
using Christ3D.Infra.Data.Repository;
using Christ3D.Infra.Data.Repository.EventSourcing;
using Christ3D.Infra.Data.UoW;
using Christ3D.Infrastruct.Identity.Authorization;
using Christ3D.Infrastruct.Identity.Models;
using Christ3D.Infrastruct.Identity.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Christ3D.Infra.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {

            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // ASP.NET Authorization Polices
            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();

            // 注入 应用层Application
            services.AddScoped<IStudentAppService, StudentAppService>();
            services.AddScoped<IOrderAppService, OrderAppService>();

            // 命令总线Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();


            // Domain - Events
            // 将事件模型和事件处理程序匹配注入

            // 领域通知
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            // 领域事件
            services.AddScoped<INotificationHandler<StudentRegisteredEvent>, StudentEventHandler>();
            services.AddScoped<INotificationHandler<StudentUpdatedEvent>, StudentEventHandler>();
            services.AddScoped<INotificationHandler<StudentRemovedEvent>, StudentEventHandler>();


            // 领域层 - 领域命令
            // 将命令模型和命令处理程序匹配
            services.AddScoped<IRequestHandler<RegisterStudentCommand, bool>, StudentCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateStudentCommand, bool>, StudentCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveStudentCommand, bool>, StudentCommandHandler>();

            services.AddScoped<IRequestHandler<RegisterOrderCommand, bool>, OrderCommandHandler>();


            // 领域层 - Memory
            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });



            // 注入 基础设施层 - 数据层
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<StudyContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            // 注入 基础设施层 - 事件溯源
            services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
            services.AddScoped<IEventStoreService, SqlEventStoreService>();
            services.AddScoped<EventStoreSQLContext>();

            // 注入 基础设施层 - Identity Services
            services.AddTransient<IEmailSender, AuthEmailMessageSender>();
            services.AddTransient<ISmsSender, AuthSMSMessageSender>();

            // 注入 基础设施层 - Identity
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}