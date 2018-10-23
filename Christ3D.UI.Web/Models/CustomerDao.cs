using System;

namespace Christ3D.UI.Web.Models
{
    /// <summary>
    /// 领域对象持久化层
    /// </summary>
    public class CustomerDao
    {
        public static Customer GetCustomer(string id)
        {
            return new Customer() { Id = "1", Name = "Christ", Email = "Christ@123.com" };
        }


        public static string SaveCustomer(Customer customer)
        {
            return "保存成功";
        }
    }
}