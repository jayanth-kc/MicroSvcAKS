using Autofac;
using DesignPatterns.CreationalPatterns.Builder;
using DesignPatterns.CreationalPatterns.Builder.Function;
using DesignPatterns.CreationalPatterns.Factory;
using DesignPatterns.CreationalPatterns.Factory.IOC_DI;
using DesignPatterns.CreationalPatterns.Prototype;
using DesignPatterns.CreationalPatterns.Singleton;
using DesignPatterns.SolidPrinciples;
using DesignPatterns.StructuralPatterns.Adapter;
using DesignPatterns.StructuralPatterns.Bridge;
using DesignPatterns.StructuralPatterns.Composite;
using DesignPatterns.StructuralPatterns.Decorator;
using DesignPatterns.StructuralPatterns.Facade;
using DesignPatterns.StructuralPatterns.Flyweight;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using JetBrains.dotMemoryUnit;
using DesignPatterns.StructuralPatterns.Flyweight.Fly;
using DesignPatterns.StructuralPatterns.Proxy;
using DesignPatterns.BehaviouralPatterns;
using DesignPatterns.BehaviouralPatterns.ChainOfReasponsibility;
using DesignPatterns.BehaviouralPatterns.Command;
using DesignPatterns.BehaviouralPatterns.Command2;
using DesignPatterns.BehaviouralPatterns.Command.Composite;
using DesignPatterns.BehaviouralPatterns.Iterator;
using DesignPatterns.BehaviouralPatterns.Interpreter;
using DesignPatterns.BehaviouralPatterns.Mediator;
using DesignPatterns.BehaviouralPatterns.Momento;
using DesignPatterns.BehaviouralPatterns.Observer;
using DesignPatterns.BehaviouralPatterns.iObserver;
using System.Reactive.Linq;
using System.ComponentModel;
using DesignPatterns.BehaviouralPatterns.BObserver;
using DesignPatterns.BehaviouralPatterns.State;
using Stateless;
using DesignPatterns.BehaviouralPatterns.Stratergy;
using DesignPatterns.BehaviouralPatterns.Template;
using DesignPatterns.BehaviouralPatterns.Visitor;

namespace DesignPatterns
{
    class Program
    {
        #region Liskov's
        //For Liskov's
        static public int Area(Rectangle r) => r.Width * r.Height;
        #endregion

        static void Main(string[] args)
        {
            #region SOLID Design Principles

            #region Single Reasponsibility
            //A class should have one, and only one, reason to change.

            var sr = new SingleReasponsibility();
            sr.AddEntry("I cried today.");
            sr.AddEntry("I ate a bug.");
            Console.WriteLine(sr);

            var p = new Persistence();
            var filename = @"c:\temp\journal.txt";
            p.SaveToFile(sr, filename);
            Process.Start(filename);
            #endregion

            #region Open Close
            // should be open for extension, but closed for modification” which means you should be able to extend a class behavior, without modifying it.

            var apple = new SolidPrinciples.Product("Apple", Color.Green, Size.Small);
            var tree = new SolidPrinciples.Product("Tree", Color.Green, Size.Large);
            var house = new SolidPrinciples.Product("House", Color.Blue, Size.Large);

            SolidPrinciples.Product[] products = { apple, tree, house };

            var pf = new ProductFilter();
            WriteLine("Green products (old):");
            foreach (var p0 in pf.FilterByColor(products, Color.Green))
                WriteLine($" - {p0.Name} is green");

            // ^^ BEFORE

            // vv AFTER
            var bf = new BetterFilter();
            WriteLine("Green products (new):");
            foreach (var p11 in bf.Filter(products, new ColorSpecification(Color.Green)))
                WriteLine($" - {p11.Name} is green");

            WriteLine("Large products");
            foreach (var p22 in bf.Filter(products, new SizeSpecification(Size.Large)))
                WriteLine($" - {p22.Name} is large");

            WriteLine("Large blue items");
            foreach (var p3 in bf.Filter(products,
              new AndSpecification<SolidPrinciples.Product>(new ColorSpecification(Color.Blue), new SizeSpecification(Size.Large)))
            )
            {
                WriteLine($" - {p3.Name} is big and blue");
            }
            #endregion

            #region Liskov’s Substitution
            /*The principle was introduced by Barbara Liskov in 1987 and according to this principle 
             * “Derived or child classes must be substitutable for their base or parent classes“. 
             * This principle ensures that any class that is the child of a parent class should be usable in place of its 
             * parent without any unexpected behavior.*/

            Rectangle rc = new Rectangle(2, 3);
            WriteLine($"{rc} has area {Area(rc)}");

            // should be able to substitute a base type for a subtype
            /*Square*/
            Rectangle sq = new SolidPrinciples.Square();
            sq.Width = 4;
            WriteLine($"{sq} has area {Area(sq)}");
            #endregion

            #region Interface Segration
            //should not be forced to implement any methods they don’t use

            SolidPrinciples.Document d = new SolidPrinciples.Document();

            IMachine multiFunctionPrinter = new MultiFunctionPrinter();
            multiFunctionPrinter.Print(d);
            multiFunctionPrinter.Scan(d);
            multiFunctionPrinter.Fax(d);

            IMachine oldFashionedPrinter = new OldFashionedPrinter();
            oldFashionedPrinter.Print(d);//it wont support scan and Fax.
            //In both the above it is going to implement all the declerations from the interface

            //Segrate the interface based on the requirement
            IPrinter print = new Printer();
            print.Print(d);

            //IScanner scan = new Photocopier();
            //scan.Scan(d);
            //IPrinter print1 = new Photocopier();
            //print1.Print(d);
            //in above interface is segrated but to access the class implemenatons need to go by specific interface

            //if it is required on a single then need to have sinle interface inheriting both print and scan for multifunctional
            //IMultiFunctionDevice multifuncdevice = new MultiFunctionMachine();
            //multifuncdevice.Print(d);
            //multifuncdevice.Scan(d);



            #endregion

            #region Dependency Inversion
            /* high - level modules / classes should not depend on low - level modules / classes.Both should depend upon abstractions. 
             Secondly, abstractions should not depend upon details. Details should depend upon abstractions*/

            var parent = new DesignPatterns.SolidPrinciples.Person { Name = "John" };
            var child1 = new DesignPatterns.SolidPrinciples.Person { Name = "Chris" };
            var child2 = new DesignPatterns.SolidPrinciples.Person { Name = "Matt" };

            // low-level modules
            var relationships = new Relationships();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);
            
            //High Level Module
            new Research(relationships);
            #endregion

            #endregion

            #region Creational Patterns

            #region Builder
            //create different parts of an object, step by step, and then connect all the parts together

            #region Builder 1
            // if you want to build a simple HTML paragraph using StringBuilder
            var hello = "hello";
            var sb = new StringBuilder();
            sb.Append("<p>");
            sb.Append(hello);
            sb.Append("</p>");
            WriteLine(sb);

            // now I want an HTML list with 2 words in it
            var words = new[] { "hello", "world" };
            sb.Clear();
            sb.Append("<ul>");
            foreach (var word in words)
            {
                sb.AppendFormat("<li>{0}</li>", word);
            }
            sb.Append("</ul>");
            WriteLine(sb);

            //With Builder Pattern
            // ordinary non-fluent builder
            var builder = new HtmlBuilder("ul");
            builder.AddChild("li", "hello");
            builder.AddChild("li", "world");
            WriteLine(builder.ToString());

            // fluent builder
            sb.Clear();
            builder.Clear(); // disengage builder from the object it's building, then...
            builder.AddChildFluent("li", "hello").AddChildFluent("li", "world");
            WriteLine(builder);
            #endregion

