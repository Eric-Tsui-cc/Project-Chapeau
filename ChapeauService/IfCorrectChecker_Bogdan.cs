using ChapeauDAL;
using System.Security.Cryptography;
using System.Text;
using ChapeauModel;

namespace ChapeauService
{
    public class IfCorrectChecker_Bogdan
    {
        private EmployeeDao employeeDb;

        public IfCorrectChecker_Bogdan()
        {
            employeeDb = new EmployeeDao();
        }

        public bool IsCorrectUserCode(string inputUserCode)
        {
            // Hash the input user code using SHA256
            string hashedInputUserCode = HashUserCode(inputUserCode);

            // Get the hashed user code from the database
            Employee employee = employeeDb.GetEmployeeByHashedUC(hashedInputUserCode);

            if (employee == null)
            {
                return false;
            }

            // Retrieve the stored hashed user code
            string storedHashedUserCode = employee.UserCode;

            // Verify if the hashed input user code matches the stored hashed user code
            bool passwordMatch = hashedInputUserCode == storedHashedUserCode;

            return passwordMatch;
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


        public void AddNewEmployee(Employee employee, string rawPassword)
        {
            employeeDb.AddEmployee(employee, rawPassword);
        }
        public Employee GetEmployeeByHashedUC(string inputUserCode)
        {
            string hashedInputUserCode = HashUserCode(inputUserCode);

            // Get the hashed user code from the database
            Employee employee = employeeDb.GetEmployeeByHashedUC(hashedInputUserCode);

            return employee;
        }
    }
}