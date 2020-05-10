using System.IO;
using Christ3D.Infrastruct.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Christ3D.Infrastruct.Identity.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //执行
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // get the configuration from the app settings
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // define the database to use
            // 因为我使用的是txt文件，所以用的是 File.ReadAllText() ，这里不用写，因为web层的IdentitySetup.cs已经配置过了
            //optionsBuilder.UseSqlServer(DbConfig.InitConn(config.GetConnectionString("DefaultConnection_file"), config.GetConnectionString("DefaultConnection")));
            //optionsBuilder.UseMySql(DbConfig.InitConn(config.GetConnectionString("DefaultConnection_file"), config.GetConnectionString("DefaultConnection")));
        }
    }
}
