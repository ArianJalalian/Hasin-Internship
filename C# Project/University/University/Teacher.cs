using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University
{
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

        public void PrintTeachingCourses()
        {
            if (teachingCourses.Count <= 0)
            {
                Console.WriteLine("This teacher does not teach any courses");
                return;
            }


            teachingCourses.ForEach(
                delegate (Course course)
                {
                    Console.WriteLine($"{course.Name} : {course.Description}");
                }
                );
        }

        public string teachingCoursesIds()
        {
            String output = "";
            teachingCourses.ForEach(
                delegate (Course course)
                {
                    output = output + (course.Id + "-");
                }
                );

            if (teachingCourses.Count > 0) 
                output = output.Remove(output.Length - 1) ;
            return output;
        }

        public void AddCourse(Course c)
        { 
            if (teachingCourses.Contains(c))
            {
                Console.WriteLine($"Course {c.Name} already added to {Name}\'s list of courses");
                return;
            } 
            teachingCourses.Add(c);

            Console.WriteLine("Successfully added");
        }

        public void DeleteCourse(Course c)
        {
            try
            {
                teachingCourses.Remove(c);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"This teacher does not teach this course : {c.Name}");
            }
        }
    }
}
