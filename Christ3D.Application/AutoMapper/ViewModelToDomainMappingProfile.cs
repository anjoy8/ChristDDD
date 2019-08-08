using AutoMapper;
using Christ3D.Application.ViewModels;
using Christ3D.Domain.Commands;
using Christ3D.Domain.Models;

namespace Christ3D.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            //手动进行配置
            CreateMap<StudentViewModel, Student>()
             .ForPath(d => d.Address.Province, o => o.MapFrom(s => s.Province))
             .ForPath(d => d.Address.City, o => o.MapFrom(s => s.City))
             .ForPath(d => d.Address.County, o => o.MapFrom(s => s.County))
             .ForPath(d => d.Address.Street, o => o.MapFrom(s => s.Street))
             ;

            //这里以后会写领域命令，所以不能和DomainToViewModelMappingProfile写在一起。
            //学生视图模型 -> 添加新学生命令模型
            CreateMap<StudentViewModel, RegisterStudentCommand>()
                .ConstructUsing(c => new RegisterStudentCommand(c.Name, c.Email, c.BirthDate, c.Phone, c.Province, c.City,
            c.County, c.Street));

            //学生视图模型 -> 更新学生信息命令模型
            CreateMap<StudentViewModel, UpdateStudentCommand>()
                .ConstructUsing(c => new UpdateStudentCommand(c.Id, c.Name, c.Email, c.BirthDate, c.Phone, c.Province, c.City,
            c.County, c.Street));


            CreateMap<OrderViewModel, Order>();

            CreateMap<OrderViewModel, RegisterOrderCommand>();
        }
    }
}
