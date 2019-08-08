using System;
using Christ3D.Domain.Commands;
using FluentValidation;

namespace Christ3D.Domain.Validations
{
    /// <summary>
    /// 定义基于 OrderCommand 的抽象基类 OrderValidation
    /// 继承 抽象类 AbstractValidator
    /// 注意需要引用 FluentValidation
    /// 注意这里的 T 是命令模型
    /// </summary>
    /// <typeparam name="T">泛型类</typeparam>
    public abstract class OrderValidation<T> : AbstractValidator<T> where T : OrderCommand
    {
        //受保护方法，验证Name
        protected void ValidateName()
        {
            //定义规则，c 就是当前 OrderCommand 类
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("名称不能为空")//判断不能为空，如果为空则显示Message
                .Length(2, 10).WithMessage("名称在2~10个字符之间");//定义 Name 的长度
        }

       
    }
}