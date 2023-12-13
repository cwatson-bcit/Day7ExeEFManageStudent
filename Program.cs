using System;
using System.Data.Entity;
using System.Linq;

namespace Day7ExeEFManageStudent
{
    internal class Program
    {
        static void Main()
        {
            bool isNotExit = true;
            while (isNotExit)
            {
                Console.Clear();
                Console.WriteLine("\n\tMain Menu:");
                Console.WriteLine("\t1. List all Students");
                Console.WriteLine("\t2. List all Courses");
                Console.WriteLine("\t3. List all Student Enrollments");
                Console.WriteLine("\t4. List all Instructor Assignments");
                Console.WriteLine("\t5. Exit");
                Console.Write("\n\tEnter your choice: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        ListAllStudents();
                        break;
                    case "2":
                        ListAllCourses();
                        break;
                    case "3":
                        ListStudentEnrollments();
                        break;
                    case "4":
                        ListInstructorsAssignments();
                        break;
                    case "5":
                        Console.WriteLine("\n\tThank you for using the student registry.");
                        isNotExit = false;
                        break;
                    default:
                        Console.WriteLine("\n\tInvalid choice. Please select a valid option (between 1 and 5).");
                        break;
                }
                Console.Write("\n\tPress any key to continue...");
                Console.ReadKey();
            }
        }

        static void ListAllStudents()
        {
            Console.WriteLine($"\n\tStudent List\n\t------------");

            using (StudentEntities db = new StudentEntities())
            {
                // Return all student records
                DbSet<Student> students = db.Students;

                // Loop through the list and write each student to the console
                foreach (Student student in students)
                {
                    Console.WriteLine($"\tId: {student.pkStudentId}, " +
                                      $"{student.firstName} {student.lastName}, " +
                                      $"Enrollment: {student.enrollmentDate:dd/MMM/yyyy}");
                }
            }
        }

        static void ListAllCourses()
        {
            Console.WriteLine($"\n\tCourse List\n\t-----------");

            using (StudentEntities db = new StudentEntities())
            {
                // Return all course records
                DbSet<Course> courses = db.Courses;

                // Loop through the list and write each course to the console
                foreach (Course course in courses)
                {
                    Console.WriteLine($"\t{course.title}, credits: {course.credits}");
                }
            }
        }

        static void ListStudentEnrollments()
        {
            Console.WriteLine($"\n\tEnrollment List\n\t---------------");

            using (StudentEntities db = new StudentEntities())
            {
                Console.Write("\n\tPlease enter a Student Id: ");

                if (Int32.TryParse(Console.ReadLine(), out int studentId))
                {
                    // Get student courses
                    var studentEnrollments = from s in db.Students
                                             join e in db.Enrollments on s.pkStudentId equals e.fkStudentId
                                             join c in db.Courses on e.fkCourseId equals c.pkCourseId
                                             where s.pkStudentId == studentId
                                             select new
                                             {
                                                 c.title,
                                                 e.finalGrade,
                                                 s.firstName,
                                                 s.lastName
                                             };

                    if (studentEnrollments != null && studentEnrollments.Count() > 0)
                    {
                        string name = $"\n\t{studentEnrollments.First().firstName} " +
                                      $"{studentEnrollments.First().lastName}";

                        // Create a string of hyphens of the same length as the name
                        string underline = String.Concat(Enumerable.Repeat("-", name.Length - 2));

                        Console.WriteLine($"\n\t{name} \n\t{underline}");

                        // Loop through the list and write each student enrollment to the console
                        foreach (var enrollment in studentEnrollments)
                        {
                            Console.WriteLine($"\t{enrollment.title} - " +
                                              $"Final Grade: {enrollment.finalGrade}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\tInvalid input!");
                }
            }
        }

        static void ListInstructorsAssignments()
        {
            Console.WriteLine($"\n\tInstructors Assignments\n\t-----------------------");

            using (StudentEntities db = new StudentEntities())
            {
                // Get instructors assignments
                var instructorAssignments = from i in db.Instructors
                                            join ic in db.InstructorCourses on i.pkInstructorId equals ic.fkInstructorId
                                            join c in db.Courses on ic.fkCourseId equals c.pkCourseId
                                            select new
                                            {
                                                c.title,
                                                ic.teachingAssignment,
                                                i.firstName,
                                                i.lastName
                                            };

                if (instructorAssignments != null && instructorAssignments.Count() > 0)
                {
                    // Loop through the list and write each instructors' courses to the console
                    foreach (var instructorAssignment in instructorAssignments)
                    {
                        string name = $"{instructorAssignment.firstName} " +
                                      $"{instructorAssignment.lastName}";

                        Console.WriteLine($"\t{name}: {instructorAssignment.teachingAssignment} - " +
                                          $"{instructorAssignment.title}");
                    }
                }
            }
        }
    }
}