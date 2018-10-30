using Christ3D.Domain.Models;

namespace Christ3D.Domain.Interfaces
{
    /// <summary>
    /// IStudentRepository接口
    /// 注意，这里我们的对象，是领域对象
    /// </summary>
    public interface IStudentRepository : IRepository<Student>
    {
        //一些Student独有的接口
        Student GetByEmail(string email);
    }
}