using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.CreationalPatterns.Factory.IOC_DI
{
    /// <summary>
    /// Inversion of Control and Dependency Injection
    /// </summary>

    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public int Salary { get; set; }
    }

    public interface IEmployeeDataAccess
    {
        Employee GetEmployeeDetails(int id);
    }

    public class EmployeeDataAccess : IEmployeeDataAccess
    {
        public Employee GetEmployeeDetails(int id)
        {
            // In real-time get the employee details from db
            //but here we are hard coded the employee details
            Employee emp = new Employee()
            {
                ID = id,
                Name = "Pranaya",
                Department = "IT",
                Salary = 10000
            };
            return emp;
        }
    }
    //Metod Dependecy injection
    interface IEmployeeDataAccessDependency
    {
        void SetDependency(IEmployeeDataAccess employeeDataAccess);
    }

    public class EmployeeBusinessLogic: IEmployeeDataAccessDependency
    {
        // EmployeeDataAccess _EmployeeDataAccess;
        //DIP
        IEmployeeDataAccess _EmployeeDataAccess;
        public EmployeeBusinessLogic()
        {
            //EmployeeBusinessLogic class depends on the EmployeeDataAccess class. 
            //It creates an instance of the EmployeeDataAccess class in order to get the Employee data
            //  _EmployeeDataAccess = new EmployeeDataAccess();

            //IOC using Factory Pattern
            _EmployeeDataAccess = DataAccessFactory.GetEmployeeDataAccessObj();
        }
        //Dependecy injection
        public IEmployeeDataAccess EmpDataAccess { get; set; }

        public Employee GetEmployeeDetails(int id)
        {
            //return _EmployeeDataAccess.GetEmployeeDetails(id);
            return EmpDataAccess.GetEmployeeDetails(id);
        }
        //Method Dependency injection
        public void SetDependency(IEmployeeDataAccess employeeDataAccess)
        {
            EmpDataAccess = employeeDataAccess;
        }
    }

    //IOC using Factory pattern by passing the control to a different class to create the objec
    public class DataAccessFactory
    {
        public static IEmployeeDataAccess GetEmployeeDataAccessObj()
        {
            return new EmployeeDataAccess();
        }
    }
}
