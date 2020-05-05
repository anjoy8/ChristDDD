using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Christ3D.Domain.Commands;
using Christ3D.Domain.Core.Bus;
using Christ3D.Domain.Core.Notifications;
using Christ3D.Domain.Events;
using Christ3D.Domain.Interfaces;
using Christ3D.Domain.Models;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Christ3D.Domain.CommandHandlers
{
    /// <summary>
    /// Order命令处理程序
    /// 用来处理该Order下的所有命令
    /// 注意必须要继承接口IRequestHandler<,>，这样才能实现各个命令的Handle方法
    /// </summary>
    public class OrderCommandHandler : CommandHandler,
        IRequestHandler<RegisterOrderCommand, bool>
    {
        // 注入仓储接口
        private readonly IOrderRepository _OrderRepository;
        // 注入总线
        private readonly IMediatorHandler Bus;
        private IMemoryCache Cache;

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="OrderRepository"></param>
        /// <param name="uow"></param>
        /// <param name="bus"></param>
        /// <param name="cache"></param>
        public OrderCommandHandler(IOrderRepository OrderRepository,
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      IMemoryCache cache
                                      ) : base(uow, bus, cache)
        {
            _OrderRepository = OrderRepository;
            Bus = bus;
            Cache = cache;
        }


        // RegisterOrderCommand命令的处理程序
        // 整个命令处理程序的核心都在这里
        // 不仅包括命令验证的收集，持久化，还有领域事件和通知的添加
        public Task<bool> Handle(RegisterOrderCommand message, CancellationToken cancellationToken)
        {
            // 命令验证
            if (!message.IsValid())
            {
                // 错误信息收集
                NotifyValidationErrors(message);
                // 返回，结束当前线程
                return Task.FromResult(false);
            }

            // 实例化领域模型，这里才真正的用到了领域模型
            // 注意这里是通过构造函数方法实现

            var Order = new Order(Guid.NewGuid(),message.Name, message.Items);

            // 判断邮箱是否存在
            // 这些业务逻辑，当然要在领域层中（领域命令处理程序中）进行处理
            if (_OrderRepository.GetByName(Order.Name) != null)
            {

                //引发错误事件
                Bus.RaiseEvent(new DomainNotification("", "该Name已经被使用！"));
                return Task.FromResult(false);
            }

            // 持久化
            _OrderRepository.Add(Order);

            // 统一提交
            if (Commit())
            {
                // 提交成功后，这里需要发布领域事件
                // 比如欢迎用户注册邮件呀，短信呀等

                //Bus.RaiseEvent(new OrderRegisteredEvent(Order.Id, Order.Name, Order.Email, Order.BirthDate, Order.Phone));
            }

            return Task.FromResult(true);

        }

    
        // 手动回收
        public void Dispose()
        {
            _OrderRepository.Dispose();
        }
    }
}