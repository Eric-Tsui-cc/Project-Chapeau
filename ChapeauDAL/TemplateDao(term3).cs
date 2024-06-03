using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using ChapeauDAL;
using Models;

namespace ChapeauDAL
{                                                        //Just for references, delete this when the project finish.
    public class StudentDao : BaseDao
    {
        public List<Student> GetAllStudents()
        {

            string query = "SELECT Studentnumber, FirstName, LastName,Telephonenumber,RoomNumber,Class FROM STUDENT";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Student> ReadTables(DataTable dataTable)
        {
            List<Student> students = new List<Student>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Student student = new Student()
                {
                    Number = (int)dr["Studentnumber"],
                    FirstName = dr["FirstName"].ToString(),
                    LastName = dr["LastName"].ToString(),
                    TelePhoneNumber = dr["TelePhoneNumber"].ToString(),
                    RoomNumber = dr["RoomNumber"].ToString(),
                    Class = dr["Class"].ToString(),
                };
                students.Add(student);
            }
            return students;
        }
        public void AddStudent(Student student)
        {
            string query = "INSERT INTO Student (StudentNumber, FirstName, LastName, TelephoneNumber, Class, RoomNumber) VALUES (@StudentNumber, @FirstName, @LastName, @TelephoneNumber, @Class, @RoomNumber)";
            SqlParameter[] sqlParameters =
            {
            new SqlParameter("@StudentNumber", student.Number),
            new SqlParameter("@FirstName", student.FirstName),
            new SqlParameter("@LastName", student.LastName),
            new SqlParameter("@TelephoneNumber", student.TelePhoneNumber),
            new SqlParameter("@Class", student.Class),
            new SqlParameter("@RoomNumber", student.RoomNumber)
        };
            ExecuteEditQuery(query, sqlParameters);
        }

        public void UpdateStudent(Student student)
        {
            string query = "UPDATE Student " +
                           "SET StudentNumber=@StudentNumber,FirstName=@FirstName, LastName=@LastName, TelephoneNumber=@TelephoneNumber, Class=@Class, RoomNumber=@RoomNumber " +
                           "WHERE StudentNumber=@StudentNumber;";
            SqlParameter[] sqlParameters =
            {
            new SqlParameter("@StudentNumber", student.Number),
            new SqlParameter("@FirstName", student.FirstName),
            new SqlParameter("@LastName", student.LastName),
            new SqlParameter("@TelephoneNumber", student.TelePhoneNumber),
            new SqlParameter("@Class", student.Class),
            new SqlParameter("@RoomNumber", student.RoomNumber)
        };
            ExecuteEditQuery(query, sqlParameters);
        }

        public void DeleteStudent(Student student)
        {
            string query = "DELETE FROM Student WHERE StudentNumber=@StudentNumber;";
            SqlParameter[] sqlParameters =
            {
            new SqlParameter("@StudentNumber", student.Number)
        };
            ExecuteEditQuery(query, sqlParameters);
        }

        public Student GetByStudentNumber(int studentNumber)
        {
            Student student = null;
            string query = "SELECT * FROM Student WHERE StudentNumber = @StudentNumber;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                 new SqlParameter("@StudentNumber", studentNumber)
            };

            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                student = new Student
                {
                    Number = Convert.ToInt32(row["StudentNumber"]),
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString(),
                    TelePhoneNumber = row["TelephoneNumber"].ToString(),
                    Class = row["Class"].ToString(),
                    RoomNumber = row["RoomNumber"].ToString()
                };
            }

            return student;
        }
    }
}