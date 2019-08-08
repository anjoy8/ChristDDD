using System;
using System.Collections.Generic;
using Christ3D.Application.EventSourcedNormalizers;
using Christ3D.Application.ViewModels;

namespace Christ3D.Application.Interfaces
{
    /// <summary>
    /// 定义 IOrderAppService 服务接口
    /// 并继承IDisposable，显式释放资源
    /// 注意这里我们使用的对象，是视图对象模型
    /// </summary>
    public interface IOrderAppService : IDisposable
    {
        void Register(OrderViewModel OrderViewModel);
        IEnumerable<OrderViewModel> GetAll();
    }
}
