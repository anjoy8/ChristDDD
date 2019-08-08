using Christ3D.Domain.Models;
using System.Collections.Generic;

namespace Christ3D.Domain.Interfaces
{
    /// <summary>
    /// IOrderRepository接口
    /// 注意，这里我们的对象，是领域对象
    /// </summary>
    public interface IOrderRepository : IRepository<Order>
    {
        //一些Student独有的接口
        Order GetByName(string name);
    }
}