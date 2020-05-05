using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Christ3D.Domain.Commands;
using Christ3D.Domain.Core.Bus;
using Christ3D.Domain.Core.Notifications;
using Christ3D.Domain.Events;
using Christ3D.Domain.Interfaces;
using Christ3D.Domain.Models;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Christ3D.Domain.CommandHandlers
{
    /// <summary>
    /// Student命令处理程序
    /// 用来处理该Student下的所有命令
    /// 注意必须要继承接口IRequestHandler<,>，这样才能实现各个命令的Handle方法
    /// </summary>
    public class StudentCommandHandler : CommandHandler,
        IRequestHandler<RegisterStudentCommand, bool>,
        IRequestHandler<UpdateStudentCommand, bool>,
        IRequestHandler<RemoveStudentCommand, bool>
    {
        // 注入仓储接口
        private readonly IStudentRepository _studentRepository;
        // 注入总线
        private readonly IMediatorHandler Bus;
        private IMemoryCache Cache;

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="studentRepository"></param>
        /// <param name="uow"></param>
        /// <param name="bus"></param>
        /// <param name="cache"></param>
        public StudentCommandHandler(IStudentRepository studentRepository,
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      IMemoryCache cache
                                      ) : base(uow, bus, cache)
        {
            _studentRepository = studentRepository;
            Bus = bus;
            Cache = cache;
        }


        // RegisterStudentCommand命令的处理程序
        // 整个命令处理程序的核心都在这里
        // 不仅包括命令验证的收集，持久化，还有领域事件和通知的添加
        public Task<bool> Handle(RegisterStudentCommand message, CancellationToken cancellationToken)
        {
            // 命令验证
            if (!message.IsValid())
            {
                // 错误信息收集
                NotifyValidationErrors(message);
                // 返回，结束当前线程
                return Task.FromResult(false);
            }

            // 实例化领域模型，这里才真正的用到了领域模型
            // 注意这里是通过构造函数方法实现
            var address = new Address(message.Province, message.City,
            message.County, message.Street);
            var student = new Student(Guid.NewGuid(), message.Name, message.Email, message.Phone, message.BirthDate, address);

            // 判断邮箱是否存在
            // 这些业务逻辑，当然要在领域层中（领域命令处理程序中）进行处理
            if (_studentRepository.GetByEmail(student.Email) != null)
            {
                ////这里对错误信息进行发布，目前采用缓存形式
                //List<string> errorInfo = new List<string>() { "该邮箱已经被使用！" };
                //Cache.Set("ErrorData", errorInfo);

                //引发错误事件
                Bus.RaiseEvent(new DomainNotification("", "该邮箱已经被使用！"));
                return Task.FromResult(false);

            }

            // 持久化
            _studentRepository.Add(student);

            // 统一提交
            if (Commit())
            {
                // 提交成功后，这里需要发布领域事件
                // 比如欢迎用户注册邮件呀，短信呀等

                Bus.RaiseEvent(new StudentRegisteredEvent(student.Id, student.Name, student.Email, student.BirthDate, student.Phone));
            }

            return Task.FromResult(true);


        }

        // 同上，UpdateStudentCommand 的处理方法
        public Task<bool> Handle(UpdateStudentCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);

            }


            var address = new Address(message.Province, message.City,
            message.County, message.Street);
            var student = new Student(message.Id, message.Name, message.Email, message.Phone, message.BirthDate,address);
            var existingStudent = _studentRepository.GetByEmail(student.Email);

            if (existingStudent != null && existingStudent.Id != student.Id)
            {
                if (!existingStudent.Equals(student))
                {

                    Bus.RaiseEvent(new DomainNotification("", "该邮箱已经被使用！"));
                    return Task.FromResult(false);

                }
            }

            _studentRepository.Update(student);

            if (Commit())
            {

                Bus.RaiseEvent(new StudentUpdatedEvent(student.Id, student.Name, student.Email, student.BirthDate, student.Phone));
            }

            return Task.FromResult(true);

        }

        // 同上，RemoveStudentCommand 的处理方法
        public Task<bool> Handle(RemoveStudentCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);

            }

            _studentRepository.Remove(message.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new StudentRemovedEvent(message.Id));
            }

            return Task.FromResult(true);

        }

        // 手动回收
        public void Dispose()
        {
            _studentRepository.Dispose();
        }
    }
}