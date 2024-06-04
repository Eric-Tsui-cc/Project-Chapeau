using ChapeauModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ChapeauDAL
{
    public class EmployeeDao : BaseDao

    {
        public List<Employee> GetAllEployees()
        {
            string query = "SELECT EmployeeId,UserCode, Role,Status, LastName, FirstName FROM EMPLOYEE";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadAllEmployees(ExecuteSelectQuery(query, sqlParameters));
        }
        public Employee ReadEmployee(DataTable dataTable)
        {
            DataRow row = dataTable.Rows[0];
            Employee employee = new Employee()
            {
                EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                UserCode = row["UserCode"].ToString(),
                FirstName = row["FirstName"].ToString(),
                LastName = row["LastName"].ToString(),
                role = row["Role"].ToString(),
                status = row["Status"].ToString(),

            };
            return employee;
        }

        private List<Employee> ReadAllEmployees(DataTable dataTable)
        {
            List<Employee> employees = new List<Employee>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Employee employee = new Employee();

                employees.Add(ReadEmployee(dataTable));
            }
            return employees;
        }

        public Employee GetEmployeeByHashedUC(string UserCode)
        {
            Employee employee = new Employee();

            string query = "SELECT EmployeeId, FirstName, LastName, Role, Status, UserCode, FROM [EMPLOYEE] WHERE UserCode = @UserCode";
            // prevents from SQL injection
            SqlParameter[] sqlParameters = new SqlParameter[1]
            {
                new SqlParameter("@UserCode", UserCode)
            };
            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);
            // if no records found based on the username - user with given username doesn't exist
            if (dataTable.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return ReadEmployee(dataTable);
            }
        }

        public Employee GetByEmployeeId(int EmployeeId)
        {
            Employee employee = null;
            string query = "SELECT * FROM TABLE WHERE OrderId = @Id;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
        new SqlParameter("@Id", employee.EmployeeId)
            };

            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);
            // if no records found based on the username - user with given username doesn't exist
            if (dataTable.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return ReadEmployee(dataTable);
            }

        }

        public void ChangeEmployeeStatus(Employee employee)
        {
            string query = "UPDATE EMPLOYEE " +
                "SET Status = @Status" +
                "WHERE EmployeeId=@id ";
            SqlParameter[] sqlParameters =
            {

        new SqlParameter("@id", employee.EmployeeId),
        new SqlParameter("@Status", employee.Status),

        };
            ExecuteEditQuery(query, sqlParameters);
        }

        public void DeleteEmployee(Employee employee)
        {
            string query = "DELETE FROM EMPLOYEE WHERE EmployeeId=@id;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@id", employee.EmployeeId)
            };
            ExecuteEditQuery(query, sqlParameters);
        }
        public void AddEmployee(Employee employee)
        {
            string query = "INSERT INTO  EMPLOYEE (EmployeeId, Status, UserCode, Role, FirstName, LastName)" +
                " VALUES (@EmployeeId, @Status, @UserCode, @Role, @FirstName, @LastName);";
            SqlParameter[] sqlParameters =
            {
            new SqlParameter("@EmployeeId", employee.EmployeeId),
            new SqlParameter("@Status", employee.Status),
            new SqlParameter("@Role", employee.Role),
            new SqlParameter("@UserCode", employee.UserCode),
            new SqlParameter("@FirstName", employee.FirstName),
            new SqlParameter("@LastName", employee.LastName)

        };
            ExecuteEditQuery(query, sqlParameters);
        }
    }
}

