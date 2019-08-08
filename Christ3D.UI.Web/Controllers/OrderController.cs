using System;
using System.Collections.Generic;
using Christ3D.Application.Interfaces;
using Christ3D.Application.ViewModels;
using Christ3D.Domain.Core.Notifications;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Christ3D.UI.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderAppService _OrderAppService;
        IValidator<OrderViewModel> _validator;
        private IMemoryCache _cache;
        // 将领域通知处理程序注入Controller
        private readonly DomainNotificationHandler _notifications;

        public OrderController(IOrderAppService OrderAppService, IMemoryCache cache, INotificationHandler<DomainNotification> notifications)
        {
            _OrderAppService = OrderAppService;
            _cache = cache;
            // 强类型转换
            _notifications = (DomainNotificationHandler)notifications;
        }

        // GET: Order
        public ActionResult Index()
        {
            return View(_OrderAppService.GetAll());
        }



        // GET: Order/Create
        // 页面
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        // 方法
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderViewModel OrderViewModel)
        {
            try
            {
                //_cache.Remove("ErrorData");
                //ViewBag.ErrorData = null;
                // 视图模型验证
                if (!ModelState.IsValid)
                    return View(OrderViewModel);

                
                // 这里为了测试，手动赋值items
                OrderViewModel.Items = new List<OrderItemViewModel>() {
                    new OrderItemViewModel (){
                        Name="详情"+DateTime.Now
                    }
                };



                // 执行添加方法
                _OrderAppService.Register(OrderViewModel);

                //var errorData = _cache.Get("ErrorData");
                //if (errorData == null)

                // 是否存在消息通知
                if (!_notifications.HasNotifications())
                    ViewBag.Sucesso = "Order Registered!";

                return View(OrderViewModel);
            }
            catch (Exception e)
            {
                return View(e.Message);
            }
        }


    }
}