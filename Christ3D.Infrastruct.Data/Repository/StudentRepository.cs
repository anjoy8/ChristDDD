using System.Linq;
using Christ3D.Domain.Interfaces;
using Christ3D.Domain.Models;
using Christ3D.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Christ3D.Infra.Data.Repository
{
    /// <summary>
    /// Student仓储，操作对象还是领域对象
    /// </summary>
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(StudyContext context)
          : base(context)
        {

        }
        //对特例接口进行实现
        public Student GetByEmail(string email)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.Email == email);
        }
    }
}
