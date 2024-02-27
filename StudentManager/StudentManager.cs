using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepProjects.StudentManager
{
    public class StudentManagerClass
    {
        public static List<Student> students = new List<Student>()
        {
            new Student("Giorgi", 0, 'A'),
            new Student("John Doe", 1, 'A'),
            new Student("Markus", 2, 'C'),
            new Student("Helmut", 3, 'F')
        };

        public void Action()
        {
            while (true)
            {
                Console.WriteLine("Choose which service you want to use (enter number):\n1. Add student\n2. Show all students\n3. Search student by Id\n4. Update student's grade\n5. Exit");

                int option = 0;
                while (true) // validation for user input
                {
                    try
                    {
                        option = int.Parse(Console.ReadLine());
                        if (option == 1 || option == 2 || option == 3 || option == 4 || option == 5)
                        {
                            break;
                        }
                        else int.Parse("GoToError");
                    }
                    catch (Exception) { }
                    {
                        Console.WriteLine("Enter a valid option");
                    }
                }

                if (option == 1) // add student
                {
                    AddStudent();
                }
                else if (option == 2) // return all students
                {
                    GetAllStudents();
                }
                else if (option == 3) // search student by Id
                {
                    Console.WriteLine("Enter ID:");
                    int Id = -1;
                    while (true) // validation for Id
                    {
                        try
                        {
                            Id = int.Parse(Console.ReadLine());
                            break;
                        }
                        catch
                        {
                            Console.WriteLine("Enter a valid character for grade");
                        }
                    }
                    Console.WriteLine(GetStudentById(Id).ToString());
                }
                else if (option == 4) // update grade
                {
                    UpdateStudentGrade();
                }
                else return;
            }
        }

        private bool AddStudent()
        {
            Console.WriteLine("Enter student's name:");
            string name = Console.ReadLine();

            var stdnt = students.FirstOrDefault(s => s.Name == name); // bad checking but we don't have more info about students
            if (stdnt != null) // if student != null, it means he already exists
            {
                Console.WriteLine("Student already exists!");
                return false;
            }
            //else
            int rollNumber = students.Last().RollNumber + 1; // autoincrement Id
            Console.WriteLine("Enter student's grade:");
            char grade = 'A';
            while (true) // validation for grade
            {
                try
                {
                    grade = char.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Enter a valid character for grade");
                }
            }
            // after validation, add student
            var student = new Student(name, rollNumber, grade);
            students.Add(student);
            Console.WriteLine($"{name} has been added successfully!");
            return true;
        }

        private void GetAllStudents() // print out each student
        {
            students.ForEach(student =>
            {
                Console.WriteLine($"Id: {student.RollNumber}\nName: {student.Name}\nGrade: {student.Grade}\n");
            });
        }

        private Student GetStudentById(int rollNumber) // get by id with linq
        {
            var student = students.FirstOrDefault(s => s.RollNumber == rollNumber);
            return student;
        }

        private void UpdateStudentGrade()
        {
            string name = "";
            Student student = null;
            while (true) // validation for name
            {
                Console.WriteLine("Enter student's name:");
                name = Console.ReadLine();
                student = students.FirstOrDefault(s => s.Name == name);
                if (student != null) { break; } // if student exists, exit
                else Console.WriteLine("Student was not found. Try again");
            }

            Console.WriteLine("Enter grade");
            char grade = 'A';
            while (true) // validation for grade
            {
                try
                {
                    grade = char.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Enter a valid character for grade");
                }
            }

            students.FirstOrDefault(s => s.Name == name).Grade = grade;
            Console.WriteLine($"The grade of '{name}' has been changed to '{grade}'");
        }
    }
}
