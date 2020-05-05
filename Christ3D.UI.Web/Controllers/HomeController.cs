using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Christ3D.UI.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;

namespace Christ3D.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 保存顾客方法
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="birthDate"></param>
        public void saveCustomer(string id, string name, string email, string birthDate)
        {
            Customer customer = CustomerDao.GetCustomer(id);
            if (customer == null)
            {
                customer = new Customer();
                customer.Id = id;
            }

            if (name != null)
            {
                customer.Name = name;
            }
            if (email != null)
            {
                customer.Email = email;
            }

            //...还有其他属性

            CustomerDao.SaveCustomer(customer);
        }



        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet]
        [Route("account/AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize]
        public IActionResult Login()
        {
            return Redirect("index");

        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);

            return View("Index");

        }
    }
}
