using System;

namespace Christ3D.Domain.Core.Events
{
    /// <summary>
    /// 事件存储模型，继承事件
    /// </summary>
    public class StoredEvent : Event
    {
        /// <summary>
        /// 构造方式实例化
        /// </summary>
        /// <param name="theEvent"></param>
        /// <param name="data"></param>
        /// <param name="user"></param>
        public StoredEvent(Event theEvent, string data, string user)
        {
            Id = Guid.NewGuid();
            AggregateId = theEvent.AggregateId;
            MessageType = theEvent.MessageType;
            Data = data;
            User = user;
        }

        // 为了EFCore能正确CodeFirst
        protected StoredEvent() { }
        // 事件存储Id
        public Guid Id { get; private set; }
        // 存储的数据
        public string Data { get; private set; }
        // 用户信息
        public string User { get; private set; }
    }
}