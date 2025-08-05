using Microsoft.EntityFrameworkCore;
using Crudbyme.Models; // Ensure you are using the correct namespace
using Microsoft.EntityFrameworkCore;  // For FromSqlRaw
using Microsoft.Data.SqlClient;      // For SqlParameter if you're using SQL parameters


namespace Crudbyme.Models
{
    public class studentdbcontext : DbContext
    {
        public studentdbcontext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeding Courses 
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    CourseId = 1,
                    CourseName = "DOTNET",
                    StartDate = new DateTime(2024, 01, 15),
                    EndDate = new DateTime(2024, 05, 15),
                    DeptId = 7
                },
                new Course
                {
                    CourseId = 2,
                    CourseName = "JAVA",
                    StartDate = new DateTime(2024, 02, 01),
                    EndDate = new DateTime(2024, 06, 01),
                    DeptId = 6
                },
                new Course
                {
                    CourseId = 3,
                    CourseName = "MCA",
                    StartDate = new DateTime(2024, 02, 01),
                    EndDate = new DateTime(2024, 06, 01),
                    DeptId = 5
                },
                new Course
                {
                    CourseId = 4,  // Changed from 2 to 4
                    CourseName = "MBA",
                    StartDate = new DateTime(2024, 02, 01),
                    EndDate = new DateTime(2024, 06, 01),
                    DeptId = 4
                },
                new Course
                {
                    CourseId = 5,
                    CourseName = "BTECH",
                    StartDate = new DateTime(2024, 02, 01),
                    EndDate = new DateTime(2024, 06, 01),
                    DeptId = 3
                },
                new Course
                {
                    CourseId = 6,
                    CourseName = "PYTHON",
                    StartDate = new DateTime(2024, 02, 01),
                    EndDate = new DateTime(2024, 06, 01),
                    DeptId = 2
                },
                new Course
                {
                    CourseId = 7,  // Changed from 6 to 7
                    CourseName = "ASP.NET Core Fundamentals",
                    StartDate = new DateTime(2024, 02, 01),
                    EndDate = new DateTime(2024, 06, 01),
                    DeptId =1
                }
            );

            // Seeding Departments
            modelBuilder.Entity<Department>().HasData(
                new Department { DeptId = 1, DeptName = "HR" },
                new Department { DeptId = 2, DeptName = "IT" },
                new Department { DeptId = 3, DeptName = "CS" },
                new Department { DeptId = 4, DeptName = "EE" },
                new Department { DeptId = 5, DeptName = "ME" },
                new Department { DeptId = 6, DeptName = "EC" },
                new Department { DeptId = 7, DeptName = "Finance" }
                
            );

            modelBuilder.Entity<student>()
                .HasMany(s => s.StudentCourses) // A student can have many courses
                .WithOne(c => c.student).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ClaimDataView>().HasNoKey().ToView(null);
            // Many-to-many relationship between Student and Course
            //modelBuilder.Entity<student>()
            //    .HasMany(s => s.Courses) // A student can have many courses
            //    .WithMany(c => c.students) // A course can have many students
            //    .UsingEntity<StudentCourses>(  // Use the join entity class
            //        j => j
            //            .HasOne(sc => sc.Course)  // Foreign key relationship to Course
            //            .WithMany(c => c.StudentCourses)  // Navigation property on Course
            //            .HasForeignKey(sc => sc.CourseId)
            //            .OnDelete(DeleteBehavior.NoAction),
            //        j => j
            //            .HasOne(sc => sc.student)  // Foreign key relationship to Student
            //            .WithMany(s => s.StudentCourses)  // Navigation property on Student
            //            .HasForeignKey(sc => sc.studentId)
            //            .OnDelete(DeleteBehavior.NoAction),
            //        j =>
            //        {
            //            j.HasKey(t => new { t.studentId, t.CourseId }); // Composite key
            //            j.ToTable("StudentCourses"); // Name of the join table
            //        });

            //// One-to-many relationship between Student and Department
            //modelBuilder.Entity<student>()
            //    .HasOne(s => s.Department) // A student belongs to one department
            //    .WithMany(d => d.students) // A department has many students
            //    .HasForeignKey(s => s.DeptId); // Foreign key in Student

            //// Mapping column names for student
            //modelBuilder.Entity<student>()
            //    .Property(s => s.StudentId)
            //    .HasColumnName("studentID");  // Mapping StudentId to a unique column

            // Uncommented and applied FullName to studentName mapping
            //modelBuilder.Entity<student>()
            //    .Property(s => s.FullName)
            //    .HasColumnName("studentName");  // Mapping FullName to studentName column
        }

        public DbSet<student> Students { get; set; }  // DbSet for Students
        public DbSet<Course> Courses { get; set; }  // DbSet for Courses
        public DbSet<Department> Departments { get; set; }  // DbSet for Departments
        public DbSet<StudentCourses> StudentCourses { get; set; }  // Join table DbSet for Student-Course relationship
        public DbSet<ClaimDataView> ClaimDataView { get; set; }
    }
}