            #region Builder 2
            Report report;
            ReportDirector reportDirector = new ReportDirector();
            // Construct and display Reports
            PDFReport pdfReport = new PDFReport();
            report = reportDirector.MakeReport(pdfReport);
            report.DisplayReport();
            Console.WriteLine("-------------------");
            ExcelReport excelReport = new ExcelReport();
            report = reportDirector.MakeReport(excelReport);
            report.DisplayReport();
            #endregion

            #region Builder Inheritence Recursive Generics

            var me = DesignPatterns.CreationalPatterns.Builder.Person.New
               .Called("XYZ")
               .WorksAsA("Fiserv")
               .Born(DateTime.UtcNow)
               .Build();
            Console.WriteLine(me);

            #endregion

            #region Facet Builder
            var pb = new DesignPatterns.CreationalPatterns.Builder.Facet.PersonBuilder();
            DesignPatterns.CreationalPatterns.Builder.Facet.Person person = pb
              .Lives
                .At("123 London Road")
                .In("London")
                .WithPostcode("SW12BC")
              .Works
                .At("Fabrikam")
                .AsA("Engineer")
                .Earning(123000);

            WriteLine(person);
            #endregion

            #region Functiona Builder

            var pb1 = new DesignPatterns.CreationalPatterns.Builder.Function.PersonBuilder();
            var person1 = pb1.Called("XYZ").WorksAsA("Programmer").Build();
            #endregion

            #endregion

            #region Factory

            #region Factory
            //Define an interface for creating an object, 
            //but let the subclasses decide which class to instantiate. 
            //The Factory method lets a class defer instantiation it uses to subclasses
            var p1 = new CreationalPatterns.Factory.Point(2, 3, CreationalPatterns.Factory.Point.CoordinateSystem.Cartesian);
            var origin = CreationalPatterns.Factory.Point.Origin;

            var p2 = CreationalPatterns.Factory.Point.Factory.NewCartesianPoint(1, 2);
            #endregion

            #region Factory2
            CreditCard creditCard = new PlatinumFactory().CreateProduct();
            #endregion

            #region Async Factory
           // var asyncfoo = await AsyncFoo.CreateAsync();
            #endregion

            #region Abstract Factory
            //The Abstract Factory Design Pattern provides a way to encapsulate a group of individual factories 
            //that have a common theme without specifying their concrete classes

            Animal animal = null;
            AnimalFactory animalFactory = null;
            string speakSound = null;
            // Create the Sea Factory object by passing the factory type as Sea
            animalFactory = AnimalFactory.CreateAnimalFactory("Sea");
            Console.WriteLine("Animal Factory type : " + animalFactory.GetType().Name);
            Console.WriteLine();
            // Get Octopus Animal object by passing the animal type as Octopus
            animal = animalFactory.GetAnimal("Octopus");
            Console.WriteLine("Animal Type : " + animal.GetType().Name);
            speakSound = animal.speak();
            Console.WriteLine(animal.GetType().Name + " Speak : " + speakSound);
            Console.WriteLine();
            Console.WriteLine("--------------------------");
            // Create Land Factory object by passing the factory type as Land
            animalFactory = AnimalFactory.CreateAnimalFactory("Land");
            Console.WriteLine("Animal Factory type : " + animalFactory.GetType().Name);
            Console.WriteLine();
            // Get Lion Animal object by passing the animal type as Lion
            animal = animalFactory.GetAnimal("Lion");
            Console.WriteLine("Animal Type : " + animal.GetType().Name);
            speakSound = animal.speak();
            Console.WriteLine(animal.GetType().Name + " Speak : " + speakSound);
            Console.WriteLine();
            // Get Cat Animal object by passing the animal type as Cat
            animal = animalFactory.GetAnimal("Cat");
            Console.WriteLine("Animal Type : " + animal.GetType().Name);
            speakSound = animal.speak();
            Console.WriteLine(animal.GetType().Name + " Speak : " + speakSound);
            Console.Read();
            #endregion

            #region Abstract Factory 2
            var machine = new HotDrinkMachine();
            //var drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 300);
            //drink.Consume();

            IHotDrink drink = machine.MakeDrink();
            drink.Consume();
            #endregion

            #region Inversion of Control and Dependency Injection
            /*
             * The Inversion of Control (IoC) is a design principle but some people also refer to it as a pattern. 
             * As the name suggests, it is used to invert the various types of controls in object-oriented design to achieve loose coupling between the classes. 
             * Here, the control means any extra responsibilities a class has other than its main or fundamental responsibility, 
             * such as control over the flow of an application execution, control over the flow of an object creation or dependent object creation and binding, etc
             * */

            EmployeeBusinessLogic BL = new EmployeeBusinessLogic();
            DesignPatterns.CreationalPatterns.Factory.IOC_DI.Employee employeeDetails;// = BL.GetEmployeeDetails(1);

            //Dependecy injection by property
            BL = new EmployeeBusinessLogic();
            BL.EmpDataAccess = new EmployeeDataAccess();//property injection
            employeeDetails = BL.GetEmployeeDetails(1);
            //Dependency injection by Method
            BL = new EmployeeBusinessLogic();
            BL.SetDependency(new EmployeeDataAccess());
            employeeDetails = BL.GetEmployeeDetails(1);

            #endregion

            #endregion

            #region Prototype
            //a way to create new objects from the existing instance of the object. 
            //That means it clone the existing object with its data into a new object. 
            //If we do any changes to the cloned object (i.e. new object) then it does not affect the original object.

            #region IClonable is a Shallow Copy
            var john = new DesignPatterns.CreationalPatterns.PrototypeICloneable.Person(new[] { "John", "Smith" }, new DesignPatterns.CreationalPatterns.PrototypeICloneable.Address("London Road", 123));

            var jane = (DesignPatterns.CreationalPatterns.PrototypeICloneable.Person)john.Clone();
            jane.Address.HouseNumber = 321; // oops, John is now at 321

            // this doesn't work
            //var jane = john;

            // but clone is typically shallow copy
            jane.Names[0] = "Jane";

            WriteLine(john);
            WriteLine(jane);
            #endregion

            #region Memberwise clone - Shallow copy
            //Value(non static) types will be copiedand referecne will point to same memory location
            DesignPatterns.CreationalPatterns.Prototype.Employee emp1 = new DesignPatterns.CreationalPatterns.Prototype.Employee();
            emp1.Name = "Anurag";
            emp1.Department = "IT";
            DesignPatterns.CreationalPatterns.Prototype.Employee emp2 = emp1.GetClone();
            emp2.Name = "Pranaya";
            Console.WriteLine("Emplpyee 1: ");
            Console.WriteLine("Name: " + emp1.Name + ", Department: " + emp1.Department);
            Console.WriteLine("Emplpyee 2: ");
            Console.WriteLine("Name: " + emp2.Name + ", Department: " + emp2.Department);
            Console.Read();
            #endregion

            #region Deep Copy
            DesignPatterns.CreationalPatterns.PrototypeDeepCopy.Employee emp3 = new DesignPatterns.CreationalPatterns.PrototypeDeepCopy.Employee();
            emp3.Name = "Anurag";
            emp3.Department = "IT";
            emp3.EmpAddress = new DesignPatterns.CreationalPatterns.PrototypeDeepCopy.Address() { address = "BBSR" };
            DesignPatterns.CreationalPatterns.PrototypeDeepCopy.Employee emp4 = emp3.GetClone();
            emp4.Name = "Pranaya";
            emp4.EmpAddress.address = "Mumbai";
            Console.WriteLine("Emplpyee 1: ");
            Console.WriteLine("Name: " + emp3.Name + ", Address: " + emp3.EmpAddress.address + ", Dept: " + emp3.Department);
            Console.WriteLine("Emplpyee 2: ");
            Console.WriteLine("Name: " + emp4.Name + ", Address: " + emp4.EmpAddress.address + ", Dept: " + emp4.Department);
            Console.Read();
            #endregion

