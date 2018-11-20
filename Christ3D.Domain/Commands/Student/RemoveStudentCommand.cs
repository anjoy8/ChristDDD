using System;
using Christ3D.Domain.Validations;

namespace Christ3D.Domain.Commands
{
    /// <summary>
    /// 注册一个删除 Student 命令
    /// 基础抽象学生命令模型
    /// </summary>
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