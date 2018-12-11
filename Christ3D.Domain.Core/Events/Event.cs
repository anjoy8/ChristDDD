using System;
using MediatR;

namespace Christ3D.Domain.Core.Events
{
    /// <summary>
    /// 事件模型 抽象基类，继承 INotification
    /// 也就是说，拥有中介者模式中的 发布/订阅模式
    /// 同时继承了Messgae 也就是继承了 请求/响应模式
    /// </summary>
    public abstract class Event : Message,INotification
    {
        // 时间戳
        public DateTime Timestamp { get; private set; }
        
        // 每一个事件都是有状态的
        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}