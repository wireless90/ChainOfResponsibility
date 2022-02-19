using ChainOfResponsibility.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace ChainOfResponsibility.Core.Main
{
    public class Employee
    {
        public string Name { get; set; }

        public string Position { get; set; }

        public int Salary { get; set; }

    }

    public class ManagerSalaryChanger : AbstractChain<Employee>
    {
        public override bool IsChainResponsible(Employee request)
            => request.Position == "Manager";

        public override void RequestHandler(Employee request)
            => request.Salary += 200;
    }

    public class EngineerSalaryChanger : AbstractChain<Employee>
    {
        public override bool IsChainResponsible(Employee request)
            => request.Position.Contains("Engineer");

        public override void RequestHandler(Employee request)
            => request.Salary += 500;
    }


    internal class Program
    {
        private static void Main(string[] args)
        {
            var employees = new List<Employee>() {
                new Employee(){ Name = "Razali", Position = "Software Engineer", Salary = 8000},
                new Employee(){ Name = "Damian", Position = "UX Engineer", Salary = 6500},
                new Employee(){ Name = "Zana", Position = "Manager", Salary = 13000},
            };

            IChainGroup<Employee> salaryChainGroup = new ChainGroup<Employee>()
                                                            .AddChainGroupLink(new EngineerSalaryChanger())
                                                            .AddChainGroupLink(new ManagerSalaryChanger());

            employees.ForEach(employee => salaryChainGroup.Process(employee));

            employees.ForEach(employee => Console.WriteLine($"Position: {employee.Position}, Salary: {employee.Salary}"));
        }
    }
}
