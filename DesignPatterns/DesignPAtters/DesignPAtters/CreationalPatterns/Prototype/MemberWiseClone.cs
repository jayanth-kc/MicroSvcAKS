using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.CreationalPatterns.Prototype
{
    /*
     Points to Remember:
        The MemberwiseClone method is part of the System.Object class and it creates a shallow copy of the given object. 
        MemberwiseClone Method only copies the non-static fields of the object to the new object
        In the process of copying, if a field is a value type, a bit by bit copy of the field is performed. 
            If a field is a reference type, the reference is copied but the referenced object is not.
         */
    public class Employee
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public Employee GetClone()
        {
            return (Employee)this.MemberwiseClone();
        }
    }
}
