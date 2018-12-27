
![Logo](https://github.com/anjoy8/ChristDDD/blob/master/Christ3D.UI.Web/wwwroot/images/logoddd.png)

一个基于 DDD 领域驱动设计 + CQRS 命令查询职责分离 的 .net core 框架，完全开源，并且有博客教程，地址在下边。

编者按：
1、本项目我是借鉴了 https://github.com/EduardoPires/EquinoxProject 来讲解的，请支持原作者！因为他没有文档，所以我就写了这个系列。
2、可能你会说我是抄袭，但是我自己写的时候，结构不是这样的，我当时是这么写的（如果你和我下边的分层一样，那就证明我不是瞎说的了）：

应用层：除了Service和IService、DTO、还有使用 CQRS 方法的查询、接受的命令，事件驱动的通信（集成事件），但是没有业务规则；

领域层：这里主要放的是领域实体、值对象、聚合和事件模型、Bus等；

基础层：就是ORM的持久化相关；

U  I 层：显示页面；

不过我写的时候感觉凌乱，不适合大家初学者学习，所以就想着要改变一下，对比了Git上的各种大神结构，偶然发现了EduardoPires的代码，感觉很清晰，就按照他这个来了。 
*********************************************************


# 给个星星! ⭐️
如果你喜欢这个项目或者它帮助你, 请给 Star~（辛苦星咯）

**********************
三大平台同步直播

简  书：https://www.jianshu.com/notebooks/28621653

博客园：https://www.cnblogs.com/laozhang-is-phi/p/9806335.html

 
 码云：https://gitee.com/laozhangIsPhi/ChristDDD

****************************************************************
![流程图1](https://github.com/anjoy8/ChristDDD/blob/master/Christ3D.UI.Web/wwwroot/images/1468246-20181122182320361-566237541.png)

![流程图2](https://github.com/anjoy8/ChristDDD/blob/master/Christ3D.UI.Web/wwwroot/images/WeChat%20Image_20181203111601.png)

![流程图3](https://github.com/anjoy8/ChristDDD/blob/master/Christ3D.UI.Web/wwwroot/images/WeChat%20Image_20181203111555.png)




系统环境

　　windows 10、SQL server 2012、Visual Studio 2017、Windows Server 2008 R2、Linux Ubuntu、

开发环境

　　Visual Studio 15.3+、.NET Core SDK 2.0+、
  
  
1、知识点（补充中）
  
        ASP.NET Core 2.1.2  👉基本框架
        ASP.NET MVC Core  👉实现mvc web页面
        ASP.NET WebApi Core  👉实现 api 接口
        ASP.NET Identity Core  👉身份验证
        Entity Framework Core 2.0  👉实现ORM数据持久化
        Dapper （待定）
        .NET Core 原生 DI  👉实现依赖注入
        AOP  👉面向切面
        Autofact（待定）IoC
        AutoMapper  👉实现Dtos
        FluentValidator验证
        Swagger UI  👉实现接口文档展示
        MediatR  👉基于内存级别的消息发布订阅
        Azure  👉云服务发布
 

2、特性（补充中）

        领域驱动设计（Domain Driven Design (Layers and Domain Model Pattern)
        命令查询职责分离（CQRS：Command Query Responsibility Segregation）
        领域通知 （Domain Notification）
        领域驱动 （Domain Events）
        事件驱动架构 (EDA)
        事件回溯 （Event Sourcing）
        最终一致性 （Eventually Consistent）
        工作单元模式 （Unit of Work ）
        泛型仓储 （Repository and Generic Repository）
