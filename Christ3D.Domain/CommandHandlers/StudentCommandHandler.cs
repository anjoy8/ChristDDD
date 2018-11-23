using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Christ3D.Domain.Commands;
using Christ3D.Domain.Core.Bus;
using Christ3D.Domain.Interfaces;
using Christ3D.Domain.Models;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Christ3D.Domain.CommandHandlers
{
    public class StudentCommandHandler : CommandHandler,
        IRequestHandler<RegisterStudentCommand, Unit>,
        IRequestHandler<UpdateStudentCommand, Unit>,
        IRequestHandler<RemoveStudentCommand, Unit>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMediatorHandler Bus;
        private IMemoryCache Cache;


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




        public void Dispose()
        {
            _studentRepository.Dispose();
        }

        public Task<Unit> Handle(RegisterStudentCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(new Unit());
            }

            var customer = new Student(Guid.NewGuid(), message.Name, message.Email, message.Phone, message.BirthDate);

            if (_studentRepository.GetByEmail(customer.Email) != null)
            {
                List<string> errorInfo = new List<string>() { "The customer e-mail has already been taken." };
                Cache.Set("ErrorData", errorInfo);
                return Task.FromResult(new Unit());
            }

            _studentRepository.Add(customer);

            if (Commit())
            {

            }

            return Task.FromResult(new Unit());

        }

        public Task<Unit> Handle(UpdateStudentCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                return Task.FromResult(new Unit());

            }

            var customer = new Student(message.Id, message.Name, message.Email, message.Phone, message.BirthDate);
            var existingCustomer = _studentRepository.GetByEmail(customer.Email);

            if (existingCustomer != null && existingCustomer.Id != customer.Id)
            {
                if (!existingCustomer.Equals(customer))
                {

                    return Task.FromResult(new Unit());

                }
            }

            _studentRepository.Update(customer);

            if (Commit())
            {

            }

            return Task.FromResult(new Unit());

        }

        public Task<Unit> Handle(RemoveStudentCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                return Task.FromResult(new Unit());

            }

            _studentRepository.Remove(message.Id);

            if (Commit())
            {
            }

            return Task.FromResult(new Unit());

        }


    }
}