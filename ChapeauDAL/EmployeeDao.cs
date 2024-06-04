using ChapeauModel;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauDAL
{
    public class EmployeeDao : BaseDao
    public class EmployeeDao:BaseDao
    {
        public List<Employee> GetAllEmployees()
        public void CreateEmployee(Employee employee)
        {
            string query = "SELECT EmployeeId, UserCode, Role, Status, LastName, FirstName FROM EMPLOYEE";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadAllEmployees(ExecuteSelectQuery(query, sqlParameters));
        }

        private Employee ReadEmployee(DataRow row)
            string query = "INSERT INTO Employee (EmployeeId, Username, Password, Status) VALUES (@EmployeeId, @Username, @Password, @Status)";
            SqlParameter[] sqlParameters =
        {
            return new Employee()
            {
                EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                UserCode = row["UserCode"].ToString(),
                FirstName = row["FirstName"].ToString(),
                LastName = row["LastName"].ToString(),
                role = row["Role"].ToString(),
                status = row["Status"].ToString()
            new SqlParameter("@EmployeeId", employee.EmployeeId),
            new SqlParameter("@Username", employee.Username),
            new SqlParameter("@Password", employee.Password),
            new SqlParameter("@Status", employee.Status.ToString())  // Convert enum to string
            };
            ExecuteEditQuery(query, sqlParameters);
        }

        private List<Employee> ReadAllEmployees(DataTable dataTable)
        public void UpdateEmployee(Employee employee)
        {
            List<Employee> employees = new List<Employee>();

            foreach (DataRow dr in dataTable.Rows)
            string query = "UPDATE Employee " +
                           "SET Username=@Username, Password=@Password, Status=@Status " +
                           "WHERE EmployeeId=@EmployeeId;";
            SqlParameter[] sqlParameters =
            {
                employees.Add(ReadEmployee(dr));
            new SqlParameter("@EmployeeId", employee.EmployeeId),
            new SqlParameter("@Username", employee.Username),
            new SqlParameter("@Password", employee.Password),
            new SqlParameter("@Status", employee.Status.ToString())  // Convert enum to string
    };
            ExecuteEditQuery(query, sqlParameters);
            }
            return employees;
        }

        public Employee GetEmployeeByHashedUC(string userCode)
        public void DeleteEmployee(Employee employee)
        {
            string query = "SELECT EmployeeId, FirstName, LastName, Role, Status, UserCode FROM [EMPLOYEE] WHERE UserCode = @UserCode";
            SqlParameter[] sqlParameters = new SqlParameter[1]
            string query = "DELETE FROM Employee WHERE EmployeeId=@EmployeeId;";
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@UserCode", userCode)
            new SqlParameter("@EmployeeId", employee.EmployeeId)
            };
            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);

            if (dataTable.Rows.Count == 0)
            {
                return null;
            ExecuteEditQuery(query, sqlParameters);
            }
            else
            {
                return ReadEmployee(dataTable.Rows[0]);
            }
        }

        public Employee GetByEmployeeId(int employeeId)
        public Employee GetEmployeeById(int employeeId)
        {
            string query = "SELECT * FROM EMPLOYEE WHERE EmployeeId = @Id;";
            Employee employee = null;
            string query = "SELECT * FROM EMPLOYEE WHERE EmployeeId = @EmployeeId;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@Id", employeeId)
                new SqlParameter("@EmployeeId", employeeId)
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
                new SqlParameter("@Status", employee.Status)
            };
            ExecuteEditQuery(query, sqlParameters);
        }

        public void DeleteEmployee(Employee employee)
            if (dataTable.Rows.Count > 0)
        {
            string query = "DELETE FROM EMPLOYEE WHERE EmployeeId = @Id;";
            SqlParameter[] sqlParameters = new SqlParameter[]
                DataRow row = dataTable.Rows[0];
                employee = new Employee
            {
                new SqlParameter("@Id", employee.EmployeeId)
                    EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                    Username = row["Username"].ToString(),
                    Password = row["Password"].ToString(),
                    Status = (EmployeeStatus)Enum.Parse(typeof(EmployeeStatus), row["Status"].ToString())
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
                new SqlParameter("@Status", employee.status),
                new SqlParameter("@UserCode", hashedPassword),
                new SqlParameter("@Role", employee.role),
                new SqlParameter("@FirstName", employee.FirstName),
                new SqlParameter("@LastName", employee.LastName)
            };
            ExecuteEditQuery(query, sqlParameters);
            return employee;
        }
    }
}
