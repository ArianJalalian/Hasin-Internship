using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University
{
    class Course
    {
        Dictionary<Student,double> enrolledStudentsMarks = new Dictionary<Student, double>();


        public Course(String name, String Id, String description = "No description added yet", Teacher teacher = null)
        {
            this.Description = description;
            this.Name = name;
            this.Id = Id;
            this.Teacher = teacher;
        }

        public Teacher Teacher
        { get; set; }

        public String Description
        { set; get; }

        public String Name
        { set; get; }

        public String Id
        {  get; }

        public Student[] EnrolledStudents
        {
            get
            {
                Student[] students = new Student[enrolledStudentsMarks.Count];
                enrolledStudentsMarks.Keys.CopyTo(students, 0);
                return students;
            }
        }


        public void PrintEnrolledStudents()
        {
            if (enrolledStudentsMarks.Count <= 0)
            {
                Console.WriteLine("This course has no students");
                return;
            }




            enrolledStudentsMarks.Keys.ToList().ForEach(
                delegate (Student student)
                {
                    String mark = enrolledStudentsMarks[student] == -1 ? "nothing yet" : enrolledStudentsMarks[student].ToString();
                    Console.WriteLine($"{student.Name} , {student.Age} years old with {student.Id} as an ID got {mark}");
                }
                );
        } 


        public string enrolledStudentsIds()
        {
            String output = "";
            enrolledStudentsMarks.Keys.ToList().ForEach( 
                delegate (Student student) 
                {
                    output = output + (student.Id + "-");
                }
                );

            if (enrolledStudentsMarks.Count > 0)
                output = output.Remove(output.Length - 1) ;
            return output;
        }


        public void setMark(String Id, double mark)
        { 


            int idx = enrolledStudentsMarks.Keys.ToList().FindIndex
                (
                   student => student.Id == Id
                ) ;


            if (idx == -1)
            {
                Console.WriteLine($"{Id} as a student does not enroll this course");
                return;
            } 

            enrolledStudentsMarks[enrolledStudentsMarks.ElementAt(idx).Key] = mark;

            Console.WriteLine("Successfully graded");
        }

        public void AddStudents(Student s)
        {
            try
            {
                enrolledStudentsMarks.Add(s, -1);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"student {s.Id} already exists in this course");
            }
        }

        public void DeleteStudent(Student s)
        { 
            try
            {
                enrolledStudentsMarks.Remove(s);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Student {s.Id} does not exist");
            }
        }


    }
}
