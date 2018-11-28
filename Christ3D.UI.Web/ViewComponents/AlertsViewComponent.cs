using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Christ3D.UI.Web.ViewComponents
{
    public class AlertsViewComponent : ViewComponent
    {
        //缓存注入，为了收录信息（错误方法，以后会用通知，通过领域事件来替换）
        private IMemoryCache _cache;
        public AlertsViewComponent(IMemoryCache cache)
        {
            _cache = cache;
        }
        /// <summary>
        /// Alerts 视图组件
        /// 可以异步，也可以同步，注意方法名称，同步的时候是Invoke
        /// 我写异步是为了为以后做准备
        /// </summary>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // 获取到缓存中的错误信息
            var errorData = _cache.Get("ErrorData");
            var notificacoes = await Task.Run(() => (List<string>)errorData);
            // 遍历添加到ViewData.ModelState 中
            notificacoes?.ForEach(c => ViewData.ModelState.AddModelError(string.Empty, c));

            return View();
        }
    }
}