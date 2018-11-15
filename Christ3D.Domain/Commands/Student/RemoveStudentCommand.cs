using System;
using Christ3D.Domain.Validations;

namespace Christ3D.Domain.Commands
{
    public class RemoveStudentCommand : StudentCommand
    {
        public RemoveStudentCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveStudentCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}