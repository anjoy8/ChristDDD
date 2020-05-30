using System.IO;
using Christ3D.Domain.Models;
using Christ3D.Infra.Data.Mappings;
using Christ3D.Infrastruct;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Christ3D.Infra.Data.Context
{
    public class StudyContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        /// <summary>
        /// 重写自定义Map配置
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //对 StudentMap 进行配置
            modelBuilder.ApplyConfiguration(new StudentMap());
            modelBuilder.ApplyConfiguration(new OrderMap());

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// 重写连接数据库
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // 从 appsetting.json 中获取配置信息
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // 定义要使用的数据库
            //我是读取的文件内容，为了数据安全
            if (config.GetConnectionString("IsMysql").ObjToBool())
            {
                optionsBuilder.UseMySql(DbConfig.InitConn(config.GetConnectionString("DefaultConnection_file"), config.GetConnectionString("DefaultConnection")));
            }
            else
            {
                optionsBuilder.UseSqlServer(DbConfig.InitConn(config.GetConnectionString("DefaultConnection_file"), config.GetConnectionString("DefaultConnection")));
            }
        }
    }
}
