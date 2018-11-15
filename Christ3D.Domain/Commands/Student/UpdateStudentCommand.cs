using System;
using Christ3D.Domain.Validations;

namespace Christ3D.Domain.Commands
{
    public class UpdateStudentCommand : StudentCommand
    {
        public UpdateStudentCommand(Guid id, string name, string email, DateTime birthDate, string phone)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Phone = phone;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateStudentCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}