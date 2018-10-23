using System;
using System.Collections.Generic;
using Christ3D.Application.Interfaces;
using Christ3D.Application.ViewModels;
using Christ3D.Domain.Interfaces;

namespace Christ3D.Application.Services
{
    /// <summary>
    /// CustomerAppService 服务接口实现类,继承 服务接口
    /// 通过 DTO 实现视图模型和领域模型的关系处理
    /// 作为调度者，协调领域层和基础层，
    /// 这里只是做一个面向用户用例的服务接口,不包含业务规则或者知识
    /// </summary>
    public class CustomerAppService : ICustomerAppService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerAppService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IEnumerable<CustomerViewModel> GetAll()
        {
            return null;
            //return _customerRepository.GetAll().ProjectTo<CustomerViewModel>();
        }

        public CustomerViewModel GetById(Guid id)
        {
            return null;
            //return _mapper.Map<CustomerViewModel>(_customerRepository.GetById(id));
        }

        public void Register(CustomerViewModel customerViewModel)
        {
            //var registerCommand = _mapper.Map<RegisterNewCustomerCommand>(customerViewModel);
        }

        public void Update(CustomerViewModel customerViewModel)
        {
            //var updateCommand = _mapper.Map<UpdateCustomerCommand>(customerViewModel);
        }

        public void Remove(Guid id)
        {
            //var removeCommand = new RemoveCustomerCommand(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
