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
    public enum EmployeeRole
    {
        Waiter, Bartender, Chef, Manager, Undefined
    }
    public class Employee
    {
        public string role = "";

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserCode { get; set; }
        public string status { get; set; }

        public EmployeeStatus Status
        {
            get
            {
                if (status == "Active")
                {
                    return EmployeeStatus.Active;
                }
                else
                {
                    return EmployeeStatus.Inactive;
                }

            }
        }

        public EmployeeRole Role
        {
            get
            {
                if (role == "Waiter")
                {
                    return EmployeeRole.Waiter;
                }
                else if (role == "Chef")
                {
                    return EmployeeRole.Chef;
                }
                else if (role == "Bartender")
                {
                    return EmployeeRole.Bartender;
                }
                else if (role == "Manager")
                {
                    return EmployeeRole.Manager;
                }
                else
                {
                    return EmployeeRole.Undefined;
                }
            }
        }
    }
}

