using System;

namespace Christ3D.Domain.Models
{
    public class Student
    {
        protected Student() { }
        public Student(Guid id, string name, string email, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }


        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public string Email { get; private set; }
        public string Phone { get; private set; }

        public DateTime BirthDate { get; private set; }
    }
}