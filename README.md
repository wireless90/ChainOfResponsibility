# ChainOfResponsibility
Get easily started using the ChainOfResponsibility Design Pattern


# Let's define a Employee class as an Example

```cs
    public class Employee
    {
        public string Name { get; set; }

        public string Position { get; set; }

        public int Salary { get; set; }

    }

```


# Objective: Increase Salary of employees
We want to increase the salary of managers by 200 and those of engineers by 500

# Let's define our Chains

For each chain , we need to inherit from `AbstractChain<TRequest>`.

Below is an example of 2 chains. In each chain, we need to override the 2 functions, `IsChainResponsible` and `RequestHandler`.

A `RequestHandler` is only invoked when `IsChainResponsible` returns `True`.

```cs
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
```

```cs
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

```
