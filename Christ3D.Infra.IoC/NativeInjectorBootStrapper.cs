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
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Christ3D.Infra.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // 注入 应用层Application
            services.AddScoped<IStudentAppService, StudentAppService>();

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


            // Domain - Commands
            // 将命令模型和命令处理程序匹配
            services.AddScoped<IRequestHandler<RegisterStudentCommand, Unit>, StudentCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateStudentCommand, Unit>, StudentCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveStudentCommand, Unit>, StudentCommandHandler>();

            // Domain - Memory
            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });



            // 注入 基础设施层 - 数据层
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<StudyContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            // 注入 基础设施层 - 事件溯源
            services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
            services.AddScoped<IEventStoreService, SqlEventStoreService>();
            services.AddScoped<EventStoreSQLContext>();
        }
    }
}