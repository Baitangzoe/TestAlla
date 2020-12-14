# 一、状态模式

## 1、状态模式原理
1. 当一个对象的内在状态改变时允许改变其行为，这个对象看起来像是改变了其类
2. 状态模式主要解决的是当控制一个对象状态转换的条件表达式过于复杂时的情况。把状态的判断逻辑转移到表示不同状态的一系列类当中，可以复杂的判断逻辑简化。

## 2、图表
![状态模式](F:\TestA2\pics\zhuangt.png)

## 3、相关代码
```c#
/// <summary>  
/// 状态模式：电灯类，对应模式中的Context类,维护一个ConcreteState(On Off)子类的实例，这个实例定义当前的状态。  
/// </summary>  
public class Light
{
    private LightState state;
    /// <summary>
    /// 定义Light的初始状态
    /// </summary>
    /// <param name="state"></param>
    public Light(LightState state)
    {
        this.state = state;//从外面传进来的灯泡状态，初始状态为关
    }

    /// <summary>  
    /// 按下电灯开关,对请求做处理，并设置下一个状态   
    /// </summary>  
    public void PressSwich()
    {
        state.PressSwich(this);//this=this.state,当按下的时候，它现在的状态是开着的，所以
    }
    /// <summary>
    /// 可读写的状态属性，用于【读取和设置新状态】 
    /// </summary>
    public LightState State
    {
        get { return state; }
        set { state = value; }
    }
}
/// <summary>  
/// 抽象的【电灯状态类】，相当于State类，定义一个抽象类以封装与Context（Light）的一个【特定状态相关的行为】
/// 对于继承抽象类的子类来说，对于抽象类来说，属于“是”的关系，电灯的状态【是】【开】还是【关】
/// 当大家具有共同行为，但是行为的实现方式不同时，则可以把这个共同行为封装成一个接口interface，具有该行为的类来具体实现该接口
/// 例如：灯泡的状态、人和动物都需要移动
/// </summary>
public abstract class LightState
{
    public abstract void PressSwich(Light light);
}

/// <summary>  
/// 具体状态类, 开  
/// </summary>  
public class On : LightState
{
    /// <summary>  
    /// 在开(ConcreteStateA)状态下，按下开关则切换到关(ConcreteStateB)的状态。  
    /// </summary>  
    /// <param name="light"></param>  
    public override void PressSwich(Light light)
    {
        Console.WriteLine("Turn off the light.");
        //读取和设置新状态
        light.State = new Off();
    }
}
/// <summary>  
/// 具体状态类，关，每一个子类实现一个与Context(Light)的一个状态相关的行为    
/// </summary>  
public class Off : LightState
{
    /// <summary>  
    /// 在关(ConcreteStateB)状态下，按下开关则打开(ConcreteStateA)电灯。  
    /// </summary>  
    /// <param name="light"></param>  
    public override void PressSwich(Light light)
    {
        Console.WriteLine("Turn on the light.");
        //读取和设置新状态
        light.State = new On();
    }
}
class Program
{
    static void Main(string[] args)
    {
        Light light = new Light(new Off());
        Console.WriteLine("状态模式：");
        Console.WriteLine("电灯状态：");
        light.PressSwich();
        light.PressSwich();
        light.PressSwich();
    }
}
```
* * *
# 二、访问者模式
## 1、访问者模式原理
1. 表示一个作用于其对象结构中的各个元素的操作。访问者模式是一种对象行为型模式，它为操作存储不同的类型元素的对象结构提供了一种解决方案，用户可以对不同类型的元素施加不同的操作。访问者模式常常和组合模式一起使用。
2. 该模式包含五种角色：
    1）抽象访问者类（Visitor）：为对象结构中的每一个元素类声明一个访问操作。
    2）具体访问者类（ConcreteVisitor）：实现了每个抽象访问者声明的操作，每一个操作用于访问对象结构中一种类型的元素。
    3）抽象元素类（Element）：该类一般是抽象类或者接口，声明一个Accept()方法，用于接受访问者的访问操作，该方法通常以一个抽象访问者作为参数。
    4）具体元素类（ConcreteElement）：实现了Accept()方法，在该方法中调用访问者的访问方法以便完成对一个元素的操作。
    5）对象结构类（ObjectStructure）：是一个元素的集合，用于存放元素对象，并且提供了遍历其内部元素的方法。对象结构可以结合组合模式来实现，也可以是一个简单的集合对象。
