using System;
using FluentValidation.Results;

namespace Christ3D.Domain.Core.Commands
{
    public abstract class Command 
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public abstract bool IsValid();
    }
}