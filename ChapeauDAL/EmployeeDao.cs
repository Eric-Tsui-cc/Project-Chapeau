using ChapeauModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauDAL
{
    public class EmployeeDao:BaseDao
    {
        public void CreateEmployee(Employee employee)
        {
            string query = "INSERT INTO Employee (EmployeeId, Username, Password, Status) VALUES (@EmployeeId, @Username, @Password, @Status)";
            SqlParameter[] sqlParameters =
            {
            new SqlParameter("@EmployeeId", employee.EmployeeId),
            new SqlParameter("@Username", employee.Username),
            new SqlParameter("@Password", employee.Password),
            new SqlParameter("@Status", employee.Status.ToString())  // Convert enum to string
    };
            ExecuteEditQuery(query, sqlParameters);
        }
        public void UpdateEmployee(Employee employee)
        {
            string query = "UPDATE Employee " +
                           "SET Username=@Username, Password=@Password, Status=@Status " +
                           "WHERE EmployeeId=@EmployeeId;";
            SqlParameter[] sqlParameters =
            {
            new SqlParameter("@EmployeeId", employee.EmployeeId),
            new SqlParameter("@Username", employee.Username),
            new SqlParameter("@Password", employee.Password),
            new SqlParameter("@Status", employee.Status.ToString())  // Convert enum to string
    };
            ExecuteEditQuery(query, sqlParameters);
        }

        public void DeleteEmployee(Employee employee)
        {
            string query = "DELETE FROM Employee WHERE EmployeeId=@EmployeeId;";
            SqlParameter[] sqlParameters =
            {
            new SqlParameter("@EmployeeId", employee.EmployeeId)
    };
            ExecuteEditQuery(query, sqlParameters);
        }

        public Employee GetEmployeeById(int employeeId)
        {
            Employee employee = null;
            string query = "SELECT * FROM Employees WHERE EmployeeId = @EmployeeId;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@EmployeeId", employeeId)
            };

            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                employee = new Employee
                {
                    EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                    Username = row["Username"].ToString(),
                    Password = row["Password"].ToString(),
                    Status = (EmployeeStatus)Enum.Parse(typeof(EmployeeStatus), row["Status"].ToString())
                };
            }

            return employee;
        }
    }
}
