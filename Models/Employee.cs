using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ChapeauModel
{
    public enum EmployeeStatus
    {
        Active,
        Inactive
    }

    public class Employee
    {

        public int EmployeeId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public EmployeeStatus Status { get; set; }

        public Employee() { }

        public Employee(int employeeId, string username, string password, EmployeeStatus status)
        {
            EmployeeId = employeeId;
            Username = username;
            Password = password;
            Status = status;
        }
    }
}



