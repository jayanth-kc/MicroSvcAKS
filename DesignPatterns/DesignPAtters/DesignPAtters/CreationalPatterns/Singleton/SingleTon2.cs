using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.CreationalPatterns.Singleton
{
    public sealed class Singleton
    {
        private static int counter = 0;
        private static Singleton instance = null;
        private static readonly object Instancelock = new object();
        public static Singleton GetInstance
        {
            get
            {
                if (instance == null)
                    instance = new Singleton();
                return instance;
            }
        }

        //ThreadStaticAttribute safe locking
        public static Singleton GetInstanceLock
        {
            get
            {
                lock (Instancelock)
                {
                    if (instance == null)
                    {
                        instance = new Singleton();
                    }
                    return instance;
                }
            }
        }
        //Double Check to ver come the slowness in thread lcoking
        public static Singleton GetInstanceDoubleCheck
        {
            get
            {
                if (instance == null)
                {
                    lock (Instancelock)
                    {
                        if (instance == null)
                        {
                            instance = new Singleton();
                        }
                    }
                }
                return instance;
            }
        }

        //Eager or Non Lazy loading
        private static readonly Singleton singleInstance = new Singleton();

        public static Singleton GetInstanceEager
        {
            get
            {
                return singleInstance;
            }
        }
        //Lazy Loading
        private static readonly Lazy<Singleton> InstancelockLazy =
                   new Lazy<Singleton>(() => new Singleton());
        public static Singleton GetInstanceLazy
        {
            get
            {
                return InstancelockLazy.Value;
            }
        }
        private Singleton()
        {
            counter++;
            Console.WriteLine("Counter Value " + counter.ToString());
        }
        public void PrintDetails(string message)
        {
            Console.WriteLine(message);
        }

    }

    public class SingletonInheritance
    {
        private static int counter = 0;
        private static SingletonInheritance instance = null;
        public static SingletonInheritance GetInstance
        {
            get
            {
                if (instance == null)
                    instance = new SingletonInheritance();
                return instance;
            }
        }

        private SingletonInheritance()
        {
            counter++;
            Console.WriteLine("Counter Value " + counter.ToString());
        }
        public void PrintDetails(string message)
        {
            Console.WriteLine(message);
        }

        public class DerivedSingleton : SingletonInheritance
        {
        }
    }
}
