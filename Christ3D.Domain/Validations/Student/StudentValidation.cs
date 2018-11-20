using System;
using Christ3D.Domain.Commands;
using FluentValidation;

namespace Christ3D.Domain.Validations
{
    /// <summary>
    /// 定义基于 StudentCommand 的抽象基类 StudentValidation
    /// 继承 抽象类 AbstractValidator
    /// 注意需要引用 FluentValidation
    /// 注意这里的 T 是命令模型
    /// </summary>
    /// <typeparam name="T">泛型类</typeparam>
    public abstract class StudentValidation<T> : AbstractValidator<T> where T : StudentCommand
    {
        //受保护方法，验证Name
        protected void ValidateName()
        {
            //定义规则，c 就是当前 StudentCommand 类
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("姓名不能为空")//判断不能为空，如果为空则显示Message
                .Length(2, 10).WithMessage("姓名在2~10个字符之间");//定义 Name 的长度
        }

        //验证年龄
        protected void ValidateBirthDate()
        {
            RuleFor(c => c.BirthDate)
                .NotEmpty()
                .Must(HaveMinimumAge)//Must 表示必须满足某一个条件，参数是一个bool类型的方法，更像是一个委托事件
                .WithMessage("学生应该14岁以上！");
        }

        //验证邮箱
        protected void ValidateEmail()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();
        }
        //验证手机号
        protected void ValidatePhone()
        {
            RuleFor(c => c.Phone)
                .NotEmpty()
                .Must(HavePhone)
                .WithMessage("手机号应该为11位！");
        }

        //验证Guid
        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

        // 表达式
        protected static bool HaveMinimumAge(DateTime birthDate)
        {
            return birthDate <= DateTime.Now.AddYears(-14);
        }
        // 表达式
        protected static bool HavePhone(string phone)
        {
            return phone.Length == 11;
        }
    }
}