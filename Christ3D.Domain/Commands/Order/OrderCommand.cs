using System;
using System.Collections.Generic;
using Christ3D.Domain.Core.Commands;

namespace Christ3D.Domain.Commands
{
    /// <summary>
    /// 定义一个抽象的 Order 命令模型
    /// 继承 Command
    /// 这个模型主要作用就是用来创建命令动作的，所以是一个抽象类
    /// </summary>
    public abstract class OrderCommand : Command
    {
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }

        public List<Christ3D.Domain.Models.OrderItem> Items { get; set; }

    }
}