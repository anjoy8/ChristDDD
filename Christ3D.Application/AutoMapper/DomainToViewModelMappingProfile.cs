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
            CreateMap<Student, StudentViewModel>()
                .ForMember(d => d.County, o => o.MapFrom(s => s.Address.County))
                .ForMember(d => d.Province, o => o.MapFrom(s => s.Address.Province))
                .ForMember(d => d.City, o => o.MapFrom(s => s.Address.City))
                .ForMember(d => d.Street, o => o.MapFrom(s => s.Address.Street))
                ;

            CreateMap<Order, OrderViewModel>();
            CreateMap<OrderItem, OrderItemViewModel>();
            CreateMap<OrderItemViewModel, OrderItem>();


        }
    }
}
