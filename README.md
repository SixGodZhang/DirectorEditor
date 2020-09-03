# DirectorEditor

基于winform实现的桌面应用开发框架.  
目前计划包含以下主要内容:  
1. 网络通信框架
2. 组件库
3. 容器注册服务

## 进度
2020.9.2  
MVPFramework 主体框架初步搭建完成
 M : Model层, 主要定义数据结构.
 P : Presenter层, 主要定义逻辑.
 V : View层, 主要控制视图的显示

相比于传统的winform开发流程(View、逻辑、数据结构全部混在一起), MVPFramework将数据、逻辑、View进行了解耦合,利用C#的反射原理, 在View被初始化的时候,将Presenter与之绑定起来.
Model层是一个可选的层, 因为有一些不需要与服务端、表格、数据库等数据源交互的界面，可以不用Model层。

View和Presenter绑定原理
允许自定义策略去实现绑定结构。 目前主要支持三种策略:
- 装饰器策略
- 标准命名策略
- 组合策略
所谓装饰器策略, 就是在View初始化的时候, 找到CustomAttribute， 如果是PresenterBinding类型, 则将此View和Presenter绑定起来
标准命名策略, 也是在View初始化的时候, 根据固定命名规则在整个Assembly中找到相应的Presenter, 如果找到， 则将它们绑定起来
组合策略, 就是上面的策略组合,找到一个之后， 立马结束。

需要注意的是: 
Model ---> Presenter <-----> View
View层需要重新填充内容有以下几种场景:
- View初始化
- Presenter接收到其绑定的数据


调度中心 DispatchCenter
传统的MVC等模式, 其Control层还是依赖于View层, 如果View层被回收了, 那么相应的Control层也没有了。
原则上来说， 没什么问题， 但是Control层还有一个可以做但是不是必须做的内容 - 缓存。
基于这一点思考了下, 
之前我们是这样去处理数据刷新的:
从View层获取Control层 ---> 然后调用Control中的接口去处理数据 ---> 如果有改动，则通知View层去刷新
其实在这个流程上, 第一步【从View层获取Control层】可以省略, 前提是需要我们设计一个独立的Control层，实现与View层的完全解耦合。

