using ChapeauModel;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace ChapeauDAL
{
    public class EmployeeDao : BaseDao
    {
        public List<Employee> GetAllEmployees()
        {
            string query = "SELECT EmployeeId, UserCode, Role, Status, LastName, FirstName FROM EMPLOYEE";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadAllEmployees(ExecuteSelectQuery(query, sqlParameters));
        }
        public List<Employee> GetAllActiveEmployees()
        {
            string query = "SELECT * FROM [EMPLOYEE] WHERE Status = 'Active'";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadAllEmployees(ExecuteSelectQuery(query, sqlParameters));
        }

        private Employee ReadEmployee(DataRow row)
        {
            string roleString = row["Role"].ToString();
            EmployeeRole role = Enum.TryParse(roleString, out EmployeeRole parsedRole) ? parsedRole : EmployeeRole.Waiter;

            string statusString = row["Status"].ToString();
            EmployeeStatus status = Enum.TryParse(statusString, out EmployeeStatus parsedStatus) ? parsedStatus : EmployeeStatus.Active;

            return new Employee()
            {
                EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                UserCode = row["UserCode"].ToString(),
                FirstName = row["FirstName"].ToString(),
                LastName = row["LastName"].ToString(),
                Role = role,
                Status = status
            };
        }

        private List<Employee> ReadAllEmployees(DataTable dataTable)
        {
            List<Employee> employees = new List<Employee>();

            foreach (DataRow dr in dataTable.Rows)
            {
                employees.Add(ReadEmployee(dr));
            }
            return employees;
        }

        public Employee GetEmployeeByHashedUC(string userCode)
        {
            string query = "SELECT EmployeeId, FirstName, LastName, Role, Status, UserCode FROM [EMPLOYEE] WHERE UserCode = @UserCode";
            SqlParameter[] sqlParameters = new SqlParameter[1]
            {
        new SqlParameter("@UserCode", userCode)
            };
            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);

            if (dataTable.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return ReadEmployee(dataTable.Rows[0]);
            }
        }

        public Employee GetByEmployeeId(int employeeId)
        {
            string query = "SELECT * FROM EMPLOYEE WHERE EmployeeId = @Id;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@Id", employeeId)
            };
            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);

            if (dataTable.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return ReadEmployee(dataTable.Rows[0]);
            }
        }

        public void ChangeEmployeeStatus(Employee employee)
        {
            string query = "UPDATE EMPLOYEE SET Status = @Status WHERE EmployeeId = @Id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@Id", employee.EmployeeId),
                new SqlParameter("@Status", employee.Status.ToString())
            };
            ExecuteEditQuery(query, sqlParameters);
        }

        public void DeleteEmployee(Employee employee)
        {
            string query = "DELETE FROM EMPLOYEE WHERE EmployeeId = @Id;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@Id", employee.EmployeeId)
            };
            ExecuteEditQuery(query, sqlParameters);
        }
        private string HashUserCode(string userCode)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(userCode));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public void AddEmployee(Employee employee, string rawUserCode)
        {
            string hashedPassword = HashUserCode(rawUserCode);

            string query = "INSERT INTO EMPLOYEE ( Status, UserCode, Role, FirstName, LastName) VALUES ( @Status, @UserCode, @Role, @FirstName, @LastName);";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@Status", employee.Status.ToString()),
                new SqlParameter("@UserCode", hashedPassword),
                new SqlParameter("@Role", employee.Role.ToString()),
                new SqlParameter("@FirstName", employee.FirstName),
                new SqlParameter("@LastName", employee.LastName)
            };
            ExecuteEditQuery(query, sqlParameters);
        }
    }
}