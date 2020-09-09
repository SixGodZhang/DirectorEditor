# DirectorEditor

基于winform实现的桌面应用开发框架.  
目前计划包含以下主要内容:  
1. 网络通信框架  
2. 组件库  
3. 容器注册服务  

## 框架的最终设计理念汇总:
### MVPFramework UI框架设计结构图:
                              +--------------------------------------------------------------------------+                              
                              |                          MVPFramework                                    |                              
                              +--------------------------------------------------------------------------+                              
                                                                                                                                        
               +-------------+                            +------------------------------+                                              
               |= Model Layer|                            |= Presenter Layer             |                                              
               |             |----------send------------->|+----------------------------+|                                              
               |             |                            ||                            ||                             |   |            
               |             |                            ||                            ||                    +---------   ----------+  
               +-------------+                            ||                            ||                    |= Presenter Stub      |  
                                                          ||      Presenter List        ||-----register------>|                      |  
                                                          ||                            ||                    +----------------------+  
                                                          ||                            ||                               |              
                                                          ||                            ||                               |              
                                                          |+----------------------------+|                               |              
                                                          +------------------------------+                               |              
                                                                          |                                              |              
                                                                          |                                              |              
                                                                          n                                              |              
                                                                          :                                              |              
                                                                          n                                              |              
                                                                          |                                              |              
                                                                          |                                              |              
                                                                          V                                              |              
      +---------------------------------------------------------------------------------+                                |              
      |= View Layer                                                                     |                                |              
      |  +-------------------------+                      +----------------------------+|                                |              
      |  |= View Component Layer   |                      |= View Logic Layer          ||                                |              
      |  |                         |                      |                            ||                                V              
      |  |                         |                      |                            ||                    +----------------------+   
      |  |                         |<--------1:1--------->|                            ||                    |= View Logic Stub     |   
      |  |                         |                      |                            ||-----register------>|                      |   
      |  |                         |                      |                            ||                    +----------------------+   
      |  |                         |                      |                            ||                                               
      |  +-------------------------+                      +----------------------------+|                                               
      |---------------------------------------------------------------------------------+                                               

**优势如下:**
1. 实现V层和P层的完全隔离  
2. 完全的数据驱动. 利用Ioc,实现UI的依赖注入  
3. 增加对P层的缓存结构, 提升显示界面显示时的效率  


**设计的几个约定:  **
P层上的每个P都是单例  
View Component Layer 和 View Logic Layer 是一一对应关系, 并且可以同时存在多个实例  
Presenter Stub是整个结构的访问入口, 不要尝试通过其它手段(比如反射等)去强行直接访问MVP结构中的其它层次,这样做的目的是完全的数据驱动  


View Logic layer 与 Presenter layer 有以下几种对应关系:  
- Presenter : ViewLogic = 1:1  
- Presenter : ViewLogic = N:1  
- Presenter : ViewLogic = 1:N  
**详细说明：**  
Presenter : ViewLogic = 1:1 -> 一个Presenter实例对应一个ViewLogic实例  
Presenter : ViewLogic = N:1 -> N种不同类型的实例对应同一ViewLogic类型的不同实例  
Presenter : ViewLogic = 1:N -> 一个Presenter实例对应多个类型的ViewLogic实例  
稍微难以理解的就是第二种N:1, 解释如下:  
P层在代码中是以单例的形式存在, V层在代码中是以多实例的方式存在,  
所以P:V = N:1, 在MVPFramework是这样处理的:  
一个P层的单例对应一个V层的实例,也就是是说不同类型P层的单例可以对应V层同一类型的多实例  

### MVPFramework 界面开发具体流程【以HelperView举例, 详细可参考代码】  

1. **建立全局可访问的P层, 也是MVP结构的入口。**  
``` csharp
// 帮助界面的P层
private static HelperPresenter _helperPresenter = null;
public static HelperPresenter HelperPresenter { set => _helperPresenter = value;
	get
	{
		if (_helperPresenter == null)
		{
			_helperPresenter = new HelperPresenter();
		}
		return _helperPresenter;
	}

}
```
如上， 我们可以使用单例来缓存P层, 当然, 如果不缓存P层，可以不这样做。但是，建议这样做。  

