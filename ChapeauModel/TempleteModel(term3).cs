using System;

namespace ChapeauModel
{                                                                      //for reference only, delete this when the project finish.
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Number { get; set; }
        public string RoomNumber { get; set; }
        public string TelePhoneNumber { get; set; }
        public string Class { get; set; }
        public Student() { }
        public Student(int number, string firstName, string lastName, string telePhoneNumber, string @class, string roomNumber)
        {
            Number = number;
            FirstName = firstName;
            LastName = lastName;
            RoomNumber = roomNumber;
            TelePhoneNumber = telePhoneNumber;
            Class = @class;
        }

        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        public override string ToString()
        {
            return $"{FullName}";
        }
    }
}