## 2、图表
![访问者模式](F:\TestA2\pics\fangw.png)
## 3、相关代码
```c#
//访问者模式
/// <summary>
/// 抽象访问者
/// </summary>
public abstract class Visitor
{
    protected string name
    {
        get; set;
    }
    //获取访问者的名字
    public Visitor(string name)
    {
        this.name = name;
    }
    //访问者访问药品A、药品B
    public abstract void visitor(MedicineA a);
    public abstract void visitor(MedicineB b);
}
/// <summary>
/// 具体访问者：划价员
/// </summary>
public class Charge : Visitor
{
    /// <summary>
    /// 在继承类的构造函数中调用基类的构造函数base
    /// </summary>
    /// <param name="name"></param>
    public Charge(string name) : base(name) { }
    public override void visitor(MedicineA a)
    {
        Console.WriteLine("划价员：" + this.name + "划价、给药：" + a.GetName() + "，单价：" + a.GetPrice() + "，总价格：" + a.GetSum());
    }
    public override void visitor(MedicineB b)
    {
        Console.WriteLine("划价员：" + this.name + "划价、给药：" + b.GetName() + "，单价：" + b.GetPrice() + "，总价格：" + b.GetSum());
    }
}
/// <summary>
/// 具体访问者：药房工作者
/// </summary>
public class WorkerOfPharmacy : Visitor
{
    /// <summary>
    /// 在继承类的构造函数中调用基类的构造函数base
    /// </summary>
    /// <param name="name"></param>
    public WorkerOfPharmacy(string name) : base(name) { }
    public override void visitor(MedicineA a)
    {
        Console.WriteLine("药房工作者：" + this.name + "，抓药：" + a.GetName() + "，数量：" + a.GetCourt());
    }
    public override void visitor(MedicineB b)
    {
        Console.WriteLine("药房工作者：" + this.name + "，抓药：" + b.GetName() + "，数量：" + b.GetCourt());
    }
}
/// <summary>
/// 抽象元素：药
/// </summary>
public abstract class Medicine
{
    //定义药品的名称、价格、总价、数量
    protected string name { get; set; }
    protected double price { get; set; }
    protected double sum { get; set; }
    protected int court { get; set; }
    //获取药品的名称、价格、数量
    public Medicine(string name, double price, int court)
    {
        this.name = name;
        Console.WriteLine(name);
        this.price = price;
        Console.WriteLine(price);
        //药品数量
        this.court = court;
        //药品总价格（数量×单价）
        this.sum = court * price;
    }
    public string GetName()
    {
        return name;//可读
    }
    public double GetPrice()
    {
        return price;//可读
    }
    public double GetCourt()
    {
        return court;//可读
    }
    public double GetSum()
    {
        return sum;//可读
    }
    public void SetPrice(double price)
    {
        this.price = price;//可写
    }
    public void SetSum(int sum)
    {
        this.sum = sum;//可写
    }
    //定义抽象方法（定义一个接受相关访问者的访问操作，以访问者作为访问参数）
    public abstract void accept(Visitor visitor);
}
/// <summary>
/// 具体元素：A名称药
/// </summary>
public class MedicineA : Medicine
{
    public MedicineA(string name, double price, int court) : base(name, price, court) { }
    public override void accept(Visitor visitor)
    {
        visitor.visitor(this);
    }
}
/// <summary>
/// 具体元素：B名称药
/// </summary>
public class MedicineB : Medicine
{
    public MedicineB(string name, double price, int court) : base(name, price, court) { }
    public override void accept(Visitor visitor)
    {
        visitor.visitor(this);
    }
}
/// <summary>
/// 具体元素：药单
/// </summary>
public class Presciption
{
    //存储药品信息
    private List<Medicine> listmedicine = new List<Medicine>();
    //接受访问者visitor访问药单
    public void accpet(Visitor visitor)
    {
        foreach (var item in listmedicine)
        {
            item.accept(visitor);
        }
    }
    public void add(Medicine med)
    {
        listmedicine.Add(med);
    }
    public void remove(Medicine med)
    {
        listmedicine.Remove(med);
    }
}
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("访问者模式：");
        //药类型
        Medicine a = new MedicineA("阿莫西林", 19.9, 3);
        Medicine b = new MedicineB("土黄霉素", 5.3, 2);
        //药单（将所需药品添加到药单中）
        Presciption presciption = new Presciption();
        presciption.add(a);
        presciption.add(b);
        Visitor charge = new Charge("张三");//划价员访问者
        Visitor workerOfPharmacy = new WorkerOfPharmacy("李四");//药房工作者访问者
        presciption.accpet(charge);//划价
        presciption.accpet(workerOfPharmacy);//抓药
    }
}
```
* * *
# 三、原型模式
## 1、原型模式原理
1. 用原型实例指定创建对象的种类，并且通过拷贝这些原型创建新的对象。
2. 创建型模式中一个比较特殊的模式-原型模式，有个最大的特点是克隆一个现有的对象，这个克隆的结果有2种，一种是浅度复制，另一种是深度复制。
3. 创建型模式一般是用来创建一个新的对象，然后我们使用这个对象完成一些对象的操作，我们通过原型模式可以快速的创建一个对象而不需要提供专门的new()操作就可以快速完成对象的创建，这无疑是一种非常有效的方式，快速的创建一个新的对象。
## 2、图表
![原型模式](F:\TestA2\pics\yuanx.png)

## 3、相关代码

