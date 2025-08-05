using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crudbyme.Models
{
    public class Department
    {
        [Key]
        public int DeptId { get; set; }
       public string DeptName { get; set; }


        public ICollection<student> students { get; set; }// defining one to many relationship as one department has many students
        public ICollection<Course>courses { get; set; } // defining one to many relationship again because one  department has many courses
    }
}
