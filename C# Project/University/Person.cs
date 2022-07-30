using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University
{
    abstract class Person
    {
        public string Name
        { get; set; }
        public string Id
        { get; set; }
    }


    class Student : Person
    {
        List<Course> takenCourses = new List<Course>();

        public Student(String name, String Id)
        {
            this.Name = name;
            this.Id = Id;
        }


        public int Age
        { get; set; }

        public Course[] TakenCourses
        {
            get
            {
                Course[] courses = new Course[takenCourses.Count()];
                takenCourses.CopyTo(courses);
                return courses;
            }
        }


        public void AddCourse(Course c)
        {
            takenCourses.Add(c);
        }

        public void DeleteCourse(Course c)
        {
            takenCourses.Remove(c);
        }

    }
    class Course
    {
        Hashtable enrolledStudentsMarks = new Hashtable();



        public Course(String name, String description)
        {
            this.Description = description;
            this.Name = name;
        }

        public Teacher Teacher
        { get; set; }

        public String Description
        { set; get; }

        public String Name
        { set; get; }

        public Student[] EnrolledStudents
        {
            get
            {
                Student[] students = new Student[enrolledStudentsMarks.Count];
                enrolledStudentsMarks.Keys.CopyTo(students, 0);
                return students;
            }
        }


        public void setMark(String Id, double mark)
        {
            /* 
             * TODO 
             */
        }

        public void AddStudents(Student s)
        {
            enrolledStudentsMarks.Add(s, -1);
        }

        public void DeleteStudent(Student s)
        {
            enrolledStudentsMarks.Remove(s);
        }


    }

    class Teacher : Person
    {
        List<Course> teachingCourses = new List<Course>();

        public Teacher(String name, String Id)
        {
            this.Name = name;
            this.Id = Id;
        }

        public int Age
        { get; set; }

        public Course[] TeachingCourse
        {
            get
            {
                Course[] courses = new Course[teachingCourses.Count()];
                teachingCourses.CopyTo(courses);
                return courses;
            }
        }

        public void AddCourse(Course c)
        {
            teachingCourses.Add(c);
        }

        public void DeleteCourse(Course c)
        {
            teachingCourses.Remove(c);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Student> list1 = new List<Student>();
            Student s1 = new Student("arian", "54");
            Student s2 = new Student("sam", "64");
            list1.Add(s1);
            list1.Add(s2);


            Student[] list3 = new Student[2];
            list1.CopyTo(list3);
            Console.WriteLine(list1.Count);
            list3[0] = null;
            Console.WriteLine(list1.Count);
            Console.WriteLine(list1.ElementAt(0));
        }
    }
}