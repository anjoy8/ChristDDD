using System.Threading.Tasks;
using Christ3D.Domain.Core.Commands;
using Christ3D.Domain.Core.Events;

namespace Christ3D.Domain.Core.Bus
{
    /// <summary>
    /// 中介处理程序接口
    /// 可以定义多个处理程序
    /// 是异步的
    /// </summary>
    public interface IMediatorHandler
    {
        /// <summary>
        /// 发送命令，将我们的命令模型发布到中介者模块
        /// </summary>
        /// <typeparam name="T"> 泛型 </typeparam>
        /// <param name="command"> 命令模型，比如RegisterStudentCommand </param>
        /// <returns></returns>
        Task SendCommand<T>(T command) where T : Command;


        /// <summary>
        /// 引发事件，通过总线，发布事件
        /// </summary>
        /// <typeparam name="T"> 泛型 继承 Event：INotification</typeparam>
        /// <param name="event"> 事件模型，比如StudentRegisteredEvent，</param>
        /// 请注意一个细节：这个命名方法和Command不一样，一个是RegisterStudentCommand注册学生命令之前,一个是StudentRegisteredEvent学生被注册事件之后
        /// <returns></returns>
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
