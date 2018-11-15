using System;
using Christ3D.Domain.Validations;

namespace Christ3D.Domain.Commands
{
    public class RegisterStudentCommand : StudentCommand
    {
        public RegisterStudentCommand(string name, string email, DateTime birthDate, string phone)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Phone = phone;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterStudentCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}