using System.Threading.Tasks;
using Christ3D.Domain.Core.Bus;
using Christ3D.Domain.Core.Commands;
using MediatR;

namespace Christ3D.Infra.Bus
{
    /// <summary>
    /// 一个密封类，实现我们的中介记忆总线
    /// </summary>
    public sealed class InMemoryBus : IMediatorHandler
    {
        //构造函数注入
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
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
            return _mediator.Send(command);//请注意 入参 的类型
        }

    }
}