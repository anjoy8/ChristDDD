using AutoMapper;

namespace Christ3D.Application.AutoMapper
{
    /// <summary>
    /// 静态全局 AutoMapper 配置文件
    /// </summary>
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            //创建AutoMapperConfiguration, 提供静态方法Configure，一次加载所有层中Profile定义 
            //MapperConfiguration实例可以静态存储在一个静态字段中，也可以存储在一个依赖注入容器中。 一旦创建，不能更改/修改。
            return new MapperConfiguration(cfg =>
            {
                //这个是领域模型 -> 视图模型的映射，是 读命令
                cfg.AddProfile(new DomainToViewModelMappingProfile());
                //这里是视图模型 -> 领域模式的映射，是 写 命令
                cfg.AddProfile(new ViewModelToDomainMappingProfile());
            });
        }
    }
}
