using System;
using System.Collections.Generic;

namespace DesignMode
{
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

            Console.WriteLine();
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

            Console.WriteLine();
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

            Console.WriteLine();
            Console.WriteLine("原型模式：");
            Resume x = new Resume("靖凡");
            x.SetPersonalInfo("男", "42");
            x.SetWorkExperience("2010--2018", "君和律师事务所");

            Resume y = (Resume)x.Clone();
            y.SetPersonalInfo("男", "34");
            y.SetWorkExperience("2005--2010", "利兹律师事务所");

            Resume z = (Resume)x.Clone();
            z.SetPersonalInfo("男", "31");
            z.SetWorkExperience("1998--2005", "明和律师事务所");

            x.Display();//x公司
            y.Display();//xx公司
            z.Display();//xxx公司

            ConcretePrototype1 p1 = new ConcretePrototype1("124685");
            ConcretePrototype1 c1 = (ConcretePrototype1)p1.Clone();
            Console.WriteLine("Cloned: {0}", c1.Id);
            ConcretePrototype2 p2 = new ConcretePrototype2("135472");
            ConcretePrototype2 c2 = (ConcretePrototype2)p2.Clone();
            Console.WriteLine("Cloned: {0}", c2.Id);

            Console.WriteLine();
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

    //原型模式1
    class WorkExperience : ICloneable
    {
        private string workDate;
        public string WorkDate
        {
            get { return workDate; }
            set { workDate = value; }
        }
        private string company;
        public string Company
        {
            get { return company; }
            set { company = value; }
        }
        public Object Clone()
        {
            return MemberwiseClone();
            //return (object)this.MemberwiseClone();
        }
    }
    class Resume : ICloneable
    {
        private string name;
        private string sex;
        private string age;

        private WorkExperience work;
        public Resume(string name)
        {
            this.name = name;
            work = new WorkExperience();
        }
        private Resume(WorkExperience work)  //克隆工作经历
        {
            this.work = (WorkExperience)work.Clone();
        }

        public void SetPersonalInfo(string sex, string age)
        {
            this.sex = sex;
            this.age = age;
        }

        public void SetWorkExperience(string workDate, string company)
        {
            work.WorkDate = workDate;
            work.Company = company;
        }

        public void Display()
        {
            Console.WriteLine("{0},{1},{2}", name, sex, age);
            Console.WriteLine("工作经历 {0},{1}", work.WorkDate, work.Company);
        }

        public Object Clone()
        {
            Resume obj = new Resume(this.work); //给关键字段去赋值
            obj.name = this.name;
            obj.sex = this.sex;
            obj.age = this.age;
            return obj;
        }
    }
    //原型模式2
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
}
