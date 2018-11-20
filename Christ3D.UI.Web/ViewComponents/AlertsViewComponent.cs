using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Christ3D.UI.Web.ViewComponents
{
    public class AlertsViewComponent : ViewComponent
    {
        /// <summary>
        /// Alerts 视图组件
        /// 可以异步，也可以同步，注意方法名称，同步的时候是Invoke
        /// 我写异步是为了为以后做准备
        /// </summary>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notificacoes = await Task.Run(() => (List<string>)ViewBag.ErrorData);
            //遍历错误信息，赋值给 ViewData.ModelState
            notificacoes?.ForEach(c => ViewData.ModelState.AddModelError(string.Empty, c));

            return View();
        }
    }
}