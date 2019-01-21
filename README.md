
<img src="https://github.com/anjoy8/ChristDDD/blob/master/Christ3D.UI.Web/wwwroot/images/logoddd.png" width="350"  />

一个基于 DDD 领域驱动设计 + CQRS 命令查询职责分离 的 .net core 框架，完全开源，并且有博客教程，地址在下边。


*********************************************************


## 给个星星! ⭐️
如果你喜欢这个项目或者它帮助你, 请给 Star~（辛苦星咯）

**********************
三大平台同步直播

博客园：https://www.cnblogs.com/laozhang-is-phi/p/9806335.html

简  书：https://www.jianshu.com/c/fe7dd7fc5372
 
 码云：https://gitee.com/laozhangIsPhi/ChristDDD
 
 *******
 ```
 ```
 
 <h3 id="autoid-2-1-0">目录：</h3><ul>
<li><a href="https://www.cnblogs.com/laozhang-is-phi/p/9806335.html" target="_blank">一 ║ D3模式设计初探 与 我的计划书</a></li>
<li><a id="post_title_link_9832684" href="https://www.cnblogs.com/laozhang-is-phi/p/9832684.html">二 ║ DDD入门 &amp; 项目结构粗搭建</a></li>
<li><a id="post_title_link_9845573" href="https://www.cnblogs.com/laozhang-is-phi/p/9845573.html">三 ║ 简单说说：领域、子域、限界上下文</a></li>
<li><a id="post_title_link_9872450" href="https://www.cnblogs.com/laozhang-is-phi/p/9872450.html">四 ║一个让你明白DDD的小故事 &amp; EFCore初探</a></li>
<li><a id="post_title_link_9888502" href="https://www.cnblogs.com/laozhang-is-phi/p/9888502.html">五 ║聚合：实体与值对象 （上）</a></li>
<li><a id="post_title_link_9916785" href="https://www.cnblogs.com/laozhang-is-phi/p/9916785.html">六 ║聚合 与 聚合根 （下）</a></li>
<li><a id="post_title_link_9931304" href="https://www.cnblogs.com/laozhang-is-phi/p/9931304.html">七 ║项目第一次实现 &amp; CQRS初探</a></li>
<li><a id="post_title_link_9962759" href="https://www.cnblogs.com/laozhang-is-phi/p/9962759.html">八 ║剪不断理还乱的 值对象和Dto</a></li>
<li><a id="post_title_link_9984740" href="https://www.cnblogs.com/laozhang-is-phi/p/9984740.html">九 ║从军事故事中，明白领域命令验证（上）</a></li>
<li><a id="post_title_link_10000662" href="https://www.cnblogs.com/laozhang-is-phi/p/10000662.html">十 ║领域驱动【实战篇·中】：命令总线Bus分发（一）</a></li>
<li><a id="post_title_link_10025913" href="https://www.cnblogs.com/laozhang-is-phi/p/10025913.html">十一 ║ 基于源码分析，命令分发的过程（二）</a></li>
<li><a id="post_title_link_10059878" href="https://www.cnblogs.com/laozhang-is-phi/p/10059878.html">十二 ║ 核心篇【下】：事件驱动EDA 详解</a></li>
<li><a id="post_title_link_10093444" href="https://www.cnblogs.com/laozhang-is-phi/p/10093444.html">十三 ║ 当事件溯源 遇上 粉丝活动</a></li>
</ul>

```
```

****************************************************************
主要的流程图，在下边的图中可以体现：


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



编者按：
1、本项目我是借鉴了 https://github.com/EduardoPires/EquinoxProject 来讲解的，请支持原作者！因为他没有文档，所以我就写了这个系列。
2、可能你会说我是抄袭，但是我自己写的时候，结构不是这样的，我当时是按照下边写的（如果你和我下边的分层一样，那就证明我不是瞎说的了）：

应用层：除了Service和IService、DTO、还有使用 CQRS 方法的查询、接受的命令，事件驱动的通信（集成事件），但是没有业务规则；

领域（模型）层：这里主要放的是领域实体、值对象、聚合和事件模型、Bus等主要都是模型，非贫血；

基础层：就是ORM的持久化相关；

U  I 层：显示页面；

不过我写的时候感觉各层之间凌乱，不适合初学者学习，所以就想着要改变一下，找一个清晰明了的，对比了Git上的各种大神结构，偶然发现了EduardoPires的代码，感觉很清晰，就按照他这个来了。 说白了，就是把CQRS读写分离和事件驱动全部放到领域层了，然后又提炼出来一个领域核心Doman.Core层。
