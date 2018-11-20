using System;
using Christ3D.Domain.Validations;

namespace Christ3D.Domain.Commands
{
    /// <summary>
    /// 注册一个更新 Student 命令
    /// 基础抽象学生命令模型
    /// </summary>
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