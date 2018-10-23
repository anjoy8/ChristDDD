using System.Linq;
using Christ3D.Domain.Interfaces;
using Christ3D.Domain.Models;

namespace Christ3D.Infra.Data.Repository
{
    /// <summary>
    /// Customer仓储，操作对象还是领域对象
    /// </summary>
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        //对特例接口进行实现
        public Customer GetByEmail(string email)
        {
            throw new System.NotImplementedException();
        }
    }
}
