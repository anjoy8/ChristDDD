using AutoMapper;

namespace Christ3D.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            //这里以后会写领域命令，所以不能和DomainToViewModelMappingProfile写在一起。
            ////学生视图模型 -> 添加新学生命令模型
            //CreateMap<StudentViewModel, RegisterNewStudentCommand>()
            //    .ConstructUsing(c => new RegisterNewStudentCommand(c.Name, c.Email, c.BirthDate));
            ////学生视图模型 -> 更新学生信息命令模型
            //CreateMap<StudentViewModel, UpdateStudentCommand>()
            //    .ConstructUsing(c => new UpdateStudentCommand(c.Id, c.Name, c.Email, c.BirthDate));
        }
    }
}
