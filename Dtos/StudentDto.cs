using Humanizer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crudbyme.Dtos
{




    public class StudentDto
    {

        private DateTime dateOfBirth; 
        private int age;
        public int StudentId { get; set; }
        
        [Required(ErrorMessage = "First Name is a Required field.")]
        [DataType(DataType.Text)]
        [Display(Order = 1, Name = "FirstName")]
        [RegularExpression("^((?!^First Name$)[a-zA-Z '])+$", ErrorMessage = "First name is required and must be properly formatted.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is a Required field.")]
        [DataType(DataType.Text)]
        [Display(Order = 2, Name = "LastName")]
        [RegularExpression("^((?!^Last Name$)[a-zA-Z '])+$", ErrorMessage = "Last name is required and must be properly formatted.")]

        public string LastName { get; set; }
        [DataType(DataType.Currency)]
        [Range(5000, 200000, ErrorMessage = "Salary must be between 30,000 and 200,000")]
        public int Salary { get; set; }
        // public string FullName => $"{FirstName} {LastName}";
        private string _firstName;
        private string _lastName;

        public string firstName
        {
            get => _firstName;
            set => _firstName = value;
        }

        public string lastName
        {
            get => _lastName;
            set => _lastName = value;
        }

        // FullName property with both getter and setter
        public string FullName
        {
            get => $"{FirstName} {LastName}";
            set
            {
                if (value != null)
                {
                    var names = value.Split(' '); // Split the full name by space
                    FirstName = names[0]; // Set FirstName to the first part
                    LastName = names.Length > 1 ? names[1] : string.Empty; // Set LastName to the second part (or empty if not present)
                }
            }
        }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // public DateTime EnrollmentDate { get; set; }
        private DateTime enrollmentDate = DateTime.Now; // Set to current date and time
        public DateTime EnrollmentDate { get { return enrollmentDate; } set { enrollmentDate = value; } }

        [Required]
        [Column("studentDateOfBirth")]
        //public DateTime DateOfBirth { get; set; }
        public DateTime DateOfBirth 
        { get { return dateOfBirth; } 
        set { dateOfBirth = value; age = DateTime.Today.Year - dateOfBirth.Year - (dateOfBirth.Date > DateTime.Today.AddYears(-(DateTime.Today.Year - dateOfBirth.Year)) ? 1 : 0); } }

        [Range(16, 40)]
        //public int Age { get; set; }
        [Required]
        [Column("studentage")]
        public int Age { get { return age; } set { age = value; } }
        

        public string? CourseName {  get; set; }

        public string? DepartmentName { get; set; }
        public int CourseId { get; set; }//defining foreign key here

        public int DeptId { get; set; }  // defining another foreign key here 
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password should be at least 6 characters")]
        public string Password { get; set; }


    }
}
