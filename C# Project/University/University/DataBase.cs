using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University
{
    class DataBase
    {
        public Dictionary<String, Student> students;
        public Dictionary<String, Teacher> teachers;
        public Dictionary<String, Course> courses;

        //HashSet<String> Ids;

        Admin admin;

        int idBase = 0;


        public DataBase()
        {
            students = new Dictionary<String, Student>(); 
            teachers = new Dictionary<String, Teacher>();
            courses = new Dictionary<String, Course>();
            //Ids = new HashSet<string>();
            admin = new Admin();

            //Ids.Add(admin.Id);
        }  


        public bool Login(String Id)
        {
            if (students.ContainsKey(Id) || teachers.ContainsKey(Id)) 
                return true;
            return false;
        }

        public void RegisterStudent(String registratorId, String name)
        {
            if (admin.Id != registratorId)
            {
                Console.WriteLine("You don't have the privileges to register a student");
                return;
            }

            Student newStudent = new Student(name, (idBase+1).ToString());
            idBase++;

            students.Add(newStudent.Id, newStudent);
            writeStudentToFile(newStudent.Id);

            Console.WriteLine($"{newStudent.Name} successfully registered");
            Console.WriteLine($"Your Id is {newStudent.Id}");
            //Ids.Add(newStudent.Id);

        }

        public void RegisterTeacher(String registratorId, String name)
        {
            if (admin.Id != registratorId)
            {
                Console.WriteLine("You don't have the privileges to register a Teacher");
                return;
            }

            Teacher newTeacher= new Teacher(name, (idBase + 1).ToString());
            idBase++;

            teachers.Add(newTeacher.Id, newTeacher);
            writeTeacherToFile(newTeacher.Id);

            Console.WriteLine($"{newTeacher.Name} successfully registered");
            Console.WriteLine($"Your Id is {newTeacher.Id}");
            //Ids.Add(newTeacher.Id);

        }

        public void RegisterCourse(String registratorId, String name, String description="No description added yet")
        {
            if (admin.Id != registratorId)
            {
                Console.WriteLine("You don't have the privileges to register a course");
                return;
            }



            Course newCourse= new Course(name, (idBase + 1).ToString(), description);
            idBase++;

            //Console.WriteLine(newCourse.Id);

            courses.Add(newCourse.Id, newCourse);
            writeCourseToFile(newCourse.Id);

            Console.WriteLine($"{newCourse.Name} successfully registered");
            Console.WriteLine($"Your course Id is {newCourse.Id}");
            //Ids.Add(newCourse.Id);

        }

        public void AddCourseStudent(String courseId, String studentId)
        {
            if (!courses.Keys.Contains(courseId))
            {
                Console.WriteLine($"There is no course with {courseId} ID registered");
            }
            if (!students.Keys.Contains(studentId))
            {
                Console.WriteLine($"There is no student with {studentId} ID registered");
                return;
            }


            courses[courseId].AddStudents(students[studentId]);
            students[studentId].AddCourse(courses[courseId]);

            writeStudentToFile(studentId);
            writeCourseToFile(courseId);

        }

        public void AddCourseTeacher(String Id, String courseId, String teacherId)
        {  

            if (Id != admin.Id || teachers.Keys.Contains(Id))
            {
                Console.WriteLine("You don't have the privileges to set a teacher for a course");
                return;
            }
            if (!courses.Keys.Contains(courseId))
            {
                Console.WriteLine($"There is no course with {courseId} ID registered");
            }
            if (!teachers.Keys.Contains(teacherId))
            {
                Console.WriteLine($"There is no teacher with {teacherId} ID registered");
                return;
            }


            courses[courseId].Teacher = teachers[teacherId];
            teachers[teacherId].AddCourse(courses[courseId]);

            writeCourseToFile(courseId);
            writeTeacherToFile(teacherId);


        } 

        public void setMark(String Id, String studentId, String courseId, double mark)
        {
            if (!courses.Keys.Contains(courseId))
            {
                Console.WriteLine($"There is no course with {courseId} ID registered");
            }
            if (Id != admin.Id && courses[courseId].Teacher.Id != Id)
            {
                Console.WriteLine("You don't have the privileges to set a garde");
                return;
            }
            if (!students.Keys.Contains(studentId))
            {
                Console.WriteLine($"There is no student with {studentId} ID registered");
                return;
            }
            

            if (mark < 0 && mark > 20)
            {
                Console.WriteLine("Please enter a valid garde");
                return;
            }


            courses[courseId].setMark(studentId, mark);
        }  

        public void writeCourseToFile(String courseId)
        {
            if (!courses.Keys.Contains(courseId))
            {
                Console.WriteLine($"There is no course with {courseId} ID registered");
                return;
            }

            String fileText = File.ReadAllText(@"C:\Users\HP\source\repos\University\University\Database\courses.txt");
            String[] coursesData = fileText.Split("\n");


            List<String> modifiedCoursesData = new List<string>(); 
            if (coursesData.Length > 0 && coursesData[0].Length > 0 )
            {
                modifiedCoursesData = coursesData.ToList().Where(
                   line => line.Split(",")[0].Split(":")[1] != courseId).ToList();
            }
               
           

            Course course = courses[courseId]; 
            String teacherId = course.Teacher == null ? "-1" : course.Teacher.Id;

            String newCourseData = $"id:{course.Id},name:{course.Name},description:{course.Description},teacher:{teacherId}," +
                $"enrolled students ids:{course.enrolledStudentsIds()}";

            modifiedCoursesData.Add(newCourseData);


            String text = "";
            modifiedCoursesData.ForEach( 
                delegate (String line)
                {
                    text = text + line + "\n";
                }
                );

            text = text.Remove(text.Length - 1);
            File.WriteAllText(@"C:\Users\HP\source\repos\University\University\Database\courses.txt", text);
        
        }

        public void writeStudentToFile(String studentId)
        {
            if (!students.Keys.Contains(studentId))
            {
                Console.WriteLine($"There is no course with {studentId} ID registered");
                return;
            }

            String fileText = File.ReadAllText(@"C:\Users\HP\source\repos\University\University\Database\students.txt");
            String[] studentsData = fileText.Split("\n");


            List<String> modifiedStudentsData = new List<string>();
            if (studentsData.Length > 0 && studentsData[0].Length > 0)
            {
                modifiedStudentsData = studentsData.ToList().Where(
                   line => line.Split(",")[0].Split(":")[1] != studentId).ToList();
            }



            Student student = students[studentId];


            String newStudentData = $"id:{student.Id},name:{student.Name},age:{student.Age}," +
                $"enrolled courses ids:{student.enrolledCoursesIds()}";


            modifiedStudentsData.Add(newStudentData);


            String text = "";
            modifiedStudentsData.ForEach(
                delegate (String line)
                {
                    text = text + line + "\n";
                }
                );

            text = text.Remove(text.Length - 1);
            File.WriteAllText(@"C:\Users\HP\source\repos\University\University\Database\students.txt", text);
        }

        public void writeTeacherToFile(String teacherId)
        {
            if (!teachers.Keys.Contains(teacherId))
            {
                Console.WriteLine($"There is no course with {teacherId} ID registered");
                return;
            }


            String fileText = File.ReadAllText(@"C:\Users\HP\source\repos\University\University\Database\teachers.txt");
            String[] teachersData = fileText.Split("\n");


            List<String> modifiedTeachersData = new List<string>();
            if (teachersData.Length > 0 && teachersData[0].Length > 0)
            {
                modifiedTeachersData = teachersData.ToList().Where(
                   line => line.Split(",")[0].Split(":")[1] != teacherId).ToList();
            }


            Teacher teacher= teachers[teacherId];

            String newTeacherData = $"id:{teacher.Id},name:{teacher.Name},age:{teacher.Age}," +
                $"teaching courses ids:{teacher.teachingCoursesIds()}";


            modifiedTeachersData.Add(newTeacherData);


            String text = "";
            modifiedTeachersData.ForEach(
                delegate (String line)
                {
                    text = text + line + "\n";
                }
                );

            text = text.Remove(text.Length - 1);
            File.WriteAllText(@"C:\Users\HP\source\repos\University\University\Database\teachers.txt", text);
        }

        public void PrintStudentCourses(String Id)
        {
            if (!students.Keys.Contains(Id))
            {
                Console.WriteLine("Student did not found");
                return;
            }

            Student student = students[Id];
            student.PrintTakenCourses();
        }

        public void PrintTeacherCourses(String Id)
        {
            if (!teachers.Keys.Contains(Id))
            {
                Console.WriteLine("Teacher did not found");
                return;
            }

            Teacher teacher= teachers[Id];
            teacher.PrintTeachingCourses();
        }

        public void PrintCoursesStudents(String Id)
        {
            if (!courses.Keys.Contains(Id))
            {
                Console.WriteLine("Course did not found");
                return;
            }

            Course course = courses[Id];
            course.PrintEnrolledStudents();
        }

        //public void updateStudentsDict() 
        //{
        //    String fileText = File.ReadAllText(@"C:\Users\HP\source\repos\University\University\Database\teachers.txt");
        //    String [] studentsData = fileText.Split("\n");

        //    studentsData.ToList().ForEach( 
        //        delegate (String studentData)
        //        {
        //            String[] fields = studentData.Split(",");

        //            if (students.Keys.Contains(fields[0].Split(":")[1])) 
        //                // check if this specific id is already in list
        //                return;
        //            Student student = new Student(fields[1].Split(":")[1], fields[0].Split(":")[1] );
        //            student.Age = Int32.Parse(fields[2].Split(":")[1]) ; 


        //        }
        //        );
        //}



    }
}