2. **P层和View Logic绑定。P层主要处理2件事, 一是与ViewLogic绑定, 二是处理数据, 将数据转换成界面显示需要的格式。**  
``` csharp
[ViewLogicBinding(typeof(HelperViewLogic))]// 指定Presenter绑定的ViewLogic类型
public class HelperPresenter:Presenter<IHelperView,HelperModel>
{
	// ...
｝
```
如上, 通过装饰器我们可以确定P层和ViewLogic的绑定关系, IHelperView 定义了P层可以访问ViewLogic中的接口,HelperModel 定义了P层可以缓存M层类型的数据  

3. **创建ViewLogic类。这一层级专注于处理界面的显示逻辑。比如组件的显/隐、组件的数据填充等**  
``` csharp
// ViewLogic<T1,T2>
// T1 : 指的是绑定的 UI组件
// T2 : 指的是暴露给Presenter的接口
public class HelperViewLogic : ViewLogic<HelperView, IHelperView>, IHelperView
{

	public HelperViewLogic() : base()
	{
		InitViewLogic += () => { this.LayoutView(new HelperModel() { EditorDesc = "帮助文档描述初始数据" }); };
	}
```

4. **创建组件。也可以直接引用第三方组件, 因为不需要修改, 这需要在这一层级用第三方的组件拼接好界面即可。**  
``` csharp
/// <summary>
/// 帮助界面
/// </summary>
public partial class HelperView:Form
{
	public HelperView()
	{
		InitializeComponent();
	}
```

## 进度(设计思考流程过程在此记录)
### 2020.9.8.15.34  
超级大版本更新 -> 移除绑定器的概念  

### 2020.9.8.10.40
发现一个问题, 以前的结构是先创建viewlogic, 然后再创建presenter实行绑定  
现在解耦合之后, 可以先创建presenter, 使用到ViewLogic的时候再进行创建, 这时候, 又会根据绑定器去查找、创建一个实例 。 这样没必要啊  
也就是说, 现在没必要用这个绑定器了。 这个只有在强绑定时， 也就是在根据viewLogic去找presenter的时候才会使用  

### 2020.9.7.17.30
实现了Presenter -> ViewLogic 的1:N结构, 具体demo见DataPartPresenter  
``` csharp
	[ViewLogicBinding(typeof(DataPart1ViewLogic))]// 绑定DataPart1ViewLogic
	[ViewLogicBinding(typeof(DataPart2ViewLogic))]// 绑定DataPart2ViewLogic
	public class DataPartPresenter : PresenterNN
	{
		// ...
	}
```
Presenter 需要2步:  
1. 继承PresenterNN  
2. 与Presenter绑定的ViewLogic需要用装饰器进行标注  

PresenterNN的意思是, 对应N个ViewLogic,  N个Model数据描述结构  
效果图如下, DataPartPresenter可以处理两个ViewLogic的内容:  
![](Images/PresenterNN结构实现.png)  

### 2020.9.7.9.30
现在注册到PresenterStub的访问顺序结构有问题:  
之前是这样的:  
``` csharp
PresenterStub.HelperPresenter?.Show();
```
Presenter -> 创建ViewLogic实例 ->  创建组件实例  
问题出现在Presenter的实例是在ViewLogic的实例创建之后, 从ViewLogic实例中的绑定的Presenters中选择的第一个  
现在的想法是, Presenter可以独立创建. 如果Presenter需要显示某个界面, 则再去处理ViewLogi的创建  

这样思考的话, 其实我只是做了ViewLogic -> Presenter的单向绑定, 如果需要做NN关系的话, 那么就需要做一个双向绑定  

### 2020.9.7.8
接入MaterialSkin控件开源库  
![](Images/接入MatetrialSkin控件开源库.png)

### 2020.9.4 晚上
尝试接入第三方插件库【HZH_Controls】, 完美接入.  
![](Images/接入开源控件HZH.png)

