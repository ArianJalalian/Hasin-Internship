using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University
{
    class Terminal
    {
        DataBase db = new DataBase(); 

        public void PrintFirstPage()
        {
            String command = "" ;

            while (command != "4")
            {
                Console.WriteLine("1.Admin");
                Console.WriteLine("2.SignUp");
                Console.WriteLine("3.Login");
                Console.WriteLine("4.End");


                command = Console.ReadLine();

                while (command == null || Int32.Parse(command) > 5 || Int32.Parse(command) < 0)
                {
                    Console.WriteLine("Invalid number");
                    command = Console.ReadLine();
                }

                switch (Int32.Parse(command))
                {
                    case 1:
                        PrintMainMenu("0");
                        break;
                    case 2:
                        PrintSignUpPage();
                        break;
                    case 3:
                        PrintLogInPage();
                        break;
                }
            }
        }

        public void PrintLogInPage()
        {
            Console.WriteLine("Your ID : "); 

            String enterdId = Console.ReadLine();
         
            if (db.Login(enterdId))
                PrintMainMenu(enterdId);
            else
                Console.WriteLine("Did not found user");

            return;   
        }

        public void PrintSignUpPage()
        {
            Console.WriteLine("What kind of user are you ?"); 
            Console.WriteLine("1.Student");
            Console.WriteLine("2.Teacher");

            String command = Console.ReadLine();

            while (Int32.Parse(command) != 1 && Int32.Parse(command) != 2)
            {
                Console.WriteLine("Invalid number");
                command = Console.ReadLine();
            }

            Console.WriteLine("What is your name ?");

            String name = Console.ReadLine(); 

            while(name == null)
            {
                Console.WriteLine("Please enter a valid name : ");
                name = Console.ReadLine();
            } 

            if (command == "1")
            {
                db.RegisterStudent("0", name);
            } 
            else
            {
                db.RegisterTeacher("0", name);
            }

            return;

        }

        public void PrintMainMenu(String Id)
        {
            String command = "";
            while (command != "11")
            {
                Console.WriteLine("1.Register a course");
                Console.WriteLine("2.Register a student");
                Console.WriteLine("3.Register a teacher");
                Console.WriteLine("4.Add a student to a course");
                Console.WriteLine("5.Set a teacher for a course");
                Console.WriteLine("6.Grade a student");
                Console.WriteLine("7.A student\'s list of courses");
                Console.WriteLine("8.A teacher\'s list of courses");
                Console.WriteLine("9.A class\'s list of students");
                Console.WriteLine("10.Set a description for a course");
                Console.WriteLine("11.Logout");

                command = Console.ReadLine();
                while (command == null || Int32.Parse(command) < 1 || Int32.Parse(command) > 11)
                {
                    Console.WriteLine("Enter a valid number");
                    command = Console.ReadLine();
                }



                switch (Int32.Parse(command))
                {
                    case 1:
                        PrintRegisterACoursePage(Id);
                        break;
                    case 2:
                        PrintRegisterAStudentPage(Id);
                        break;
                    case 3:
                        PrintRegisterATeacherPage(Id);
                        break;
                    case 4:
                        PrintAddAStudentToACoursePage();
                        break;
                    case 5:
                        PrintSetATeacherForACoursePage(Id);
                        break;
                    case 6:
                        PrintGradeAStudentPage(Id);
                        break;
                    case 7:
                        PrintAStudentListOfCourses();
                        break;
                    case 8:
                        PrintATeacherListOfCourses();
                        break;
                    case 9:
                        PrintACourseListOfStudents();
                        break;
                    case 10:
                        break;
                }
            }

        }

        public void PrintRegisterACoursePage(String Id)
        {
            Console.WriteLine("Enter a name :");  

            String name = Console.ReadLine();   

            while(name == null)
            {
                Console.WriteLine("Enter a valid name"); 
                name = Console.ReadLine();  
            }

            db.RegisterCourse(Id, name);

        }

        public void PrintRegisterAStudentPage(String Id)
        {
            Console.WriteLine("Enter a name :");

            String name = Console.ReadLine();

            while (name == null)
            {
                Console.WriteLine("Enter a valid name");
                name = Console.ReadLine();
            }

            db.RegisterStudent(Id, name);

        }

        public void PrintRegisterATeacherPage(String Id)
        {
            Console.WriteLine("Enter a name :");

            String name = Console.ReadLine();

            while (name == null)
            {
                Console.WriteLine("Enter a valid name");
                name = Console.ReadLine();
            }

            db.RegisterTeacher(Id, name);

        }

        public void PrintSetATeacherForACoursePage(String Id)
        {
            Console.WriteLine("Enter teacher ID :");
            String teacherId = Console.ReadLine();

            while (teacherId == null)
            {
                Console.WriteLine("Enter a valid id");
                teacherId = Console.ReadLine();
            }

            Console.WriteLine("Enter course ID :");
            String courseId = Console.ReadLine();

            while (courseId == null)
            {
                Console.WriteLine("Enter a valid id");
                courseId = Console.ReadLine();
            }

            db.AddCourseTeacher(Id, courseId, teacherId);
        }

        public void PrintAddAStudentToACoursePage()
        {
            Console.WriteLine("Enter student ID :");
            String studentId = Console.ReadLine();

            while (studentId == null)
            {
                Console.WriteLine("Enter a valid id");
                studentId = Console.ReadLine();
            }

            Console.WriteLine("Enter course ID :");
            String courseId = Console.ReadLine();

            while (courseId == null)
            {
                Console.WriteLine("Enter a valid id");
                courseId = Console.ReadLine();
            }

            db.AddCourseStudent(courseId, studentId);
        }

        public void PrintGradeAStudentPage(String Id)
        {
            Console.WriteLine("Enter student ID :");
            String studentId = Console.ReadLine();

            while (studentId == null)
            {
                Console.WriteLine("Enter a valid id");
                studentId = Console.ReadLine();
            }

            Console.WriteLine("Enter course ID :");
            String courseId = Console.ReadLine();

            while (courseId == null)
            {
                Console.WriteLine("Enter a valid id");
                courseId = Console.ReadLine();
            }

            Console.WriteLine("Enter the grade :");
            String grade = Console.ReadLine();

            while (grade == null || Double.Parse(grade) < 0 || Double.Parse(grade) > 20)
            {
                Console.WriteLine("Enter a valid grade");
                courseId = Console.ReadLine();
            }

            db.setMark(Id, studentId, courseId, Double.Parse(grade));
        }

        public void PrintAStudentListOfCourses()
        {
            Console.WriteLine("Enter student ID :");
            String studentId = Console.ReadLine();

            while (studentId == null)
            {
                Console.WriteLine("Enter a valid id");
                studentId = Console.ReadLine();
            } 

            db.PrintStudentCourses(studentId);

        }

        public void PrintATeacherListOfCourses()
        {
            Console.WriteLine("Enter teacher ID :");
            String teacherId = Console.ReadLine();

            while (teacherId == null)
            {
                Console.WriteLine("Enter a valid id");
                teacherId = Console.ReadLine();
            }

            db.PrintTeacherCourses(teacherId);

        }

        public void PrintACourseListOfStudents()
        {
            Console.WriteLine("Enter course ID :");
            String courseId = Console.ReadLine();

            while (courseId == null)
            {
                Console.WriteLine("Enter a valid id");
                courseId = Console.ReadLine();
            }

            db.PrintCoursesStudents(courseId);

        }
    }
}
