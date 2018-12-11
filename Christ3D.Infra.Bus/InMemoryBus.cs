using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Christ3D.Domain.Core.Bus;
using Christ3D.Domain.Core.Commands;
using Christ3D.Domain.Core.Events;
using MediatR;
using MediatR.Internal;

namespace Christ3D.Infra.Bus
{
    /// <summary>
    /// 一个密封类，实现我们的中介内存总线
    /// </summary>
    public sealed class InMemoryBus : IMediatorHandler
    {
        //构造函数注入
        private readonly IMediator _mediator;
        //注入服务工厂
        private readonly ServiceFactory _serviceFactory;
        private static readonly ConcurrentDictionary<Type, object> _requestHandlers = new ConcurrentDictionary<Type, object>();
        // 事件仓储服务
        private readonly IEventStoreService _eventStoreService;


        public InMemoryBus(IMediator mediator, ServiceFactory serviceFactory,IEventStoreService eventStoreService)
        {
            _mediator = mediator;
            _serviceFactory = serviceFactory;
            _eventStoreService = eventStoreService;
        }

        /// <summary>
        /// 实现我们在IMediatorHandler中定义的接口
        /// 没有返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        public Task SendCommand<T>(T command) where T : Command
        {
            //这个是正确的
            return _mediator.Send(command);//请注意 入参 的类型

            //注意！这个仅仅是用来测试和研究源码的，请开发的时候不要使用这个
            //return Send(command);//请注意 入参 的类型
        }

        /// <summary>
        /// 引发事件的实现方法
        /// </summary>
        /// <typeparam name="T">泛型 继承 Event：INotification</typeparam>
        /// <param name="event">事件模型，比如StudentRegisteredEvent</param>
        /// <returns></returns>
        public Task RaiseEvent<T>(T @event) where T : Event
        {
            // 除了领域通知以外的事件都保存下来
            if (!@event.MessageType.Equals("DomainNotification"))
                _eventStoreService?.Save(@event);

            // MediatR中介者模式中的第二种方法，发布/订阅模式
            return _mediator.Publish(@event);
        }



        /// <summary>
        /// Mdtiator Send方法源码
        /// </summary>
        /// <typeparam name="TResponse">泛型</typeparam>
        /// <param name="request">请求命令</param>
        /// <param name="cancellationToken">用来控制线程Task</param>
        /// <returns></returns>
        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            // 判断请求是否为空
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            // 获取请求命令类型
            var requestType = request.GetType();

            // 对我们的命令进行封装
            // 请求处理程序包装器
            var handler = (RequestHandlerWrapper<TResponse>)_requestHandlers.GetOrAdd(requestType,
                t => Activator.CreateInstance(typeof(RequestHandlerWrapperImpl<,>).MakeGenericType(requestType, typeof(TResponse))));

            //↑↑↑↑↑↑↑这以上是第二步↑↑↑↑↑↑↑↑↑↑


            //↓↓↓↓↓↓↓第三步开始↓↓↓↓↓↓↓↓

            // 执行封装好的处理程序
            // 说白了就是执行我们的命令
            return handler.Handle(request, cancellationToken, _serviceFactory);
        }
    }
}