using System;
using Christ3D.Domain.Core.Commands;

namespace Christ3D.Domain.Commands
{
    public abstract class StudentCommand : Command
    {
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }

        public string Email { get; protected set; }

        public DateTime BirthDate { get; protected set; }

        public string Phone { get; protected set; }
    }
}