            #region Serialization Deep Copy
            DesignPatterns.CreationalPatterns.Prototype.Foo foo = new DesignPatterns.CreationalPatterns.Prototype.Foo { Stuff = 42, Whatever = "abc" };

            //Foo foo2 = foo.DeepCopy(); // crashes without [Serializable]
            DesignPatterns.CreationalPatterns.Prototype.Foo foo2 = foo.DeepCopyXml();

            foo2.Whatever = "xyz";
            WriteLine(foo);
            WriteLine(foo2);
            #endregion

            #endregion

            #region Singleton
            //only one instance of a particular class is going to be created and then provide simple global access to that instance for the entire application.

            #region Singleton 
            var db = SingletonDatabase.Instance;

            // works just fine while you're working with a real database.
            var city = "Tokyo";
            WriteLine($"{city} has population {db.GetPopulation(city)}");
            #endregion

            #region Singleton No Thread-Safe Singleton
            Singleton instace1 = Singleton.GetInstance;
            instace1.PrintDetails("From instace1");
            Singleton instace2 = Singleton.GetInstance;
            instace2.PrintDetails("From instace2");

            /*
            * Instantiating singleton from a derived class. 
            * This violates singleton pattern principles.
            */
            SingletonInheritance.DerivedSingleton derivedObj = new SingletonInheritance.DerivedSingleton();
            derivedObj.PrintDetails("From Derived");

            #endregion

            #region Parallel Programming
            Parallel.Invoke(
                () => PrintTeacherDetails(),
                () => PrintStudentdetails()
                );
            Console.ReadLine();
            //Two threand will be created with one instance on Single ton object in each thread

            /* Lazy Initilization
             * until and unless we invoke the GetInstance Property of the Singleton class, 
             * the Singleton instance is not created. That means when we invoke the GetInstance Property of 
             * the Singleton class then only the Singleton object is created. This Delay of Singleton Instance 
             * creation is called Lazy Initialization
             */
            /* Multi Thread 
             * The lazy initialization i.e. the on-demand object creation of the singleton class works fine 
             * when we invoke the GetInstance property in a Single-threaded environment. 
             * But in a multi-threaded environment, it will not work as expected. In a multi-thread environment, 
             * the lazy initialization may end up creating multiple instances of the singleton class when 
             * multiple threads invoke the GetInstance property parallelly at the same time
             */
            #endregion

            #region Thread Safe Singleton
            //using lock solves the multithreading issue

            instace1 = Singleton.GetInstanceLock;
            instace1.PrintDetails("From instace1");
            instace2 = Singleton.GetInstanceLock;
            instace2.PrintDetails("From instace2");
            #endregion

            #region Double Checking
            /*using lock solves the multithreading issue. But the problem is that it is slow down your application 
              * as only one thread can access the GetInstance property at any given point of time. 
              * We can overcome the above problem by using the Double-checked locking mechanism */

            instace1 = Singleton.GetInstanceDoubleCheck;
            instace1.PrintDetails("From instace1");
            instace2 = Singleton.GetInstanceDoubleCheck;
            instace2.PrintDetails("From instace2");

            #endregion

            #region Eager or Non Lazy loading
            /*initialize the singleton object at the time of application start-up rather than on-demand and keep it 
             * ready in memory to be used in the future. The advantage of using Eager Loading in the 
             * Singleton design pattern is that the CLR (Common Language Runtime) will take care of 
             * object initialization and thread-safety. That means we will not require to write any code explicitly 
             * for handling the thread-safety for a multithreaded environment. 
             */
            Parallel.Invoke(
                () => PrintTeacherDetailsEager(),
                () => PrintStudentdetailsEager()
                );
            Console.ReadLine();
            //Application will create only one instace of the thread
            #endregion

            #region Lazy loading
            /*Lazy or Deferred Loading is a design pattern or you can say its a concept which is commonly used to 
             * delay the initialization of an object until the point at which it is needed.
             * */
            Parallel.Invoke(
              () => PrintTeacherDetailsLazy(),
              () => PrintStudentdetailsLazy()
              );
            Console.ReadLine();
            //Application will create only one instace of the thread. instiatiated of the object  on-demand or you can say object when needed
            #endregion

            #region DI
            var builder1 = new ContainerBuilder();
            builder1.RegisterType<EventBroker>().SingleInstance();
            builder1.RegisterType<DesignPatterns.CreationalPatterns.Singleton.Foo>();

            using (var c = builder1.Build())
            {
                var foo3 = c.Resolve<DesignPatterns.CreationalPatterns.Singleton.Foo>();
                var foo4 = c.Resolve<DesignPatterns.CreationalPatterns.Singleton.Foo>();

                WriteLine(ReferenceEquals(foo3, foo4));
                WriteLine(ReferenceEquals(foo3.Broker, foo4.Broker));
            }


            #endregion

            #region Monostate
            var ceo = new ChiefExecutiveOfficer();
            ceo.Name = "Adam Smith";
            ceo.Age = 55;

            var ceo2 = new ChiefExecutiveOfficer();
            WriteLine(ceo2);//it will be same as ceo because of static fields
            #endregion

