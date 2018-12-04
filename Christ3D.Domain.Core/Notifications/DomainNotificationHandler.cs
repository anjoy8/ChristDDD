using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Christ3D.Domain.Core.Notifications
{
    /// <summary>
    /// 领域通知处理程序，把所有的通知信息放到事件总线中
    /// 继承 INotificationHandler<T>
    /// </summary>
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        // 通知信息列表
        private List<DomainNotification> _notifications;

        // 每次访问该处理程序的时候，实例化一个空集合
        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        // 处理方法，把全部的通知信息，添加到内存里
        public Task Handle(DomainNotification message, CancellationToken cancellationToken)
        {
            _notifications.Add(message);
            return Task.CompletedTask;
        }
        
        // 获取当前生命周期内的全部通知信息
        public virtual List<DomainNotification> GetNotifications()
        {
            return _notifications;
        }

        // 判断在当前总线对象周期中，是否存在通知信息
        public virtual bool HasNotifications()
        {
            return GetNotifications().Any();
        }

        // 手动回收（清空通知）
        public void Dispose()
        {
            _notifications = new List<DomainNotification>();
        }
    }
}