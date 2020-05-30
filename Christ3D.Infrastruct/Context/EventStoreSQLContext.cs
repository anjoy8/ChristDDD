using System.IO;
using Christ3D.Domain.Core.Events;
using Christ3D.Infra.Data.Mappings;
using Christ3D.Infrastruct;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Christ3D.Infra.Data.Context
{
    /// <summary>
    /// 事件存储数据库上下文，继承 DbContext
    /// </summary>
    public class EventStoreSQLContext : DbContext
    {
        // 事件存储模型
        public DbSet<StoredEvent> StoredEvent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StoredEventMap());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // 获取链接字符串
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // 使用默认的sql数据库连接
            //
           
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