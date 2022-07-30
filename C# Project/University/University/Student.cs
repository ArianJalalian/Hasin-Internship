using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University
{
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


        public void PrintTakenCourses()
        { 
            if (takenCourses.Count <= 0)
            {
                Console.WriteLine("This student has no courses");
                return;
            } 


            takenCourses.ForEach( 
                delegate (Course course)
                { 
                    String teacherName = course.Teacher == null ? "nobody yet" : course.Teacher.Name;
                    Console.WriteLine($"{course.Name} taught by {teacherName} : {course.Description}");
                } 
                );  
        }

        public string enrolledCoursesIds()
        {
            String output = "";
            takenCourses.ForEach(
                delegate (Course course)
                {
                    output = output + (course.Id + "-");
                }
                );

            if (takenCourses.Count > 0)
                output = output.Remove(output.Length - 1) ;
            return output;
        }


        public void AddCourse(Course c)
        {
            if (takenCourses.Contains(c))
                Console.WriteLine($"Course {c.Name} already added to {Name}\'s list of courses");
            else
            {
                takenCourses.Add(c);
                Console.WriteLine("Successfully added");
            }
        }

        public void DeleteCourse(Course c)
        {
            try 
            {
                if(!takenCourses.Contains(c))
                Console.WriteLine($"This student does not have this course : {c.Name}");
                else
                    takenCourses.Remove(c);
            } 
            catch
            {
                Console.WriteLine("The object you passes is null");
            }


        }

    }
}