```c#
//原型模式
public abstract class Prototype
{
    private string _id;

    public Prototype(string id)
    {
        this._id = id;
    }

    public string Id
    {
        get { return _id; }
    }

    public abstract Prototype Clone();
}
public class ConcretePrototype1 : Prototype
{
    public ConcretePrototype1(string id) : base(id) { }
    public override Prototype Clone()
    {
        return (Prototype)this.MemberwiseClone();
    }
}
public class ConcretePrototype2 : Prototype
{
    public ConcretePrototype2(string id) : base(id) { }
    public override Prototype Clone()
    {
        return (Prototype)this.MemberwiseClone();
    }
}
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("原型模式：");
        ConcretePrototype1 p1 = new ConcretePrototype1("124685");
        ConcretePrototype1 c1 = (ConcretePrototype1)p1.Clone();
        Console.WriteLine("Cloned: {0}", c1.Id);
        ConcretePrototype2 p2 = new ConcretePrototype2("135472");
        ConcretePrototype2 c2 = (ConcretePrototype2)p2.Clone();
        Console.WriteLine("Cloned: {0}", c2.Id);
    }
}
```
* * *
# 四、观察者模式
## 1、观察者模式原理
1. 观察者模式，有时被称作发布/订阅模式，观察者模式定义了一种一对多的依赖关系，让多个观察者对象同时监听某一个主题对象。这个主题对象在状态发生变化时，会通知所有观察者对象，使它们能够自动更新自己。
2. 模式中具有的角色
    1） 抽象主题（Subject）：它把所有观察者对象的引用保存到一个聚集里，每个主题都可以有任何数量的观察者。抽象主题提供一个接口，可以增加和删除观察者对象。
    2）具体主题（ConcreteSubject）：将有关状态存入具体观察者对象；在具体主题内部状态改变时，给所有登记过的观察者发出通知。
    3）抽象观察者（Observer）：为所有的具体观察者定义一个接口，在得到主题通知时更新自己。
    4）具体观察者（ConcreteObserver）：实现抽象观察者角色所要求的更新接口，以便使本身的状态与主题状态协调。
## 2、图表
![观察者模式](F:\TestA2\pics\guanc.png)

## 3、相关代码

```c#
//观察者模式
// 订阅号抽象类
public abstract class Blog
{
    // 保存订阅者列表
    private List<IObserver> observers = new List<IObserver>();

    public string Symbol { get; set; }//描写订阅号的相关信息
    public string Info { get; set; }//描写此次update的信息
    public Blog(string symbol, string info)
    {
        this.Symbol = symbol;
        this.Info = info;
    }

    // 对同一个订阅号，新增和删除订阅者的操作
    public void AddObserver(IObserver ob)
    {
        observers.Add(ob);
    }
    public void RemoveObserver(IObserver ob)
    {
        observers.Remove(ob);
    }

    public void Update()
    {
        // 遍历订阅者列表进行通知
        foreach (IObserver ob in observers)
        {
            if (ob != null)
            {
                ob.Receive(this);
            }
        }
    }
}
// 具体订阅号类
public class MyBlog : Blog
{
    public MyBlog(string symbol, string info) : base(symbol, info) { }
}
// 订阅者接口
public interface IObserver
{
    void Receive(Blog tenxun);
}
// 具体的订阅者类
public class Subscriber : IObserver
{
    public string Name { get; set; }
    public Subscriber(string name)
    {
        this.Name = name;
    }
    public void Receive(Blog weibo)
    {
        Console.WriteLine("订阅者 {0} 观察到{1}{2}", Name, weibo.Symbol, weibo.Info);
    }
}
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("观察者模式：");
        //订阅号
        Blog WeiBo = new MyBlog("微博", "发布了一篇新文章");
        // 添加订阅者
        WeiBo.AddObserver(new Subscriber("小张"));
        WeiBo.Update();
        Blog TengXun = new MyBlog("腾讯", "发布了一篇新闻");
        TengXun.AddObserver(new Subscriber("小赵"));
        //更新信息
        TengXun.Update();
    }
}
```
* * *
# 五、单例模式
## 1、单例模式原理
1. 意图
保证一个类仅有一个实例，并提供一个访问它的全局访问点。
2. 适用性
当类只能有一个实例而且客户可以从一个众所周知的访问点访问它时。
当这个唯一实例应该是通过子类化可扩展的，并且客户应该无需更改代码就能使用一个扩展的实例时。
## 2、图表
![单例模式](F:\TestA2\pics\danl.png)

## 3、相关代码

```c#
//单例模式
public class Singleton
{
    private static Singleton _instance = null;
    private static readonly object _instanceLock = new object();
    private Singleton() { }
    public static Singleton Instance
    {
        get
        {
            if (null == _instance)
            {
                lock (_instanceLock)
                {
                    if (null == _instance)
                    {
                        _instance = new Singleton();
                    }
                }
            }
            return _instance;
        }
    }
    public int Age { get; set; }
    public void GetShow()
    {
        this.Age = this.Age + 1;
        Console.WriteLine("我是一个单例对象：Age=" + Age);
    }
}
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("单例模式：");
        Singleton singleton1 = Singleton.Instance;
        singleton1.GetShow();
        Singleton singleton2 = Singleton.Instance;
        singleton2.GetShow();
        if (singleton1 == singleton2)
        {
            Console.WriteLine("ok");
        }
        else
        {
            Console.WriteLine("no");
        }
        Console.ReadKey();
    }
}
```

