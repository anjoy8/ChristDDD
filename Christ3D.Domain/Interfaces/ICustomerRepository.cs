using Christ3D.Domain.Models;

namespace Christ3D.Domain.Interfaces
{
    /// <summary>
    /// ICustomerRepository接口
    /// 注意，这里我们的对象，是领域对象
    /// </summary>
    public interface ICustomerRepository : IRepository<Customer>
    {
        //一些Customer独有的接口
        Customer GetByEmail(string email);
    }
}