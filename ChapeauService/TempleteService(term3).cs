using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using ChapeauDAL;
using Models;

namespace SomerenService
{                                                                          //for reference only, delete this when the project finish.
    public class StudentService
    {
        private StudentDao studentdb;

        public StudentService()
        {
            studentdb = new StudentDao();
        }

        public List<Student> GetStudents()
        {
            List<Student> students = studentdb.GetAllStudents();
            return students;
        }
        public void AddStudent(Student student)
        {
            studentdb.AddStudent(student);
        }
        public void UpdateStudent(Student student)
        {
            studentdb.UpdateStudent(student);

        }
        public void DeleteStudent(Student student)
        {
            studentdb.DeleteStudent(student);

        }
        public Student GetByStudentNumber(int studentNumber)
        {
            Student student = studentdb.GetByStudentNumber(studentNumber);
            return student;
        }

    }
}
