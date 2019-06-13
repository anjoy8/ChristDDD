using System;
using System.IO;

namespace Christ3D.Infrastruct
{
    public class DbConfig
    {
        public static string InitConn(params string[] conn)
        {
            try
            {
                foreach (var item in conn)
                {
                    try
                    {
                        if (File.Exists(item))
                        {
                            return File.ReadAllText(item).Trim();
                        }
                    }
                    catch (Exception) { }
                }

                return conn[conn.Length - 1];
            }
            catch (Exception)
            {
                throw new Exception("数据库连接字符串配置有误，请检查 web 层下  appsettings.json 文件");
            }
        }
    }
}
