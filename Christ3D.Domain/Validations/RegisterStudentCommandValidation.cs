using Christ3D.Domain.Commands;

namespace Christ3D.Domain.Validations
{
    public class RegisterStudentCommandValidation : StudentValidation<RegisterStudentCommand>
    {
        public RegisterStudentCommandValidation()
        {
            ValidateName();
            ValidateBirthDate();
            ValidateEmail();
            ValidatePhone();
        }
    }
}