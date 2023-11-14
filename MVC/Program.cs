// See https://aka.ms/new-console-template for more information
using MVC;
using System.Data.Entity;
using System.Diagnostics;

using (var context = new StudentContext())
{
    // Create and save a new Students
    Console.WriteLine("Adding new students");

    var student = new Student
    {
        FirstMidName = "Atyia",
        LastName = "Alam",
        EnrollmentDate = DateTime.Parse(DateTime.Today.ToString()),


        PhoneNumber = "+9201234567"
    };

    context.Students?.Add(student);



    var student1 = new Student
    {
        FirstMidName = "Ali",
        LastName = "Ahmed",
        EnrollmentDate = DateTime.Parse(DateTime.Today.ToString()),
        
        PhoneNumber = "+9201234567"
    };

    context.Students?.Add(student1);
    context.SaveChanges();

    // Display all Students from the database
    var students = (from s in context.Students
                    orderby s.FirstMidName
                    select s).ToList<Student>();

    Console.WriteLine("Retrieve all Students from the database:");

    foreach (var stdnt in students)
    {
        string name = stdnt.FirstMidName + " " + stdnt.LastName;
        Console.WriteLine("ID: {0}, Name: {1}", stdnt.ID, name);
    }

    // Update a student
    if (students.Count > 0)
    {
        var studentToUpdate = students[0];
        studentToUpdate.FirstMidName = "Atiya Ali";
        context.UpdateStudent(studentToUpdate);
        context.SaveChanges();
    }

    // Delete a student
    if (students.Count > 1)
    {
        var studentToDelete = students[1];
        context.DeleteStudent(studentToDelete);
        context.SaveChanges();
    }

    // Display updated Students from the database
    var updatedStudents = (from s in context.Students
                           orderby s.FirstMidName
                           select s).ToList<Student>();

    Console.WriteLine("Retrieve updated Students from the database:");

    foreach (var stdnt in updatedStudents)
    {
        string name = stdnt.FirstMidName + " " + stdnt.LastName;
        Console.WriteLine("ID: {0}, Name: {1}", stdnt.ID, name);
    }



    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
}


namespace MVC {
    public enum Grade
    {
        A, B, C, D, F
    }
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade? Grade { get; set; }

        public virtual Course? Course { get; set; }
        public virtual Student? Student { get; set; }
    }

    public class Student
    {
        public int ID { get; set; }
        public string? LastName { get; set; }
        public string? FirstMidName { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public virtual ICollection<Enrollment>? Enrollments { get; set; }
    }

    public class Course
    {
        public int CourseID { get; set; }
        public string? Title { get; set; }
        public int Credits { get; set; }

        public virtual ICollection<Enrollment>? Enrollments { get; set; }
    }

    public class StudentContext : DbContext
    {
        public virtual DbSet<Course>? Courses { get; set; }
        public virtual DbSet<Enrollment>? Enrollments { get; set; }
        public virtual DbSet<Student>? Students { get; set; }
        // Method to update a student
        public void UpdateStudent(Student student)
        {
            Entry(student).State = EntityState.Modified;
        }

        // Method to delete a student
        public void DeleteStudent(Student student)
        {
            Entry(student).State = EntityState.Deleted;
        }
    }



}