            #region PreThread - Multi threading
            var t1 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"t1: " + PerThreadSingleton.Instance.Id);
            }); //new thread
            var t2 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"t2: " + PerThreadSingleton.Instance.Id);
                Console.WriteLine($"t2 again: " + PerThreadSingleton.Instance.Id);
            });//Common thread for above two lines
            //basically on instacne per thead
            Task.WaitAll(t1, t2);
            #endregion

            #endregion

            #endregion

            #region Structural Patterns
            //ease the design by identifying a simple way to realize the relationship among entities
            //use inheritance to compose interfaces. The structural object patterns define ways to compose objects to obtain new functionality.
            
            #region Adapter
            #region Adapter
            //works as a bridge between two incompatible interfaces. This design pattern involves a single class 
            //called adapter which is responsible for communication between two independent or incompatible interfaces

            string[,] employeesArray = new string[5, 4]
           {
                {"101","John","SE","10000"},
                {"102","Smith","SE","20000"},
                {"103","Dev","SSE","30000"},
                {"104","Pam","SE","40000"},
                {"105","Sara","SSE","50000"}
           };

            ITarget target = new EmployeeAdapter();
            Console.WriteLine("HR system passes employee string array to Adapter\n");
            target.ProcessCompanySalary(employeesArray);
            Console.Read();
            #endregion

            #region Adapter NoCache and Cache
            Draw();
            Draw();
            #endregion

            #endregion

            #region Bridge
            //Decouples an abstraction from its implementation so that the two can vary independently.
            /*  there are 2 parts. The first part is the abstraction and the second part is the implementation. 
             *  The bridge design pattern allows the abstraction and implementation to be developed independently 
             *  and the client code can access only the abstraction part without being concerned about the 
             *  implementation part
             */
            #region Bridge
            //AbstractRemoteControl :  Abstract Class
            //Abstract Implementation : SonyRemoteControl
            //LEDTV : Implementer interface
            //SonyLedTv: Implementer's implementation
            AbstractRemoteControl sonyRemoteControl = new SonyRemoteControl(new SonyLedTv());
            sonyRemoteControl.SwitchOn();
            sonyRemoteControl.SetChannel(101);
            sonyRemoteControl.SwitchOff();

            Console.WriteLine();
            SamsungRemoteControl samsungRemoteControl = new SamsungRemoteControl(new SamsungLedTv());
            samsungRemoteControl.SwitchOn();
            samsungRemoteControl.SetChannel(202);
            samsungRemoteControl.SwitchOff();

            Console.ReadKey();
            #endregion

            #region Bridge2
            Console.WriteLine("Select the Message Type 1. For longmessage or 2. For shortmessage");
            //int MessageType = Convert.ToInt32(Console.ReadLine());
            int MessageType = Convert.ToInt32(1);
            Console.WriteLine("Please enter the message that you want to send");
            string Message = Console.ReadLine();
            if (MessageType == 1)
            {
                AbstractMessage longMessage = new LongMessage(new EmailMessageSender());
                longMessage.SendMessage(Message);
            }
            else
            {
                AbstractMessage shortMessage = new ShortMessage(new SmsMessageSender());
                shortMessage.SendMessage(Message);
            }
            Console.ReadKey();
            #endregion

            #endregion

            #region Composite
            /*allows us to have a tree structure and ask each node in the tree structure to perform a task. 
             * That means this pattern creates a tree structure of a group of objects. 
             * The Composite Pattern is used where we need to treat a group of objects in a similar way as a 
             * single unit object.*/
            #region Composite
            //Creating Leaf Objects
            StructuralPatterns.Composite.IComponent hardDisk = new Leaf("Hard Disk", 2000);
            StructuralPatterns.Composite.IComponent ram = new Leaf("RAM", 3000);
            StructuralPatterns.Composite.IComponent cpu = new Leaf("CPU", 2000);
            StructuralPatterns.Composite.IComponent mouse = new Leaf("Mouse", 2000);
            StructuralPatterns.Composite.IComponent keyboard = new Leaf("Keyboard", 2000);
            //Creating composite objects
            Composite motherBoard = new Composite("Peripherals");
            Composite cabinet = new Composite("Cabinet");
            Composite peripherals = new Composite("Peripherals");
            Composite computer = new Composite("Computer");
            //Creating tree structure
            //Ading CPU and RAM in Mother board
            motherBoard.AddComponent(cpu);
            motherBoard.AddComponent(ram);
            //Ading mother board and hard disk in Cabinet
            cabinet.AddComponent(motherBoard);
            cabinet.AddComponent(hardDisk);
            //Ading mouse and keyborad in peripherals
            peripherals.AddComponent(mouse);
            peripherals.AddComponent(keyboard);
            //Ading cabinet and peripherals in computer
            computer.AddComponent(cabinet);
            computer.AddComponent(peripherals);
            //To display the Price of Computer
            computer.DisplayPrice();
            Console.WriteLine();
            //To display the Price of Keyboard
            keyboard.DisplayPrice();
            Console.WriteLine();
            //To display the Price of Cabinet
            cabinet.DisplayPrice();
            Console.Read();
            #endregion

            #region Composite 2
            var drawing = new GraphicObject { Name = "My Drawing" };
            drawing.Children.Add(new StructuralPatterns.Composite.Square { Color = "Red" });
            drawing.Children.Add(new StructuralPatterns.Composite.Circle { Color = "Yellow" });

            var group = new GraphicObject();
            group.Children.Add(new StructuralPatterns.Composite.Circle { Color = "Blue" });
            group.Children.Add(new StructuralPatterns.Composite.Square { Color = "Blue" });
            drawing.Children.Add(group);

            WriteLine(drawing);
            #endregion

            #endregion

            #region Decorator
            /*Allows us to dynamically add new functionalities to an existing object without altering or modifying 
             * its structure and this design pattern acts as a wrapper to the existing class.
             * Dynamically changes the functionality of an object at runtime without impacting the existing functionality of the objects. 
             * In short, this decorator design pattern adds additional functionalities to the object by wrapping it
             */
            #region Decorator
            StructuralPatterns.Decorator.ICar bmwCar1 = new BMWCar();
            bmwCar1.ManufactureCar();
            Console.WriteLine(bmwCar1 + "\n");
            DieselCarDecorator carWithDieselEngine = new DieselCarDecorator(bmwCar1);
            carWithDieselEngine.ManufactureCar();
            Console.WriteLine();
            StructuralPatterns.Decorator.ICar bmwCar2 = new BMWCar();
            PetrolCarDecorator carWithPetrolEngine = new PetrolCarDecorator(bmwCar2);
            carWithPetrolEngine.ManufactureCar();
            #endregion

            #region Decorator 2
            PlainPizza plainPizzaObj = new PlainPizza();
            string plainPizza = plainPizzaObj.MakePizza();
            Console.WriteLine(plainPizza);
            PizzaDecorator chickenPizzaDecorator = new ChickenPizzaDecorator(plainPizzaObj);
            string chickenPizza = chickenPizzaDecorator.MakePizza();
            Console.WriteLine("\n'" + chickenPizza + "' using ChickenPizzaDecorator");
            VegPizzaDecorator vegPizzaDecorator = new VegPizzaDecorator(plainPizzaObj);
            string vegPizza = vegPizzaDecorator.MakePizza();
            Console.WriteLine("\n'" + vegPizza + "' using VegPizzaDecorator");
            Console.Read();
            #endregion

            #region Multiple Inheritance
            //Dragon is an 
            var dragon = new Dragon(new Bird(), new Lizard());
            dragon.Fly();
            dragon.Crawl();
            #endregion

            #region Dynamic Static
            //Dynamic
            //Square is a componenet implementation of component Shape
            var square = new StructuralPatterns.Decorator.Square(1.23f);
            WriteLine(square.AsString());
            //ColoredShape is the dynamic decorator, adding more functionality to the Square
            var redSquare = new ColoredShape(square, "red");
            WriteLine(redSquare.AsString());

            var redHalfTransparentSquare = new TransparentShape(redSquare, 0.5f);
            WriteLine(redHalfTransparentSquare.AsString());

            // static
            ColoredShape<StructuralPatterns.Decorator.Circle> blueCircle = new ColoredShape<StructuralPatterns.Decorator.Circle>("blue");
            WriteLine(blueCircle.AsString());

            TransparentShape<ColoredShape<StructuralPatterns.Decorator.Square>> blackHalfSquare = new TransparentShape<ColoredShape<StructuralPatterns.Decorator.Square>>(0.4f);
            WriteLine(blackHalfSquare.AsString());
            #endregion

            #endregion

            #region Facade
            /*provide a unified interface to a set of interfaces in a subsystem. 
             * The Façade Design Pattern defines a higher-level interface that makes the subsystem easier to use
             * The Façade (usually a wrapper) sits on the top of a group of subsystems and allows them to communicate in a unified manner.
             * 
             * We want to provide a simple interface to a complex subsystem. Subsystems often get more complex as they evolve.
             * There are many dependencies between clients and the implementation classes
             */
            #region Facade
            Order order = new Order();
            order.PlaceOrder();
            Console.Read();
            #endregion

            #endregion

            #region Flyweight
            /*create a large number of objects of almost similar nature. 
             * A large number of objects consumes a large amount of memory and the Flyweight design pattern provides 
             * a solution for reducing the load on memory by sharing objects.
             * 
             * Space optimization Technique
             */
            #region Flyweight
            var users = new List<StructuralPatterns.Flyweight.User>();

            var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());

            foreach (var firstName in firstNames)
                foreach (var lastName in lastNames)
                    users.Add(new StructuralPatterns.Flyweight.User($"{firstName} {lastName}"));

           ForceGC();

            //dotMemory.Check(memory =>
            //{
            //    WriteLine(memory.SizeInBytes);
            //});
            #endregion

            #region Flyweight 2
            Console.WriteLine("\n Red color Circles ");
            for (int i = 0; i < 3; i++)
            {
                DesignPatterns.StructuralPatterns.Flyweight.Fly.Circle circle = (DesignPatterns.StructuralPatterns.Flyweight.Fly.Circle)ShapeFactory.GetShape("circle");
                circle.SetColor("Red");
                circle.Draw();
            }
            Console.WriteLine("\n Green color Circles ");
            for (int i = 0; i < 3; i++)
            {
                DesignPatterns.StructuralPatterns.Flyweight.Fly.Circle circle = (DesignPatterns.StructuralPatterns.Flyweight.Fly.Circle)ShapeFactory.GetShape("circle");
                circle.SetColor("Green");
                circle.Draw();
            }
            Console.WriteLine("\n Blue color Circles");
            for (int i = 0; i < 3; ++i)
            {
                DesignPatterns.StructuralPatterns.Flyweight.Fly.Circle circle = (DesignPatterns.StructuralPatterns.Flyweight.Fly.Circle)ShapeFactory.GetShape("circle");
                circle.SetColor("Blue");
                circle.Draw();
            }
            Console.WriteLine("\n Orange color Circles");
            for (int i = 0; i < 3; ++i)
            {
                DesignPatterns.StructuralPatterns.Flyweight.Fly.Circle circle = (DesignPatterns.StructuralPatterns.Flyweight.Fly.Circle)ShapeFactory.GetShape("circle");
                circle.SetColor("Orange");
                circle.Draw();
            }
            Console.WriteLine("\n Black color Circles");
            for (int i = 0; i < 3; ++i)
            {
                DesignPatterns.StructuralPatterns.Flyweight.Fly.Circle circle = (DesignPatterns.StructuralPatterns.Flyweight.Fly.Circle)ShapeFactory.GetShape("circle");
                circle.SetColor("Black");
                circle.Draw();
            }
            Console.ReadKey();
            #endregion

            #region Flyweight 3
            var ft = new FormattedText("This is a brave new world");
            ft.Capitalize(10, 15);//Consuming memory as need to load the memory and compare the character by character
            WriteLine(ft);

            var bft = new BetterFormattedText("This is a brave new world");
            bft.GetRange(10, 15).Capitalize = true;
            WriteLine(bft);
            #endregion

            #endregion

            #region Proxy
            /* a surrogate (act on behalf of other) or placeholder for another object to control the access to it. 
             * Proxy means ‘in place of‘ or ‘representing‘ or ‘on behalf of‘.
             * we can define a proxy is a class functioning as an interface to something else. 
             * The proxy could interface to anything such as a network connection, a large object in memory, 
             * a file or some other resources that are expensive or impossible to duplicate
             * Virtual Proxy: A virtual proxy is a place holder for “expensive to create” objects. The real object is only created when a client first requests or accesses the object.
             * Remote Proxy: A remote proxy provides local representation for an object that resides in a different address space.
             * Protection Proxy: A protection proxy control access to a sensitive master object. The surrogate object checks that the caller has the access permissions required prior to forwarding the request.
             * A server that sits between a client application such as a web browser and a real server is called a Proxy Server. 
             * This Proxy server intercepts all the incoming requests for the real server to see if it can fulfill the requests by itself. 
             * If not then it will forward the requests to the real server.*/

            #region Proxy "Dynamic"
            Console.WriteLine("Client passing employee with Role Developer to folderproxy");
            DesignPatterns.StructuralPatterns.Proxy.Employee pemp1 = new DesignPatterns.StructuralPatterns.Proxy.Employee("Anurag", "Anurag123", "Developer");
            SharedFolderProxy folderProxy1 = new SharedFolderProxy(pemp1);
            folderProxy1.PerformRWOperations();
            Console.WriteLine();
            Console.WriteLine("Client passing employee with Role Manager to folderproxy");
            DesignPatterns.StructuralPatterns.Proxy.Employee pemp2 = new DesignPatterns.StructuralPatterns.Proxy.Employee("Pranaya", "Pranaya123", "Manager");
            SharedFolderProxy folderProxy2 = new SharedFolderProxy(pemp2);
            folderProxy2.PerformRWOperations();
            Console.Read();
            #endregion

            #region Virtual Proxy
            IImage Image1 = new ProxyImage("Tiger Image");

            Console.WriteLine("Image1 calling DisplayImage first time :");
            Image1.DisplayImage(); // loading necessary
            Console.WriteLine("Image1 calling DisplayImage second time :");
            Image1.DisplayImage(); // loading unnecessary
            Console.WriteLine("Image1 calling DisplayImage third time :");
            Image1.DisplayImage(); // loading unnecessary
            Console.WriteLine();
            IImage Image2 = new ProxyImage("Lion Image");
            Console.WriteLine("Image2 calling DisplayImage first time :");
            Image2.DisplayImage(); // loading necessary
            Console.WriteLine("Image2 calling DisplayImage second time :");
            Image2.DisplayImage(); // loading unnecessary
            Console.ReadKey();
            #endregion

            #region  Protection
            DesignPatterns.StructuralPatterns.Proxy.ICar car = new CarProxy(new Driver(12)); // 22
            car.Drive();
            #endregion

            #endregion

            #endregion

            #region Behavioural Pattern
            /* Concerned with the interaction between the objects. 
             * The interaction between the objects should be in such a way that they are talking to each other and 
             * still are loosely coupled. The loose coupling is the key to n-tier architecture.*/

            #region Chain Of Reasponsibility
            /*void coupling the sender of a request to its receiver by giving more than one receiver object a chance to handle the request. 
             * Chain the receiving objects and pass the request along the chain until an object handle it.*/
            #region Chain of Reasponsibility
            ATM atm = new ATM();
            Console.WriteLine("\n Requested Amount 4600");
            atm.withdraw(4600);
            Console.WriteLine("\n Requested Amount 1900");
            atm.withdraw(1900);
            Console.WriteLine("\n Requested Amount 600");
            atm.withdraw(600);
            Console.Read();
            #endregion

            #region Chain2
            var goblin = new Creature("Goblin", 2, 2);
            WriteLine(goblin);

            var root = new CreatureModifier(goblin);

           // root.Add(new NoBonusesModifier(goblin));

            WriteLine("Let's double goblin's attack...");
            root.Add(new DoubleAttackModifier(goblin));

            WriteLine("Let's increase goblin's defense");
            root.Add(new IncreaseDefenseModifier(goblin));

            // eventually...
            root.Handle();
            WriteLine(goblin);
            #endregion

            #region Broker Chain
            var game = new DesignPatterns.BehaviouralPatterns.ChainOfReasponsibility.Broker.Game();
            var goblin1 = new DesignPatterns.BehaviouralPatterns.ChainOfReasponsibility.Broker.Creature(game, "Strong Goblin", 3, 3);
            WriteLine(goblin1);

            using (new DesignPatterns.BehaviouralPatterns.ChainOfReasponsibility.Broker.DoubleAttackModifier(game, goblin1))
            {
                WriteLine(goblin1);
                using (new DesignPatterns.BehaviouralPatterns.ChainOfReasponsibility.Broker.IncreaseDefenseModifier(game, goblin1))
                {
                    WriteLine(goblin1);
                }
            }

            WriteLine(goblin1);
            #endregion

            #endregion

            #region Command
            /*to encapsulate a request as an object (i.e. a command) and pass to an invoker, 
             * wherein the invoker does now knows how to service the request but uses the encapsulated command to 
             * perform an action.*/

            #region Command
            BehaviouralPatterns.Command.Document document = new BehaviouralPatterns.Command.Document(); //Receiver
            BehaviouralPatterns.Command.ICommand openCommand = new OpenCommand(document);//Request
            BehaviouralPatterns.Command.ICommand saveCommand = new SaveCommand(document);//Request
            BehaviouralPatterns.Command.ICommand closeCommand = new CloseCommand(document);//Request
            MenuOptions menu = new MenuOptions(openCommand, saveCommand, closeCommand); //Invoker
            menu.clickOpen();
            menu.clickSave();
            menu.clickClose();
            Console.ReadKey();
            #endregion

            #region Command2
            var ba = new BehaviouralPatterns.Command2.BankAccount();//Reciver
            var commands = new List<BehaviouralPatterns.Command2.BankAccountCommand>
      {
        new BehaviouralPatterns.Command2.BankAccountCommand(ba, BehaviouralPatterns.Command2.BankAccountCommand.Action.Deposit, 100),//Requester
        new BehaviouralPatterns.Command2.BankAccountCommand(ba, BehaviouralPatterns.Command2.BankAccountCommand.Action.Withdraw, 1000)
      };

            WriteLine(ba);

            foreach (var c in commands)
                c.Call();//invoker

            WriteLine(ba);

            foreach (var c in Enumerable.Reverse(commands))
                c.Undo();

            WriteLine(ba);
            #endregion

            #region Composite
            var cba = new DesignPatterns.BehaviouralPatterns.Command.Composite.BankAccount();
            var cmdDeposit = new DesignPatterns.BehaviouralPatterns.Command.Composite.BankAccountCommand(cba, DesignPatterns.BehaviouralPatterns.Command.Composite.BankAccountCommand.Action.Deposit, 100);
            var cmdWithdraw = new DesignPatterns.BehaviouralPatterns.Command.Composite.BankAccountCommand(cba, DesignPatterns.BehaviouralPatterns.Command.Composite.BankAccountCommand.Action.Withdraw, 1000);
            cmdDeposit.Call();
            cmdWithdraw.Call();
            WriteLine(ba);
            cmdWithdraw.Undo();
            cmdDeposit.Undo();
            WriteLine(ba);


            var from = new DesignPatterns.BehaviouralPatterns.Command.Composite.BankAccount();
            from.Deposit(100);
            var to = new DesignPatterns.BehaviouralPatterns.Command.Composite.BankAccount();

            var mtc = new MoneyTransferCommand(from, to, 1000);
            mtc.Call();


            // Deposited $100, balance is now 100
            // balance: 100
            // balance: 0

            WriteLine(from);
            WriteLine(to);
            #endregion

            #endregion

            #region Iterator 
            /*allows sequential access of elements without exposing the inside logic. 
             * That means using the Iterator Design Pattern we can access the elements of a collection object in a sequential manner without any need to know its internal representations.*/

            #region Iterator
            // Build a collection
            ConcreteCollection collection = new ConcreteCollection();
            collection.AddEmployee(new Elempoyee("Anurag", 100));
            collection.AddEmployee(new Elempoyee("Pranaya", 101));
            collection.AddEmployee(new Elempoyee("Santosh", 102));
            collection.AddEmployee(new Elempoyee("Priyanka", 103));
            collection.AddEmployee(new Elempoyee("Abinash", 104));
            collection.AddEmployee(new Elempoyee("Preety", 105));

            // Create iterator
            Iterator iterator = collection.CreateIterator();
            //looping iterator      
            Console.WriteLine("Iterating over collection:");

            for (Elempoyee emp = iterator.First(); !iterator.IsCompleted; emp = iterator.Next())
            {
                Console.WriteLine($"ID : {emp.ID} & Name : {emp.Name}");
            }
            Console.Read();
            #endregion

            #endregion

            #region Interpreter
            /*used in SQL parsing, symbol processing engine, etc.*/

            #region Interpreter
            List<AbstractExpression> objExpressions = new List<AbstractExpression>();
            Context context = new Context(DateTime.Now);
            Console.WriteLine("Please select the Expression  : MM DD YYYY or YYYY MM DD or DD MM YYYY ");
            context.expression = Console.ReadLine();
            string[] strArray = context.expression.Split(' ');
            foreach (var item in strArray)
            {
                if (item == "DD")
                {
                    objExpressions.Add(new DayExpression());
                }
                else if (item == "MM")
                {
                    objExpressions.Add(new MonthExpression());
                }
                else if (item == "YYYY")
                {
                    objExpressions.Add(new YearExpression());
                }
            }
            objExpressions.Add(new SeparatorExpression());
            foreach (var obj in objExpressions)
            {
                obj.Evaluate(context);
            }
            Console.WriteLine(context.expression);
            Console.Read();
            #endregion

            #region Interpreter 2
            //   1
            //  / \
            // 2   3

            // in-order:  213
            // preorder:  123
            // postorder: 231

            var iroot = new Node<int>(1,new Node<int>(2), new Node<int>(3));

            // C++ style
            var it = new InOrderIterator<int>(iroot);

            while (it.MoveNext())
            {
                Write(it.Current.Value);
                Write(',');
            }
            WriteLine();

            // C# style
            var itree = new BinaryTree<int>(iroot);

            WriteLine(string.Join(",", itree.NaturalInOrder.Select(x => x.Value)));

            // duck typing!
            foreach (var node in itree)
                WriteLine(node.Value);

            Console.ReadKey();
            #endregion

            #endregion

            #region Mediator
            /*reduce the communication complexity between multiple objects. 
             * This design pattern provides a mediator object and that mediator object normally handles all the 
             * communication complexities between different objects.
             * when an object needs to communicate to another object, then it does not call the other object directly, instead, 
             * it calls the mediator object and it is the responsibility of the mediator object to route the message to the destination object.*/

            #region Mediator
            FacebookGroupMediator facebookMediator = new ConcreteFacebookGroupMediator();
            BehaviouralPatterns.Mediator.User Ram = new ConcreteUser(facebookMediator, "Ram");
            BehaviouralPatterns.Mediator.User Dave = new ConcreteUser(facebookMediator, "Dave");
            BehaviouralPatterns.Mediator.User Smith = new ConcreteUser(facebookMediator, "Smith");
            BehaviouralPatterns.Mediator.User Rajesh = new ConcreteUser(facebookMediator, "Rajesh");
            BehaviouralPatterns.Mediator.User Sam = new ConcreteUser(facebookMediator, "Sam");
            BehaviouralPatterns.Mediator.User Pam = new ConcreteUser(facebookMediator, "Pam");
            BehaviouralPatterns.Mediator.User Anurag = new ConcreteUser(facebookMediator, "Anurag");
            BehaviouralPatterns.Mediator.User John = new ConcreteUser(facebookMediator, "John");
            facebookMediator.RegisterUser(Ram);
            facebookMediator.RegisterUser(Dave);
            facebookMediator.RegisterUser(Smith);
            facebookMediator.RegisterUser(Rajesh);
            facebookMediator.RegisterUser(Sam);
            facebookMediator.RegisterUser(Pam);
            facebookMediator.RegisterUser(Anurag);
            facebookMediator.RegisterUser(John);
            Dave.Send("dotnettutorials.net - this website is very good to learn Design Pattern");
            Console.WriteLine();
            Rajesh.Send("What is Design Patterns? Please explain ");
            Console.Read();
            #endregion

            #region Mediator 2
            var room = new ChatRoom();

            var john1 = new BehaviouralPatterns.Mediator.Person("John");
            var jane1 = new BehaviouralPatterns.Mediator.Person("Jane");

            room.Join(john1);
            room.Join(jane1);

            john1.Say("hi room");
            jane1.Say("oh, hey john");

            var simon = new BehaviouralPatterns.Mediator.Person("Simon");
            room.Join(simon);
            simon.Say("hi everyone!");

            jane1.PrivateMessage("Simon", "glad you could join us!");

            Console.Read();
            #endregion

            #endregion

            #region Momento
            /*if you want to perform some kind of undo or rollback operation in your application then you need to 
             * use the Memento Design Pattern*/

            #region Momento
            Originator originator = new Originator();
            originator.ledTV = new BehaviouralPatterns.Momento.LEDTV("42 inch", "60000Rs", false);

            Caretaker caretaker = new Caretaker();
            caretaker.AddMemento(originator.CreateMemento());
            originator.ledTV = new BehaviouralPatterns.Momento.LEDTV("46 inch", "80000Rs", true);
            caretaker.AddMemento(originator.CreateMemento());
            originator.ledTV = new BehaviouralPatterns.Momento.LEDTV("50 inch", "100000Rs", true);

            Console.WriteLine("\nOrignator current state : " + originator.GetDetails());
            Console.WriteLine("\nOriginator restoring to 42 inch LED TV");
            originator.ledTV = caretaker.GetMemento(0).ledTV;
            Console.WriteLine("\nOrignator current state : " + originator.GetDetails());
            Console.ReadKey();
            #endregion

            #region Momento 2
            var ba1 = new BehaviouralPatterns.Momento2.BankAccount(100);
            ba1.Deposit(50);
            ba1.Deposit(25);
            WriteLine(ba1);

            ba1.Undo();
            WriteLine($"Undo 1: {ba1}");
            ba1.Undo();
            WriteLine($"Undo 2: {ba1}");
            ba1.Redo();
            WriteLine($"Redo 2: {ba1}");
            #endregion

            #endregion

            #region Observer
            /*“Define a one-to-many dependency between objects so that when one object changes state, 
             * all its dependents are notified and updated automatically”
             * Subject. They are the publishers. When a change occurs to a subject it should notify all of its subscribers.
             * Observers. They are the subscribers. They simply listen to the changes in the subjects.*/

            #region Observer
            //Create a Product with Out Of Stock Status
            Subject RedMI = new Subject("Red MI Mobile", 10000, "Out Of Stock");
            //User Anurag will be created and user1 object will be registered to the subject
            Observer user1 = new Observer("Anurag", RedMI);
            //User Pranaya will be created and user1 object will be registered to the subject
            Observer user2 = new Observer("Pranaya", RedMI);
            //User Priyanka will be created and user3 object will be registered to the subject
            Observer user3 = new Observer("Priyanka", RedMI);

            Console.WriteLine("Red MI Mobile current state : " + RedMI.getAvailability());
            Console.WriteLine();
            // Now product is available
            RedMI.setAvailability("Available");
            Console.Read();
            #endregion

            #region Event
            var operson = new BehaviouralPatterns.Observer.Person();
            operson.FallsIll += CallDoctor;
            operson.CatchACold();
            #endregion

            #region WeekEvent
            var btn = new Button();
            var window = new Window2(btn);
            //var window = new Window(btn);
            var windowRef = new WeakReference(window);
            btn.Fire();

            WriteLine("Setting window to null");
            window = null;

            FireGC();
            WriteLine($"Is window alive after GC? {windowRef.IsAlive}");

            btn.Fire();

            WriteLine("Setting button to null");
            btn = null;

            FireGC();
            #endregion

            #region Interface
            new Demo();
            #endregion

            #region Collection
            Market market = new Market();
            //      market.PriceAdded += (sender, eventArgs) =>
            //      {
            //        Console.WriteLine($"Added price {eventArgs.Price}");
            //      };
            //      market.AddPrice(123);
            market.Prices.ListChanged += (sender, eventArgs) => // Subscribe
            {
                if (eventArgs.ListChangedType == ListChangedType.ItemAdded)
                {
                    Console.WriteLine($"Added price {((BindingList<float>)sender)[eventArgs.NewIndex]}");
                }
            };
            market.AddPrice(123);
            // 1) How do we know when a new item becomes available?

            // 2) how do we know when the market is done supplying items?
            // maybe you are trading a futures contract that expired and there will be no more prices

            // 3) What happens if the market feed is broken?
            #endregion

            #region Bidirection
            var product = new BehaviouralPatterns.BObserver.Product { Name = "Book" };
            var window1 = new BehaviouralPatterns.BObserver.Window(product);

            // want to ensure that when product name changes
            // in one component, it also changes in another

            // product.PropertyChanged += (sender, eventArgs) =>
            // {
            //   if (eventArgs.PropertyName == "Name")
            //   {
            //     Console.WriteLine("Name changed in Product");
            //     window.ProductName = product.Name;
            //   }
            // };
            //
            // window.PropertyChanged += (sender, eventArgs) =>
            // {
            //   if (eventArgs.PropertyName == "ProductName")
            //   {
            //     Console.WriteLine("Name changed in Window");
            //     product.Name = window.ProductName;
            //   }
            // };

            var binding = new BidirectionalBinding(
                product,
                (() => product.Name),
                window1,
                (() => window1.ProductName));

            // there is no infinite loop because of
            // self-assignment guard
            product.Name = "Table";
            window1.ProductName = "Chair";

            Console.WriteLine(window1);
            Console.WriteLine(window);
            #endregion

            #endregion

            #region State
            /*allows an object to alter its behavior when it’s internal state changes.
             * allows an object to completely change its behavior depending upon its current internal stat*/

            #region State
            // Initially ATM Machine in DebitCardNotInsertedState
            ATMMachine atmMachine = new ATMMachine();
            Console.WriteLine("ATM Machine Current state : "
                            + atmMachine.atmMachineState.GetType().Name);
            Console.WriteLine();
            atmMachine.EnterPin();
            atmMachine.WithdrawMoney();
            atmMachine.EjectDebitCard();
            atmMachine.InsertDebitCard();
            Console.WriteLine();
            // Debit Card has been inserted so internal state of ATM Machine
            // has been changed to DebitCardInsertedState
            Console.WriteLine("ATM Machine Current state : "
                            + atmMachine.atmMachineState.GetType().Name);
            Console.WriteLine();
            atmMachine.EnterPin();
            atmMachine.WithdrawMoney();
            atmMachine.InsertDebitCard();
            atmMachine.EjectDebitCard();
            Console.WriteLine("");
            // Debit Card has been ejected so internal state of ATM Machine
            // has been changed to DebitCardNotInsertedState
            Console.WriteLine("ATM Machine Current state : "
                            + atmMachine.atmMachineState.GetType().Name);
            Console.Read();
            #endregion

            #region
            // Initially Vending Machine will be 'noMoneyState'
            VendingMachine vendingMachine = new VendingMachine();
            Console.WriteLine("Current VendingMachine State : "
                            + vendingMachine.vendingMachineState.GetType().Name + "\n");
            vendingMachine.DispenseProduct();
            vendingMachine.SelectProductAndInsertMoney(50, "Pepsi");
            // Money has been inserted so vending Machine internal state
            // changed to 'hasMoneyState'
            Console.WriteLine("\nCurrent VendingMachine State : "
                            + vendingMachine.vendingMachineState.GetType().Name + "\n");
            vendingMachine.SelectProductAndInsertMoney(50, "Fanta");
            vendingMachine.DispenseProduct();
            // Product has been dispensed so vending Machine internal state
            // changed to 'NoMoneyState'
            Console.WriteLine("\nCurrent VendingMachine State : "
                            + vendingMachine.vendingMachineState.GetType().Name);
            Console.Read();
            #endregion

            #region Switch
            SwitchBase switchBase = new SwitchBase();
            switchBase.SwithchBaseState();
            #endregion

            #region StateLess
            StateMachine<Health, Activity>  stateMachine = new StateMachine<Health, Activity>(Health.NonReproductive);
            stateMachine.Configure(Health.NonReproductive)
              .Permit(Activity.ReachPuberty, Health.Reproductive);
            stateMachine.Configure(Health.Reproductive)
              .Permit(Activity.Historectomy, Health.NonReproductive)
              .PermitIf(Activity.HaveUnprotectedSex, Health.Pregnant,() => ParentsNotWatching);
            stateMachine.Configure(Health.Pregnant)
              .Permit(Activity.GiveBirth, Health.Reproductive)
              .Permit(Activity.HaveAbortion, Health.Reproductive);
            #endregion

            #endregion

            #region Stratergy
            /* to change the behavior of an object.*/

            #region Stratergy
            Console.WriteLine("Please enter Travel Type : Auto or Bus or Train or Taxi");
            string travelType = Console.ReadLine();
            Console.WriteLine("Travel type is : " + travelType);
            TravelContext ctx = null;
            ctx = new TravelContext();
            if ("Bus".Equals(travelType, StringComparison.InvariantCultureIgnoreCase))
            {
                ctx.SetTravelStrategy(new BusTravelStrategy());
            }
            else if ("Train".Equals(travelType, StringComparison.InvariantCultureIgnoreCase))
            {
                ctx.SetTravelStrategy(new TrainTravelStrategy());
            }
            else if ("Taxi".Equals(travelType, StringComparison.InvariantCultureIgnoreCase))
            {
                ctx.SetTravelStrategy(new TaxiTravelStrategy());
            }
            else if ("Auto".Equals(travelType, StringComparison.InvariantCultureIgnoreCase))
            {
                ctx.SetTravelStrategy(new AutoTravelStrategy());
            }
            ctx.gotoAirport();

            Console.Read();
            #endregion

            #region Stratergy 2
            Console.WriteLine("Please Select Payment Type : CreditCard or DebitCard or Cash");
            string PaymentType = Console.ReadLine();
            Console.WriteLine("Payment type is : " + PaymentType);
            Console.WriteLine("\nPlease enter Amount to Pay : ");
            double Amount = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Amount is : " + Amount);
            PaymentContext context1 = new PaymentContext();

            if ("CreditCard".Equals(PaymentType, StringComparison.InvariantCultureIgnoreCase))
            {
                context1.SetPaymentStrategy(new CreditCardPaymentStrategy());
            }
            else if ("DebitCard".Equals(PaymentType, StringComparison.InvariantCultureIgnoreCase))
            {
                context1.SetPaymentStrategy(new DebitCardPaymentStrategy());
            }
            else if ("Cash".Equals(PaymentType, StringComparison.InvariantCultureIgnoreCase))
            {
                context1.SetPaymentStrategy(new CashPaymentStrategy());
            }
            context1.Pay(Amount);
            Console.ReadKey();
            #endregion

            #region Dymanic
            var tp = new TextProcessor();
            tp.SetOutputFormat(OutputFormat.Markdown);
            tp.AppendList(new[] { "foo", "bar", "baz" });
            WriteLine(tp);

            tp.Clear();
            tp.SetOutputFormat(OutputFormat.Html);
            tp.AppendList(new[] { "foo", "bar", "baz" });
            WriteLine(tp);
            #endregion

            #region Static
            var tp3 = new TextProcessorStatic<MarkdownListStrategy>();
            tp.AppendList(new[] { "foo", "bar", "baz" });
            WriteLine(tp);

            var tp4 = new TextProcessorStatic<HtmlListStrategy>();
            tp4.AppendList(new[] { "foo", "bar", "baz" });
            WriteLine(tp4);
            #endregion

            #endregion

            #region Template
            /*sequence of steps of an algorithm and allows the subclasses to override the steps 
             * but not allowed to change the sequence*/

            #region Template
            Console.WriteLine("Build a Concrete House\n");
            HouseTemplate houseTemplate = new ConcreteHouse();
            // call the template method
            houseTemplate.BuildHouse();
            Console.WriteLine();
            Console.WriteLine("Build a Wooden House\n");
            houseTemplate = new WoodenHouse();
            // call the template method
            houseTemplate.BuildHouse();
            Console.Read();
            #endregion

            #region Template 2
            Console.WriteLine("Nescafe Coffee Preparation\n");
            CoffeeTemplate coffee = new NescafeCoffee();
            coffee.PrepareCoffee();
            Console.WriteLine();
            Console.WriteLine("Bru coffee preparation\n");
            coffee = new BruCoffee();
            coffee.PrepareCoffee();
            Console.Read();
            #endregion

            #region Function Template

           Program prg = new Program();

           GameTemplate.Run(prg.Start, prg.TakeTurn, prg.HaveWinner, prg.WinningPlayer);
            #endregion

            #endregion

            #region Visitor
            /*which changes the executing algorithm of an element object. 
             * In this way, when the visitor varies, the execution algorithm of the element object can also 
             * vary. As per the Visitor Design Pattern, the element object has to accept the visitor object 
             * so that the visitor object handles the operation on the element object.

             * The Visitor Design Pattern should be used when you have distinct and unrelated operations 
             * to perform across a structure of objects (element objects). 
             * This avoids adding in code throughout your object structure that is better kept separate.
             */

            #region Visitor
            //Object Structure : School
            //element : Kid
            //Visitor : Doctor
            School school = new School();
            var visitor1 = new Doctor("James");
            school.PerformOperation(visitor1);
            Console.WriteLine();
            var visitor2 = new Salesman("John");
            school.PerformOperation(visitor2);
            #endregion

            #endregion

            #endregion
        }

        #region Singleton Parallel Programming, Lazy, Eager
        private static void PrintTeacherDetails()
        {
            Singleton fromTeacher = Singleton.GetInstance;
            fromTeacher.PrintDetails("From Teacher");
        }
        private static void PrintStudentdetails()
        {
            Singleton fromStudent = Singleton.GetInstance;
            fromStudent.PrintDetails("From Student");
        }
        private static void PrintTeacherDetailsEager()
        {
            Singleton fromTeacher = Singleton.GetInstanceEager;
            fromTeacher.PrintDetails("From Teacher");
        }
        private static void PrintStudentdetailsEager()
        {
            Singleton fromStudent = Singleton.GetInstanceEager;
            fromStudent.PrintDetails("From Student");
        }
        private static void PrintTeacherDetailsLazy()
        {
            Singleton fromTeacher = Singleton.GetInstanceLazy;
            fromTeacher.PrintDetails("From Teacher");
        }
        private static void PrintStudentdetailsLazy()
        {
            Singleton fromStudent = Singleton.GetInstanceLazy;
            fromStudent.PrintDetails("From Student");
        }
        #endregion

        #region Adapter NoCache
        private static readonly List<VectorObject> vectorObjects = new List<VectorObject>
        {
          new VectorRectangle(1, 1, 10, 10),
          new VectorRectangle(3, 3, 6, 6)
            };

        // the interface we have
        public static void DrawPoint(StructuralPatterns.Adapter.Point p)
        {
            Write(".");
        }
        private static void Draw()
        {
            foreach (var vo in vectorObjects)
            {
                foreach (var line in vo)
                {
                    var adapter = new LineToPointAdapter(line);
                    adapter.ForEach(DrawPoint);
                }
            }
        }
        #endregion

        #region Flyweight
        public static void ForceGC()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        public static string RandomString()
        {
            Random rand = new Random();
            return new string(
              Enumerable.Range(0, 10).Select(i => (char)('a' + rand.Next(26))).ToArray());
        }
        #endregion

        #region Observer
        private static void CallDoctor(object sender, FallsIllEventArgs eventArgs)
        {
            Console.WriteLine($"A doctor has been called to {eventArgs.Address}");
        }
        private static void FireGC()
        {
            WriteLine("Starting GC");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            WriteLine("GC is done!");
        }
        public class Demo : IObserver<Event>
        {
            public Demo()
            {
                var operson1 = new oPerson();
                var sub = operson1.Subscribe(this);
                operson1.CatchACold();

                operson1.OfType<FallsIllEvent>()
                  .Subscribe(args => WriteLine($"A doctor has been called to {args.Address}"));

                operson1.CatchACold();
            }

            public void OnNext(Event value)
            {
                if (value is FallsIllEvent args)
                    WriteLine($"A doctor has been called to {args.Address}");
            }

            public void OnError(Exception error) { }
            public void OnCompleted() { }
        }
        #endregion

        #region State
        public static bool ParentsNotWatching
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        #endregion

        #region Template
        int numberOfPlayers = 2;
        int currentPlayer = 0;
        int turn = 1, maxTurns = 10;

        void Start()
        {
            WriteLine($"Starting a game of chess with {numberOfPlayers} players.");
        }

        bool HaveWinner()
        {
            return turn == maxTurns;
        }

        void TakeTurn()
        {
            WriteLine($"Turn {turn++} taken by player {currentPlayer}.");
            currentPlayer = (currentPlayer + 1) % numberOfPlayers;
        }

        int WinningPlayer()
        {
            return currentPlayer;
        }
        #endregion

    }
}
