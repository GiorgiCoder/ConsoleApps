using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepProjects.StudentManager
{
    public class Student // student class with fields, constructor and toString
    {
        public string Name { get; set; }
        public int RollNumber { get; set; }
        public char Grade { get; set; }

        public Student(string name, int rollNumber, char grade)
        {
            Name = name;
            RollNumber = rollNumber;
            Grade = grade;
        }

        public override string ToString()
        {
            return $"Id: {RollNumber}\nName: {Name}\nGrade: {Grade}\n";
        }
    }
}
