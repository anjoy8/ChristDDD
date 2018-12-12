using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Christ3D.Domain.Core.Bus;
using Christ3D.Domain.Core.Commands;
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

        public InMemoryBus(IMediator mediator, ServiceFactory serviceFactory)
        {
            _mediator = mediator;
            _serviceFactory = serviceFactory;
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
           // return Send(command);//请注意 入参 的类型
        }


       
    }
}