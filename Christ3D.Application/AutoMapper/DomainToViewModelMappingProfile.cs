using AutoMapper;
using Christ3D.Application.ViewModels;
using Christ3D.Domain.Models;

namespace Christ3D.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile: Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Student, StudentViewModel>();
        }
    }
}