### 2020.9.4 早晨  
在仔细思考了MVC、MVP等框架的设计理念之后, 对MVPFramework进行了迭代升级:  
                              +--------------------------------------------------------------------------+                              
                              |                          MVPFramework                                    |                              
                              +--------------------------------------------------------------------------+                              
                                                                                                                                        
               +-------------+                            +------------------------------+                                              
               |= Model Layer|                            |= Presenter Layer             |                                              
               |             |----------send------------->|+----------------------------+|                                              
               |             |                            ||                            ||                             |   |            
               |             |                            ||                            ||                    +---------   ----------+  
               +-------------+                            ||                            ||                    |= Presenter Stub      |  
                                                          ||      Presenter List        ||-----register------>|                      |  
                                                          ||                            ||                    +----------------------+  
                                                          ||                            ||                               |              
                                                          ||                            ||                               |              
                                                          |+----------------------------+|                               |              
                                                          +------------------------------+                               |              
                                                                          |                                              |              
                                                                          |                                              |              
                                                                          n                                              |              
                                                                          :                                              |              
                                                                          n                                              |              
                                                                          |                                              |              
                                                                          |                                              |              
                                                                          V                                              |              
      +---------------------------------------------------------------------------------+                                |              
      |= View Layer                                                                     |                                |              
      |  +-------------------------+                      +----------------------------+|                                |              
      |  |= View Component Layer   |                      |= View Logic Layer          ||                                |              
      |  |                         |                      |                            ||                                V              
      |  |                         |                      |                            ||                    +----------------------+   
      |  |                         |<--------1:1--------->|                            ||                    |= View Logic Stub     |   
      |  |                         |                      |                            ||-----register------>|                      |   
      |  |                         |                      |                            ||                    +----------------------+   
      |  |                         |                      |                            ||                                               
      |  +-------------------------+                      +----------------------------+|                                               
      |---------------------------------------------------------------------------------+                                               

其实, 传统的MVC等模式等模式并没有完全实现V层和C层的完全分离, 有的在V层甚至可以直接修改Model层的数据, 基于此, 主要做了以下改进:  
1. **将V层进一步划分为View Component Layer 和 View Logic Layer**  
  这样做的好处主要有:  
  a. 在View Component Layer上， 不进行任何业务逻辑的处理， UI设计可以完全移交给美术，并且业务逻辑不会受到UI迭代的影响  
  b. 可以自由嵌入第三方的组件库, 并且支持随时升级。 很多时候，我们都会去选择魔改第三方库以支持自己的业务，这种设计对第三方库无任何侵入式修改，便于维护  
2. **完全的数据驱动View层**  
  a. 为了严格做到各层之间的隔离, 防止因开发者的随意访问各层，而造成后期项目层之间的混乱  
  b. 作为开发者一方， 我们只关注数据和业务即可， 做到有变动即刷新  
3. **P层 和 V层 n:n 的对应关系**  
  传统的MVC、或则MVP框架, V层和C层、P层都是1:1关系, 主要会出现数据复制的情况:  
  a. 对于那些不会改动的数据, 每次重新显示界面的时候都会在C层或P层重新处理一遍  
  b. 如果多个界面用到同一份数据, 传统做法是将数据在每个C层或P层都赋值一份，然后重新处理一遍,效率比较低  
  c. 如果一个界面有多个数据源, 传统做法是将涉及到的所有Model都会放到C层去处理, 然后传递到V层.问题同上  
  因此,做了一下改进:  
  在P层做一次缓存(已做), 缓存的量级达到某一程度时, 触发清理过程(还没做). 清理过程就是根据GC那套思想, 不常用的先清理, 数据占用内存较大的先清理  

此外, 完善了View Logic Layer 和 Presenter Layer 绑定时的策略。 目前支持以下三种策略, 预计未来增加一种配置文件策略:  
- 装饰器策略  
- 标准命名策略  
- 组合策略  

9.2 的版本提到的DispatchCenter 概念正式替换为 Presenter Stub, 所有的Presenter 都可以在此访问.  

DirectorEditor 项目为演示工程  
目录结构如下:  
Models: 存储各种数据结构  
Presenters: 存储Presenter， 也就是业务逻辑处理层  
UIComponents: UI组件层, 不包含任何业务逻辑  
UILogic: UI业务逻辑层  
Views: ViewLogic 需要暴露给Presenter的接口集合  
PresenterStub：PresenterStub的全局访问中心  
ViewLogicStub: ViewLogicStub的全局访问中心(这个在未来不会对开发者开放直接访问权限)  

UI界面编写流程:  
1. 在UIComponents目录下创建组件  
2. 在Views目录下创建ViewLogic层需要暴露给Presenter层的接口  
3. 在UILogic目录下,编写界面的业务逻辑以及实现需要暴露个Presenter层的接口  
4. 在Presenters目录下,进行数据处理项目的业务逻辑, 如果有需要,在Models文件夹下定义数据结构  
5. 将ViewLogic和PresenteStub分别注册到全局访问中心  
6. 在需要的地方进行数据驱动即可  

### 2020.9.2  
MVPFramework 主体框架初步搭建完成  
 - M : Model层, 主要定义数据结构.  
 - P : Presenter层, 主要定义逻辑.  
 - V : View层, 主要控制视图的显示  

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

