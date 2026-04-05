using System;

namespace ChapeauModel
{
    public enum EmployeeRole
    {
        Waiter,
        Chef,
        Bartender
    }

    public enum EmployeeStatus
    {
        Active,
        Inactive
    }

    public class Employee
    {
        public int EmployeeId { get; set; }
        public string UserCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EmployeeRole Role { get; set; }
        public EmployeeStatus Status { get; set; }

        public Employee() { }

        public Employee(int employeeId, string userCode, string firstName, string lastName, EmployeeRole role, EmployeeStatus status)
        {
            EmployeeId = employeeId;
            UserCode = userCode;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
            Status = status;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
