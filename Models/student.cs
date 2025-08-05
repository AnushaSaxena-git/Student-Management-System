using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crudbyme.Models
{
    public class student
    {
        private DateTime dateOfBirth; private int age;

        [Key]
        public int StudentId { get; set; }
        [Required]
        [Column("studentFirstName", TypeName = "varchar(20)")]
        public string FirstName { get; set; }
        [Required]
        [Column("studentLasttName", TypeName = "varchar(20)")]
        public string LastName { get; set; }

        //public string FullName(string firstName, string lastName)
        //{
        //    this.FirstName = firstName;
        //    this.LastName = lastName;
        //    return firstName + lastName;
        //}
        //public string FullName => $"{FirstName} {LastName}";
        //public DateTime  DateOfBirth
        //{
        //    get { return DateTime.Now.AddYears(-Age).Year; }
        //}
        [Required]
        [Column("studentDateOfBirth")]
        //public DateTime DateOfBirth { get; set; }


        public DateTime DateOfBirth {
            get 
            { return dateOfBirth; } 
            set {
                dateOfBirth = value; 
                age = DateTime.Today.Year - dateOfBirth.Year - (dateOfBirth.Date > DateTime.Today.AddYears(-(DateTime.Today.Year - dateOfBirth.Year)) ? 1 : 0); } }


        //[Required]
        //[EmailAddress]
        //[RegularExpression(@ "[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        //public string Email { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$", ErrorMessage = "Please enter a valid email address")]

        public string Email { get; set; }
        public DateTime EnrollmentDate { get; set; }
        // private string _firstName;
        //private string _lastName;

        //public string firstName
        //{
        //    get => _firstName;
        //    set => _firstName = value;
        //}

        //public string lastName
        //{
        //    get => _lastName;
        //    set => _lastName = value;
        //}

        //// FullName property with both getter and setter
        //public string FullName
        //{
        //    get => $"{FirstName} {LastName}";
        //    set
        //    {
        //        if (value != null)
        //        {
        //            var names = value.Split(' '); // Split the full name by space
        //            FirstName = names[0]; // Set FirstName to the first part
        //            LastName = names.Length > 1 ? names[1] : string.Empty; // Set LastName to the second part (or empty if not present)
        //        }
        //    }
        //}

        [Required]
        [Column("studentage")]
       
        public int Age { get { return age; } set { age = value; } }

        [Required]
        [Column("studentsalary")]
        public int Salary {  get; set; }

        public string Password { get; set; }
        public string UserName { get; set; }


        [ForeignKey("Course")]
        public int CourseId { get; set; }

        [ForeignKey("Department")]
        public int DeptId { get; set; }  
        public Department Department { get; set; }
        public Course Course { get; set; }
        public ICollection<StudentCourses> StudentCourses { get; set; } 
    }
}
